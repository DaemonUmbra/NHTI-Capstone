using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(AbilityManager))]
[RequireComponent(typeof(PlayerShoot))]
public class PlayerController : Photon.MonoBehaviour
{
    private bool CrowdControlled = false;
    private float CCStartTime, duration;

    [SerializeField]
    private int groundLayer = 8;
    [SerializeField]
    float jumpCooldown;
    float lastJumpTime;

    [SerializeField]
    public int maxJumpCount;
    
    int jumpCount = 0;
    
    // Local components
    PlayerMotor motor;
    PlayerShoot pShoot;
    //AbilityManager abilityManager;

    bool isGrounded = false;
    bool debounce = false;

<<<<<<< HEAD
    

=======
    private void Awake()
    {
        if (!photonView.isMine)
        {
            enabled = false;
        }
    }
>>>>>>> aa76c525e830c3fa2e734e3732183ec39859aef7
    void Start ()
    {
        motor = GetComponent<PlayerMotor>();
        pShoot = GetComponent<PlayerShoot>();
        lastJumpTime = Time.time - jumpCooldown;
	}
    
    void Update () {
        // Get movement input
        Vector3 velocity = Vector3.zero;
        velocity = Input.GetAxis("Horizontal") * transform.right;
        velocity += Input.GetAxis("Vertical") * transform.forward;
        if (!CrowdControlled)
        {
            motor.SetVelocity(velocity); // Apply velocity
        }
        else
        {
            CCWearOff(Time.time, duration, false);
            return;
        }

        // Check for jump}
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            StopMomentum();
        }
        // Check for shooting
        if (Input.GetButtonDown("Fire1"))
        {

            pShoot.shoot.Invoke();
        }
    }
    void StopMomentum()
    {
        motor.SetVelocity(Vector3.zero);
    }
    private void CCWearOff(float currentTime, float CCDuration, bool stopsMomentum)
    {
        //Debug.Log("Crowd Control started: " + CCStartTime + " Current Time: " + currentTime);
        if (stopsMomentum)
        {
            StopMomentum();
        }
        if (currentTime >= CCStartTime + CCDuration)//static value
        {
            CrowdControlled = false;
        }
    }
    

    private void TryJump()
    {
        Debug.Log("tryjump!");
        if (jumpCount < maxJumpCount)
        {
            Debug.Log("jump!");
            lastJumpTime = Time.time;
            motor.Jump();
            jumpCount++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Collided with Object on layer: " + collision.gameObject.layer.ToString());
        if(collision.collider.gameObject.layer == groundLayer)
        {
            isGrounded = true;
            jumpCount = 0;
            print("Jump Reset");
        }
        if (collision.gameObject.tag == "SlimeBall")
        {
            CCStartTime = Time.time;
            duration = 1f;
            CrowdControlled = true;
            CCWearOff(Time.time, duration, true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.gameObject.layer == groundLayer)
        {
            isGrounded = false;
        }
    }
    

}
