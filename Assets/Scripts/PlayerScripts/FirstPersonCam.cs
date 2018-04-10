using UnityEngine;

public class FirstPersonCam : Photon.MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    /// <summary>
    /// Maximum angle the player can look at vertically. 90 means you can look straight up and down.
    /// </summary>
    public float maxVerticalLook = 90;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void Awake()
    {
        if (!photonView.isMine)
            enabled = false;
    }

    private void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        if(pitch < -maxVerticalLook)
        {
            pitch = -maxVerticalLook;
        }
        else if(pitch > maxVerticalLook)
        {
            pitch = maxVerticalLook;
        }
        transform.eulerAngles = new Vector3(Mathf.Clamp(pitch, -maxVerticalLook, maxVerticalLook), yaw, 0.0f);
        
    }
}