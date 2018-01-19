using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(AbilityManager))]
[RequireComponent(typeof(PlayerShoot))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    int groundLayer;
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
    
	void Start () {
        motor = GetComponent<PlayerMotor>();
        pShoot = GetComponent<PlayerShoot>();
        lastJumpTime = Time.time - jumpCooldown;
	}
	
	void Update () {
        // Get movement input
        Vector3 velocity = Vector3.zero;
        velocity.x = Input.GetAxis("Horizontal") * transform.right.x;
        velocity.z = Input.GetAxis("Vertical") * transform.forward.z;

        motor.SetVelocity(velocity); // Apply velocity

        // Check for jump}
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }

        // Check for shooting
        if (Input.GetButtonDown("Fire1"))
        {
            if(pShoot.shoot != null)
                pShoot.shoot();
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
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.gameObject.layer == groundLayer)
        {
            isGrounded = false;
        }
    }
    
}
