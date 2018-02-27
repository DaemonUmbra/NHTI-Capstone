using UnityEngine;

public class LobbyCanvas : MonoBehaviour
{
    //Finished
    [SerializeField]
    private RoomLayoutGroup _roomLayoutGroup;

    public RoomLayoutGroup RoomLayoutGroup
    {
        get { return _roomLayoutGroup; }
    }
}