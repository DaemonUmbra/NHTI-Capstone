﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour {

    public GameObject optionsCanvas;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(optionsCanvas.active)
            {
                optionsCanvas.SetActive(false);
            } else {
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
        PhotonNetwork.LoadLevel(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}