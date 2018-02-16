using UnityEngine;

namespace PUNTutorial
{
    public class Player : Photon.PunBehaviour
    {
        private Camera playerCam;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            playerCam = GetComponentInChildren<Camera>();

            if (!photonView.isMine)
            {
                playerCam.gameObject.SetActive(false);
            }
        }
    }
}