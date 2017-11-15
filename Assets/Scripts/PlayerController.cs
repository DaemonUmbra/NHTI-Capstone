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
        float jump = Input.GetAxis("Jump");
        if (jump != 0)
            TryJump();

        // Check for shooting
        if (Input.GetButtonDown("Fire1"))
        {
            if(pShoot.shoot != null)
                pShoot.shoot();
        }
            
	}

    private void TryJump()
    {
        if (Time.time-lastJumpTime >= jumpCooldown && isGrounded)
        {
            Debug.Log("jump!");
            lastJumpTime = Time.time;
            motor.Jump();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == playerFloor)
        {
            isGrounded = true;
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
