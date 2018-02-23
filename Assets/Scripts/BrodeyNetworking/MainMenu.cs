using UnityEngine;
using UnityEngine.UI;

namespace PUNTutorial
{
    public class MainMenu : Photon.PunBehaviour
    {
        private static MainMenu instance;

        private GameObject ui;
        private Button joinGameButton;

        // This function is called when the object becomes enabled and active
        private void OnEnable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManager_sceneLoaded; ;
        }

        private void SceneManager_sceneLoaded(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.LoadSceneMode arg1)
        {
            ui.SetActive(!PhotonNetwork.inRoom);
        }

        private void Awake()
        {
            if (instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            instance = this;

            ui = transform.FindAnyChild<Transform>("UI").gameObject;
            joinGameButton = transform.FindAnyChild<Button>("JoinGameButton");

            ui.SetActive(true);
            joinGameButton.interactable = false;
        }

        public override void OnConnectedToMaster()
        {
            joinGameButton.interactable = true;
        }

        //Replaced with eventhandler
        /*
        private void OnLevelWasLoaded(int level)
        {
            ui.SetActive(!PhotonNetwork.inRoom);
        }
        */
    }
}