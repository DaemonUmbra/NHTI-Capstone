using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Photon.MonoBehaviour {

    public Camera camPrefab;
    [HideInInspector]
    public Camera cam;
    public Vector3 camOffset;

	// Use this for initialization
	void Awake () {
		// Load camera prefab
        if(photonView.isMine)
        {
            cam = Instantiate(camPrefab);
        }

	}
	
	// Update is called once per frame
	void Update () {
        // Set the camera positions to the current transform position + the offset
        cam.transform.position = transform.position 
            + transform.right * camOffset.x
            + transform.up * camOffset.y
            - transform.forward * camOffset.z;
	}
}
