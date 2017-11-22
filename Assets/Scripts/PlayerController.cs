using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(AbilityManager))]
[RequireComponent(typeof(PlayerShoot))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    GameObject playerFloor;
    [SerializeField]
    float jumpCooldown;
    float lastJumpTime;

    [SerializeField]
    int maxJumpCount;

    int jumpCount;


    // Local components
    PlayerMotor motor;
    PlayerShoot pShoot;
    //AbilityManager abilityManager;

    bool isGrounded;
    
	void Start () {
        motor = GetComponent<PlayerMotor>();
        pShoot = GetComponent<PlayerShoot>();
        lastJumpTime = Time.time - jumpCooldown;
        
        if (playerFloor == null)
            playerFloor = GameObject.Find("Floor");
	}
	
	void Update () {
        // Get movement input
        Vector3 velocity = Vector3.zero;
        velocity.x = Input.GetAxis("Horizontal") * transform.right.x;
        velocity.z = Input.GetAxis("Vertical") * transform.forward.z;

        motor.SetVelocity(velocity); // Apply velocity

        // Check for jump
        //float jump = Input.GetAxis("Jump");
        //if (jump != 0)
        //{
        //    TryJump();
        //}
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
        //if (Time.time-lastJumpTime >= jumpCooldown && isGrounded)
        if (jumpCount < maxJumpCount && Time.time - lastJumpTime >= jumpCooldown)
        {
            Debug.Log("jump!");
            lastJumpTime = Time.time;
            motor.Jump();
            jumpCount++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == playerFloor)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject == playerFloor)
        {
            isGrounded = false;
        }
    }
    
}
