using UnityEngine;

public class FirstPersonCam : Photon.MonoBehaviour
{
    [SerializeField]
    Camera playerCam;

    /// <summary>
    /// Maximum angle the player can look at vertically. 90 means you can look straight up and down.
    /// </summary>
    public float maxVerticalLook = 90;

    private void Awake()
    {
        if (!photonView.isMine)
            enabled = false;
    }

    private void Update()
    {
    }
}