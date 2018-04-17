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
        transform.position += transform.forward * _input.y * pStats.WalkSpeed * Time.deltaTime;
        transform.position += transform.right * _input.x * pStats.WalkSpeed * Time.deltaTime;
        
    }

    public void SetInput(Vector3 inputVec)
    {
        _input = inputVec;
    }

    public void Jump()
    {
        yMove = true;
        //Debug.Log("Jump!");
        Vector3 inverseJump = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.velocity = inverseJump;
        //rb.constraints = RigidbodyConstraints.None;
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