using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerMotor : Photon.MonoBehaviour
{
    private bool onJumpPad = false;

    private Rigidbody rb;
    private PlayerStats pStats;
    private Vector2 _input = Vector3.zero;
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
    private void Update()
    {
<<<<<<< HEAD
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
=======
        Debug.Log(transform.forward);
        transform.position += transform.forward * _input.y * pStats.WalkSpeed * Time.deltaTime;
        transform.position += transform.right * _input.x * pStats.WalkSpeed * Time.deltaTime;
>>>>>>> de78ae266cfbd7b6d0de7ad6f49d276b0dd453b2
    }

    public void SetInput(Vector3 inputVec, bool allowYMovement)
    {
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