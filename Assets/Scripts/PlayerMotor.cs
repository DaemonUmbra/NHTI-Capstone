using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float jumpPower = 10f;

    private Rigidbody rb;
    private Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    // Runs every physics update frame
    private void FixedUpdate()
    {
        // Change rb velocity to local velocity
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    public void SetVelocity(Vector3 _velocity)
    {
        // Set local velocity
        velocity = _velocity * speed;
    }

    public void Jump()
    {
        Debug.Log("Jump!");
        rb.AddForce(Vector3.up * jumpPower);
    }
}
