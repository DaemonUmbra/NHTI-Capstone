using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
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
    private void Start()
    {

    }

    private void Update()
    {
        status = Updated;
        playerCount.text = currentPlayersCount + "/" + maxPlayersCount;
    }

    private void OnDestroy()
    {
        Button button = GetComponent<Button>();
    }
    public void OnJoinClick()
    {
        PhotonNetwork.JoinRoom(_roomNameText.text);
    }
    public void SetRoomNameText(string text)
    {
        RoomName = text;
        RoomNameText.text = RoomName;
    }
}