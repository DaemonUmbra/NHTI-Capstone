﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour {
    //Finished
    [SerializeField]
    private Text _roomName;
    public Text RoomName
    {
        get { return _roomName; }
    }

    public void OnClick_CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };

        if(PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default))
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
