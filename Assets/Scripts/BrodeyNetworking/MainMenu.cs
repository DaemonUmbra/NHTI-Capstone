using UnityEngine;
using UnityEngine.UI;

namespace PUNTutorial
{
    public class MainMenu : Photon.PunBehaviour
    {
        private static MainMenu instance;

        private GameObject ui;
        private Button joinGameButton;

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

        private void OnLevelWasLoaded(int level)
        {
            ui.SetActive(!PhotonNetwork.inRoom);
        }
    }
}