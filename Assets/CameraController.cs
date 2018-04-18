using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Photon.MonoBehaviour {

    public Camera camPrefab;
    [HideInInspector]
    public Camera cam;
    public Vector3 camOffset;
    public Quaternion camRotation;

    Transform following;

	// Use this for initialization
	void Awake () {
		// Load camera prefab
        if(photonView.isMine)
        {
            cam = Instantiate(camPrefab);
        }
        FollowTransform(transform);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (cam)
        {
            // Follow player
            cam.transform.position = following.position
                + following.right * camOffset.x
                + following.up * camOffset.y
                - following.forward * camOffset.z;
            // Rotate cam
            cam.transform.rotation = camRotation;
        }
	}

    public void FollowTransform(Transform follow)
    {
        following = follow;
    }
}
