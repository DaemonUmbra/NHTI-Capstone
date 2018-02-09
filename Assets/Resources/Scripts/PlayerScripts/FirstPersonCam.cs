﻿using UnityEngine;
using System.Collections;

public class FirstPersonCam : Photon.MonoBehaviour
{

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void Awake()
    {
        if (!photonView.isMine)
            enabled = false;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(Mathf.Clamp(pitch, -30f, 30f), yaw, 0.0f);
    }
}
