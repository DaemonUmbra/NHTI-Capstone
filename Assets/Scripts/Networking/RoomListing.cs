﻿using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour {
    //Finished
    [SerializeField]
    private Text _roomNameText;
    private Text RoomNameText
    {
        get { return _roomNameText; }
    }

    public int maxPlayersCount;
    public int currentPlayersCount;
    public Text playerCount;
    public bool status;

    public string RoomName { get; private set; }

    public bool Updated { get; set; }


	// Use this for initialization
	private void Start () {
        GameObject lobbyCanvasObj = MainCanvasManager.Instance.LobbyCanvas.gameObject;
        if (lobbyCanvasObj == null) { return; }

        LobbyCanvas lobbyCanvas = lobbyCanvasObj.GetComponent<LobbyCanvas>();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => lobbyCanvas.OnClickJoinRoom(RoomNameText.text));

	}

    private void Update()
    {
        status = Updated;
        playerCount.text = currentPlayersCount + "/" + maxPlayersCount;
    }

    private void OnDestroy()
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }

    public void SetRoomNameText(string text)
    {
        RoomName = text;
        RoomNameText.text = RoomName;
    }

}
