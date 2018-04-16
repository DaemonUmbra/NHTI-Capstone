using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : Photon.PunBehaviour {

    public GameObject optionsCanvas;
    public string [] Names;
    public int size;

    public void Update()
    {
        Names = Input.GetJoystickNames();
        size = Names.Length;


    if (Input.GetButtonDown("Cancel"))
    {
            if (optionsCanvas.active)
            {
                optionsCanvas.SetActive(false);
            }
            else
            {
                    optionsCanvas.SetActive(true);
            }
    }

        if (optionsCanvas.active)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Resume()
    {
        optionsCanvas.SetActive(false);
    }

    public void Lobby()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void Quit()
    {
        Application.Quit();
    }
    public override void OnLeftRoom()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in players)
        {
            if (p.GetPhotonView().isMine)
            {
                PhotonNetwork.Destroy(p);
            }
        }
    
        PhotonNetwork.LoadLevel(0);
        base.OnLeftLobby();
    }
}
