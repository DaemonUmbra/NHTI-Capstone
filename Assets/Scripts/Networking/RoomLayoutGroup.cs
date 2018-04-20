using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutGroup : MonoBehaviour
{
    //Finished
    private GameObject lobby;

    private bool status;

    private void Start()
    {
        lobby = GameObject.Find("LobbyNetwork");
    }

    private void Update()
    {
        if (status != LobbyManager.HideFullRoom)
        {
            RefreshList();
            status = LobbyManager.HideFullRoom;
        }
        OnReceivedRoomListUpdate();
    }

    [SerializeField]
    private GameObject _roomListingPrefab;

    private GameObject RoomListingPrefab
    {
        get { return _roomListingPrefab; }
    }

    private List<RoomListing> _roomListingButtons = new List<RoomListing>();

    private List<RoomListing> RoomListingButtons
    {
        get { return _roomListingButtons; }
    }

    private void OnReceivedRoomListUpdate()
    {
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();

        foreach (RoomInfo room in rooms)
        {
            RoomReceived(room);
        }
        RemoveOldRooms();
    }

    private void RoomReceived(RoomInfo room)
    {
        int index = RoomListingButtons.FindIndex(x => x.RoomName == room.Name);
        if (index == -1)
        {
            if (room.IsVisible)
            {
                if (LobbyManager.HideFullRoom)
                {
                    if (room.PlayerCount < room.MaxPlayers)
                    {
                        GameObject roomListingObj = Instantiate(RoomListingPrefab);
                        roomListingObj.transform.SetParent(transform, false);

                        RoomListing roomListing = roomListingObj.GetComponent<RoomListing>();
                        RoomListingButtons.Add(roomListing);

                        index = (RoomListingButtons.Count - 1);
                    }
                }
                else
                {
                    GameObject roomListingObj = Instantiate(RoomListingPrefab);
                    roomListingObj.transform.SetParent(transform, false);

                    RoomListing roomListing = roomListingObj.GetComponent<RoomListing>();
                    RoomListingButtons.Add(roomListing);

                    index = (RoomListingButtons.Count - 1);
                }
            }
        }

        if (index != -1)
        {
            RoomListing roomListing = RoomListingButtons[index];
            roomListing.SetRoomNameText(room.Name);
            roomListing.currentPlayersCount = room.PlayerCount;
            roomListing.maxPlayersCount = room.MaxPlayers;
            roomListing.Updated = true;
        }
    }

    private void RefreshList()
    {
        foreach (RoomListing roomListing in RoomListingButtons)
        {
            GameObject roomListingObj = roomListing.gameObject;
            RoomListingButtons.Remove(roomListing);
            Destroy(roomListingObj);
        }
        OnReceivedRoomListUpdate();
    }

    private void RemoveOldRooms()
    {
        List<RoomListing> removeRooms = new List<RoomListing>();

        foreach (RoomListing roomListing in RoomListingButtons)
        {
            if (!roomListing.Updated)
            {
                removeRooms.Add(roomListing);
            }
            else
            {
                roomListing.Updated = false;
            }
        }

        foreach (RoomListing roomListing in removeRooms)
        {
            GameObject roomListingObj = roomListing.gameObject;
            RoomListingButtons.Remove(roomListing);
            Destroy(roomListingObj);
        }
    }
}