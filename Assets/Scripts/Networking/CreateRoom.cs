using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour {

    [SerializeField]
    private Text _roomName;
    private Text RoomName
    {
        get { return _roomName; }
    }

    public void OnClick_CreateRoom()
    {
        if(PhotonNetwork.CreateRoom(RoomName.text))
        {
            Debug.Log("Room: " + RoomName.text + " created!");
        } else {
            Debug.Log("Failed to create room!");
        }
    }

    private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        Debug.Log("Create room failed " + codeAndMessage[1]);
    }

    private void OnCreatedRoom()
    {
        Debug.Log("Created room successfully!");
    }

}
