using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    //Finished
    public static MainCanvasManager Instance;

    [SerializeField]
    private LobbyCanvas _lobbyCanvas;

    public LobbyCanvas LobbyCanvas
    {
        get { return _lobbyCanvas; }
    }

    [SerializeField]
    private CurrentRoomCanvas _currentRoomCanvas;

    public CurrentRoomCanvas CurrentRoomCanvas
    {
        get { return _currentRoomCanvas; }
    }

    [SerializeField]
    private PlayerCanvas _playerCanvas;

    public PlayerCanvas PlayerCanvas
    {
        get { return _playerCanvas; }
    }

    private void Awake()
    {
        Instance = this;
    }
}