using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerMotor : Photon.MonoBehaviour
{
    private bool onJumpPad = false;

    private Rigidbody rb;
    private PlayerStats pStats;
    private Vector2 _inputVelocity = Vector2.zero;
    public float JumpMultiplier = 1f;
    
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
        Vector3 newVel = new Vector3();

        // Move horizontally only if current horizontal velocity is less than your input velocity
        if(Mathf.Abs( rb.velocity.x) < Mathf.Abs(_inputVelocity.x))
        {
            newVel.x = _inputVelocity.x;
        }
        

    }

    public void SetInputVelocity(Vector2 inputVec)
    {
        // Prevent diagonal speed increase
        if(inputVec.magnitude > 1)
        {
            inputVec.Normalize();
        }

        // Set local velocity
        _inputVelocity = inputVec * pStats.WalkSpeed;
    }

    public void Jump()
    {
        
        if (onJumpPad)
        {
            JumpMultiplier = 2f;
            onJumpPad = false;
        }
        //Debug.Log("Jump!");
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