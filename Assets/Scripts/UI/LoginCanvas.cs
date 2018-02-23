using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginCanvas : MonoBehaviour {
    public Text NameField;

    public void SetPlayerName()
    {
        PhotonNetwork.playerName = NameField.text;
    }
}
