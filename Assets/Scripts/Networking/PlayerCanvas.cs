using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour {
    public Text player;
    // Use this for initialization

    // Update is called once per fram

    public void SetPlayerName()
    {

        PhotonNetwork.playerName = player.text;
        MainCanvasManager.Instance.PlayerCanvas.transform.SetAsFirstSibling();
    }
}
