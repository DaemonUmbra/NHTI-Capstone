﻿using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(AbilityManager))]
[RequireComponent(typeof(PlayerShoot))]
public class PlayerController : Photon.PunBehaviour
{
    Vector3 position;
    Quaternion rotation;

    private bool CrowdControlled = false;
    private float CCStartTime, duration;

    [SerializeField]
    private int groundLayer = 8;

    [SerializeField]
    private float jumpCooldown;

    private float lastJumpTime;

    public int maxJumpCount = 1;

    private int jumpCount = 0;

    // Local components
    private PlayerMotor motor;

    private PlayerShoot pShoot;
    //AbilityManager abilityManager;

    private bool isGrounded = false;
    private bool debounce = false;

    /// <summary>
    /// Inverted movement controls
    /// </summary>
    public bool InvertX = false;

    public bool InvertY = false;

    private void Awake()
    {
        
    }

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        pShoot = GetComponent<PlayerShoot>();
        lastJumpTime = Time.time - jumpCooldown;
    }

    private void Update()
    {
        // Check input only on owner client
        if (photonView.isMine)
        {
            // Get movement input
            Vector3 inputVel = Vector3.zero;

            float xAxis = Input.GetAxis("Horizontal");
            Vector3 xInput = xAxis * transform.right;
            if (InvertX) xInput *= -1;

            float yAxis = Input.GetAxis("Vertical");
            Vector3 yInput = yAxis * transform.forward;
            if (InvertY) yInput *= -1;

            inputVel = xInput + yInput;

            if (!CrowdControlled)
            {
                motor.SetVelocity(inputVel); // Apply velocity
            }
            else
            {
                CCWearOff(Time.time, duration, false);
                return;
            }

            // Check for jump}
            if (Input.GetKeyDown(KeyCode.Space))
            {
                photonView.RPC("TryJump", PhotonTargets.All);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                StopMomentum();
            }
            // Check for shooting
            if (Input.GetButtonDown("Fire1"))
            {
                photonView.RPC("RPC_FirePrimary", PhotonTargets.All);
            }
        }
    }

    [PunRPC]
    private void RPC_FirePrimary()
    {
        //HACK
        pShoot = gameObject.GetComponent<PlayerShoot>();
        pShoot.shoot.Invoke();
    }

    private void StopMomentum()
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
    [PunRPC]
    private void TryJump()
    {
        Debug.Log("tryjump! " + maxJumpCount);
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
        //print("Collided with Object on layer: " + collision.gameObject.layer.ToString());
        if (collision.collider.gameObject.layer == groundLayer)
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
        if (collision.collider.gameObject.layer == groundLayer)
        {
            isGrounded = false;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(transform.localScale);
        }
        else
        {
            
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();

        }
    }
}