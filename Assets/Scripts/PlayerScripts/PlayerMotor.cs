using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerMotor : Photon.MonoBehaviour
{
    private bool onJumpPad = false;

    private Rigidbody rb;
    private PlayerStats pStats;
    private Vector3 _input = Vector3.zero;
    public float JumpMultiplier = 1f;

    private float _acceleration;
    private float _deceleration;

    // Use this for initialization

    private void Awake()
    {
        if (!photonView.isMine)
        {
            enabled = false;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pStats = GetComponent<PlayerStats>();
    }

    // Runs every physics update frame
    private void FixedUpdate()
    {
        Vector3 currentVel = rb.velocity;
        Vector3 newVelocity = Vector3.zero;

        if(_input.x > 0)
        {
            // Needs to be implemented again
        }
    }

    public void SetInput(Vector3 inputVec)
    {
        // Prevent diagonal speed increase
        if(inputVec.magnitude > 1)
        {
            inputVec.Normalize();
        }

        // Set local velocity
        _input = inputVec;
    }

    public void Jump()
    {
        //Debug.Log("Jump!");
        Vector3 inverseJump = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.velocity = inverseJump;
        rb.AddForce(Vector3.up * pStats.JumpPower * JumpMultiplier);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "JumpPad")
        {
            JumpMultiplier = 2f;
            onJumpPad = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "JumpPad")
        {
            JumpMultiplier = 1f;
            onJumpPad = false;
        }
    }
}