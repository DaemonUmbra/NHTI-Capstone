using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour {

    public GameObject[] currentRooms;
    public bool HideFullRoom;

    private void Update()
    {
        currentRooms = GameObject.FindGameObjectsWithTag("RoomListing");
        if (HideFullRoom)
        {
            foreach (var room in currentRooms)
            {
                if (room.GetComponent<RoomListing>().currentPlayersCount == room.GetComponent<RoomListing>().maxPlayersCount)
                {

                    Debug.Log(room.GetComponent<RoomListing>().RoomName + " is full!");
                }
                
            }
        }
    }

    //Finished
    private void Start () {
        print("Connecting to server..");
        PhotonNetwork.ConnectUsingSettings("0.0.0");
	}

    private void OnConnectedToMaster()
    {
        print("Connected to master.");
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    private void OnJoinedLobby()
    {
        print("Joined lobby.");

        if(!PhotonNetwork.inRoom) { MainCanvasManager.Instance.LobbyCanvas.transform.SetAsLastSibling(); }
    }
}
