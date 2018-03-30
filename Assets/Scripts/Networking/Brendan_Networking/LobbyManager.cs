using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LobbyState { LOGIN, LOBBY, ROOM, GAME, MAP }
public class LobbyManager : Photon.PunBehaviour {

    #region Private Variables
    static LobbyState _clientState;
    [SerializeField]
    MainCanvasManager _canvasManager;
    private int level = 1;
    #endregion


    #region Public Variables
    public static bool HideFullRoom = true;
    public Button loginButton;
    public Text connectingText;
    #endregion


    #region Access Variables
    public static LobbyState State { get { return _clientState; } }
    public MainCanvasManager CanvasManager { get { return _canvasManager; } }
    
    #endregion

  
    #region Public Methods
    public void ChangeState(LobbyState newState)
    {
        switch(newState)
        {
            // Login
            case LobbyState.LOGIN:
                Debug.Log("Entered Login State");
                _canvasManager.ChangeLobbyState(LobbyState.LOGIN);
                if (PhotonNetwork.connected) { ChangeState(LobbyState.LOBBY); }
                break;

            // Main Lobby
            case LobbyState.LOBBY:
                Debug.Log("Entering Lobby State");
                PhotonNetwork.JoinLobby(TypedLobby.Default);
                _canvasManager.ChangeLobbyState(LobbyState.LOBBY);
                break;

            // Room Lobby
            case LobbyState.ROOM:
                Debug.Log("Entering Room State");
                _canvasManager.ChangeLobbyState(LobbyState.ROOM);
                break;

            // In Game
            case LobbyState.GAME:
                Debug.Log("Entering Game State");
                break;

            // Selecting Map
            case LobbyState.MAP:
                Debug.Log("Entering Game State");
                _canvasManager.ChangeLobbyState(LobbyState.MAP);
                break;
        }
    }
    public void Login()
    {
        // Change state to lobby
        ChangeState(LobbyState.LOBBY);
    }
    public void CreateRoom()
    {
        ChangeState(LobbyState.ROOM);
    }

    public void StartGame()
    {
        if (!PhotonNetwork.isMasterClient)
            return;
        photonView.RPC("RPC_ChangeLevel", PhotonTargets.MasterClient);
        ChangeState(LobbyState.GAME);
    }
    public void MapsMenu()
    {
        ChangeState(LobbyState.MAP);
    }
    public void ChooseScene(int levelChoice)
    {
        level = levelChoice;
        ChangeState(LobbyState.LOBBY);
    }
    #endregion


    #region RPCs
    [PunRPC]
    private void RPC_ChangeLevel()
    {
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;
        PhotonNetwork.LoadLevel(level);


    }
    #endregion

    #region Click Events
    public void OnClickStartSync()
    {
        StartGame();
    }
    public void OnClickStartDelayed()
    {
        StartGame();
    }
    public void OnClickRoomState()
    {
        if (!PhotonNetwork.isMasterClient)
            return;

        PhotonNetwork.room.IsOpen = !PhotonNetwork.room.IsOpen;
        PhotonNetwork.room.IsVisible = PhotonNetwork.room.IsOpen;
    }
    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        ChangeState(LobbyState.LOBBY);
    }
    #endregion


    #region Unity Callbacks
    private void Awake()
    {
        // Connect to server
        loginButton.interactable = false;
        PhotonNetwork.ConnectUsingSettings("0.0.0");

        // Find canvas manager if not set manually
        if (!_canvasManager)
        {
            GameObject cm = GameObject.Find("Canvas");
            if (cm)
                _canvasManager = cm.GetComponent<MainCanvasManager>();
        }

        // Set the initial game state
        ChangeState(LobbyState.LOGIN);
    }
    #endregion
    

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {

        loginButton.interactable = true;
        connectingText.text = "Connected!";
        print("Connected to master.");
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;
        base.OnConnectedToMaster();
    }
    public override void OnJoinedLobby()
    {
        print("Joined lobby.");
        base.OnJoinedLobby();
    }
    public override void OnJoinedRoom()
    {
        print("Joined room.");
        ChangeState(LobbyState.ROOM);
        base.OnJoinedRoom();
    }
    #endregion
}

