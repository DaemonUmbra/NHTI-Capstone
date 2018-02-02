using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PUNTutorial;

public class DDOL : MonoBehaviour
{
    public GameObject networkOBJ;

    //Finished
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        
        
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        Debug.Log(sceneName);
        if (sceneName == "BrodeyJoinScene")
        {
            PhotonNetwork.LoadLevel("Sandbox" + "");
            //networkOBJ.SetActive(true);
            //networkOBJ.GetComponent<GameManager>().JoinGame();
        } else {
            //networkOBJ.SetActive(false);
        }
    }
}
