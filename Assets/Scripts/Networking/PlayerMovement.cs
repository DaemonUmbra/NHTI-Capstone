using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //Possibly needs edit
    private PhotonView PhotonView;
    private Vector3 TargetPosition;
    private Quaternion TargetRotation;
    public float Health;
    public bool UseTransformView = true;
    private Animator m_animator;

	// Use this for initialization
	private void Awake () {
        m_animator = GetComponent<Animator>();
        PhotonView = GetComponent<PhotonView>();
	}

    private void Update()
    {
        if (PhotonView.isMine)
        {
            CheckInput();
        } else {
            SmoothMove();
        }
    }

    private void SmoothMove()
    {
        if (UseTransformView) return;

        transform.position = Vector3.Lerp(transform.position, TargetPosition, 0.25f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, 500 * Time.deltaTime);
    }

    private void CheckInput()
    {
        float moveSpeed = 0.1f;
        float rotateSpeed = 500f;

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.position += transform.forward * (vertical * moveSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, horizontal * rotateSpeed * Time.deltaTime, 0));

        m_animator.SetFloat("Input", vertical);

        if (Input.GetKeyDown(KeyCode.Space)) PhotonView.RPC("RPC_PerformTaunt", PhotonTargets.All);
    }

    [PunRPC]
    private void RPC_PerformTaunt()
    {
        m_animator.SetTrigger("Taunt");
    }

}
