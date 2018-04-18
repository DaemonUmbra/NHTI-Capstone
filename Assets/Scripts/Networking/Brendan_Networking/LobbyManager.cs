using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public enum LobbyState { LOGIN, LOBBY, ROOM, GAME, MAP }
public class LobbyManager : Photon.PunBehaviour {

    #region Private Variables
    static LobbyState _clientState;
    [SerializeField]
    MainCanvasManager _canvasManager;
    private int level = 1;
    private Room _room;
    #endregion


    #region Public Variables
    public static bool HideFullRoom = true;
    public Button loginButton;
    // Room Settings
    public Hashtable CustomProperties = new Hashtable() {
        { "GameMode", GameMode.Royale },
        { "GameState", GameState.Preparation },
        { "GameTime", 0f },
        { "Map", 1 },
        { "StartTime", 0f },
        { "StateStartTime", 0f },
        { "PrepTime", 20f },
        { "BrawlTime", 60f },
        { "RoyaleTime", 300f },
        { "PlayersLeft", 0 }
    };

    // Text fields
    public Text connectingText;
    public Text TxtRoomName;
    public Text TxtPlayerCount;
    #endregion

    #region Room Options
    // Access variables for Room Properties.
    // Automatically synced across the network
    public GameMode gameMode
    {
        get { return (GameMode)_room.CustomProperties["GameMode"]; }
        private set { _room.SetCustomProperties(new Hashtable() { { "GameMode", value } }); }
    }
    public GameState gameState
    {
        get { return (GameState)_room.CustomProperties["GameState"]; }
        private set { _room.SetCustomProperties(new Hashtable() { { "GameState", value } }); }
    }
    public float gameTime
    {
        get { return (float)_room.CustomProperties["GameTime"]; }
        private set { _room.SetCustomProperties(new Hashtable() { { "GameTime", value } }); }
    }
    public int Map
    {
        get { return (int)_room.CustomProperties["Map"]; }
        private set { _room.SetCustomProperties(new Hashtable() { { "Map", value } }); }
    }
    public float startTime
    {
        get { return (float)_room.CustomProperties["StartTime"]; }
        private set { _room.SetCustomProperties(new Hashtable() { { "StartTime", value } }); }
    }
    public float stateStartTime
    {
        get { return (float)_room.CustomProperties["StateStartTime"]; }
        private set { _room.SetCustomProperties(new Hashtable() { { "stateStartTime", value } }); }
    }
    public float prepTime
    {
        get { return (float)_room.CustomProperties["PrepTime"]; }
        private set { _room.SetCustomProperties(new Hashtable() { { "prepTime", value } }); }
    }
    public float brawlTime
    {
        get { return (float)_room.CustomProperties["BrawlTime"]; }
        private set { _room.SetCustomProperties(new Hashtable() { { "brawlTime", value } }); }
    }
    public float royaleTime
    {
        get { return (float)_room.CustomProperties["RoyaleTime"]; }
        private set { _room.SetCustomProperties(new Hashtable() { { "royaleTime", value } }); }
    }
    public int playersLeft
    {
        get { return (int)_room.CustomProperties["PlayersLeft"]; }
        private set { _room.SetCustomProperties(new Hashtable() { { "playersLeft", value } }); }
    }
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
        // Set room options
        RoomOptions roomOptions = new RoomOptions() {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = byte.Parse(TxtPlayerCount.text)
        };
        roomOptions.CustomRoomProperties = CustomProperties;
        string roomName = TxtRoomName.text;
        Debug.Log("Room: " + roomName);
        if (PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default))
        {
            Debug.Log("Room: " + roomName + " created!");
        }
        else
        {
            Debug.Log("Failed to create room!");
            ChangeState(LobbyState.LOBBY);
        }
        ChangeState(LobbyState.ROOM);
    }
    public void StartGame()
    {
        if (!PhotonNetwork.isMasterClient)
            return;
        photonView.RPC("RPC_ChangeLevel", PhotonTargets.MasterClient);
        ChangeState(LobbyState.GAME);
    }

    public void StartDeathmatch()
    {
        CustomProperties = new Hashtable() {
        { "GameMode", GameMode.Brawl },
        { "GameState", GameState.Preparation },
        { "GameTime", 0f },
        { "Map", 1 },
        { "StartTime", 0f },
        { "StateStartTime", 0f },
        { "PrepTime", 10f },
        { "BrawlTime", 300f },
        { "RoyaleTime", 0f },
        { "PlayersLeft", 0 }
        };
        PhotonNetwork.room.SetCustomProperties(CustomProperties);
        StartGame();
    }
    public void StartRoyale()
    {
        CustomProperties = new Hashtable() {
        { "GameMode", GameMode.Royale },
        { "GameState", GameState.Preparation },
        { "GameTime", 0f },
        { "Map", 1 },
        { "StartTime", 0f },
        { "StateStartTime", 0f },
        { "PrepTime", 10f },
        { "BrawlTime", 60f },
        { "RoyaleTime", 300f },
        { "PlayersLeft", 0 }
        };
        PhotonNetwork.room.SetCustomProperties(CustomProperties);
        StartGame();
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
    public void OnClickRoomState()
    {
        if (!PhotonNetwork.isMasterClient)
            return;

        PhotonNetwork.room.IsOpen = !PhotonNetwork.room.IsOpen;
        PhotonNetwork.room.IsVisible = PhotonNetwork.room.IsOpen;
    }
    public void OnClick_CreateRoom()
    {
        CreateRoom();
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
        _room = PhotonNetwork.room; 
        ChangeState(LobbyState.ROOM);
        base.OnJoinedRoom();
    }

    private void OnDestroy()
    {
        if(PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.Destroy(photonView);
        }
    }
    #endregion
}

