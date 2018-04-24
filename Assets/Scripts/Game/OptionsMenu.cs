using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {

    public GameObject optionsCanvas;
    public string [] Names;
    public int size;

    public void Update()
    {
        Names = Input.GetJoystickNames();
        size = Names.Length;


        if (Input.GetButtonDown("Cancel"))
        {
            if (optionsCanvas.GetActive())
            {
                optionsCanvas.SetActive(false);
            }
            else
            {
                    optionsCanvas.SetActive(true);
            }
        }

        if (optionsCanvas.GetActive())
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
        SceneManager.LoadScene("LobbiesMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
