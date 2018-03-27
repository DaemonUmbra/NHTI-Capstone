using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    #region Private Variables
    [SerializeField]
    private LobbyCanvas _lobbyCanvas;
    [SerializeField]
    private RoomCanvas _roomCanvas;
    [SerializeField]
    private LoginCanvas _loginCanvas;
    [SerializeField]
    private MapCanvas _mapCanvas;

    LobbyState _lobbyState = LobbyState.LOGIN;
    #endregion


    #region Access Variables
    public LobbyCanvas LobbyCanvas { get { return _lobbyCanvas; } }
    public RoomCanvas RoomCanvas { get { return _roomCanvas; } }
    public LoginCanvas LoginCanvas { get { return _loginCanvas; } }
    public MapCanvas MapCanvas { get { return _mapCanvas; } }
    #endregion


    private void Awake()
    {
        // Try to find missing canvas references
        if (!_lobbyCanvas)
        {
            GameObject lobby = GameObject.Find("LobbyCanvas");
            if (lobby) _lobbyCanvas = lobby.GetComponent<LobbyCanvas>();
        }
        if (!_roomCanvas)
        {
            GameObject room = GameObject.Find("RoomCanvas");
            if (room) _roomCanvas = room.GetComponent<RoomCanvas>();
        }
        if (!_loginCanvas)
        {
            GameObject login = GameObject.Find("LoginCanvas");
            if (login) _loginCanvas = login.GetComponent<LoginCanvas>();
        }
        if (!_mapCanvas)
        {
            GameObject map = GameObject.Find("MapCanvas");
            if (map) _mapCanvas = map.GetComponent<MapCanvas>();
        }
    }

    public void ChangeLobbyState(LobbyState newState)
    {
        switch (newState)
        {
            // Make login UI canvas visible
            case LobbyState.LOGIN:
                _loginCanvas.transform.SetAsLastSibling();
                _lobbyState = LobbyState.LOGIN;
                break;
            // Make lobby UI canvas visible
            case LobbyState.LOBBY:
                _lobbyCanvas.transform.SetAsLastSibling();
                _lobbyState = LobbyState.LOBBY;
                break;
            // Make Room UI canvas visible
            case LobbyState.ROOM:
                _roomCanvas.transform.SetAsLastSibling();
                _lobbyState = LobbyState.ROOM;
                break;
            case LobbyState.MAP:
                _mapCanvas.transform.SetAsLastSibling();
                _lobbyState = LobbyState.MAP;
                break;
            default:
                Debug.LogError("Invalid lobby state");
                break;
        }
    }

}