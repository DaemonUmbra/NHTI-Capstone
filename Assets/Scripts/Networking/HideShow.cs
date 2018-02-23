using UnityEngine;
using UnityEngine.UI;

public class HideShow : MonoBehaviour
{
    public Text buttonText;

    // Use this for initialization
    private void Start()
    {
    }

    public void ChangeLobbyStatus()
    {
        LobbyManager.HideFullRoom = !LobbyManager.HideFullRoom;

        if (LobbyManager.HideFullRoom)
        {
            buttonText.text = "Show\nFull\nRooms";
        }
        else
        {
            buttonText.text = "Hide\nFull\nRooms";
        }
    }
}