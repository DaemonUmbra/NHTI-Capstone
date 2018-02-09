using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerMotor : Photon.MonoBehaviour {

    
    bool onJumpPad = false;

    private Rigidbody rb;
    private PlayerStats pStats;
    private Vector3 _velocity = Vector3.zero;

    // Use this for initialization

    private void Awake()
    {
        if (!photonView.isMine)
        {
            enabled = false;
        }
    }

    void Start () {
        rb = GetComponent<Rigidbody>();
        pStats = GetComponent<PlayerStats>();
    }

    // Runs every physics update frame
    private void FixedUpdate()
    {
        // Change rb velocity to local velocity
        _velocity.y = rb.velocity.y;
        rb.velocity = _velocity;

    }

    public void SetVelocity(Vector3 velocity)
    {
        // Set local velocity
        _velocity = velocity * pStats.WalkSpeed;
    }

    public void Jump()
    {
        float JumpMultiplier = 1f;
        if (onJumpPad)
        {
            JumpMultiplier = 2f;
            onJumpPad = false;
        }
        Debug.Log("Jump!");
        Vector3 inverseJump = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.velocity = inverseJump;
        rb.AddForce(Vector3.up * pStats.JumpPower * JumpMultiplier);
    }

    private void OnTriggerEnter(Collider other)
    {
        onJumpPad = false;
        if (other.gameObject.tag == "JumpPad")
        {
            onJumpPad = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "JumpPad")
        {
            onJumpPad = false;
        }
    }
}
