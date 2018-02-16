using UnityEngine;

public class LeaveCurrentMatch : MonoBehaviour
{
    //Finished
    public void OnClick_LeaveMatch()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
}