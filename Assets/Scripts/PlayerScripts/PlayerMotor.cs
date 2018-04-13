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
    private bool yMove = false;

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
        _input.y = 0;
        Vector3 moveVector = transform.position + _input * Time.deltaTime * pStats.WalkSpeed;
        if (!yMove)
        {
            //moveVector.y = rb.velocity.y;
        }
        rb.MovePosition(moveVector);
    }

    public void SetInput(Vector3 inputVec, bool allowYMovement)
    {
        // Prevent diagonal speed increase
        if(inputVec.magnitude > 1)
        {
            inputVec.Normalize();
        }

        // Set local velocity
        //yMove = allowYMovement;
        _input = inputVec;
    }

    public void Jump()
    {
        yMove = true;
        //Debug.Log("Jump!");
        Vector3 inverseJump = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.velocity = inverseJump;
        rb.AddForce(Vector3.up * pStats.JumpPower * JumpMultiplier);
    }
    private void WallCheck()
    {
        Vector3 dir = transform.TransformDirection(Vector3.forward);
        RaycastHit obj;
        if (Physics.Raycast(transform.position, dir, out obj))
        {
            if (obj.transform.gameObject.tag != "Ramp")
            {

            }
        }
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