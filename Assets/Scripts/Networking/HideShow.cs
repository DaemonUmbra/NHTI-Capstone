using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideShow : MonoBehaviour {
    private GameObject lobby;
    public Text buttonText;


	// Use this for initialization
	void Start () {
        lobby = GameObject.Find("LobbyNetwork");
    }

    public void ChangeLobbyStatus()
    {
        if (lobby.GetComponent<LobbyNetwork>().HideFullRoom == false)
        {
            lobby.GetComponent<LobbyNetwork>().HideFullRoom = true;
            buttonText.text = "Show\nFull\nRooms";
        } else {
            lobby.GetComponent<LobbyNetwork>().HideFullRoom = false;
            buttonText.text = "Hide\nFull\nRooms";
        }
    }
}
