using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(AbilityManager))]
[RequireComponent(typeof(PlayerShoot))]
[RequireComponent(typeof(AudioManager))]
public class PlayerController : Photon.MonoBehaviour
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
    private AbilityManager abilityManager;

    private bool isGrounded = false;
    private bool debounce = false;

    /// <summary>
    /// Invert left and right controls
    /// </summary>
    public bool InvertX = false;
    /// <summary>
    /// Invert forward and backward controls
    /// </summary>
    public bool InvertY = false;

    // Ability input axes names
    string AxisA1 = "ActiveOne";
    string AxisA2 = "ActiveTwo";
    string AxisA3 = "ActiveThree";
    string AxisA4 = "ActiveFour";

    private void Awake()
    {
        motor = GetComponent<PlayerMotor>();
        pShoot = GetComponent<PlayerShoot>();
        abilityManager = GetComponent<AbilityManager>();
        lastJumpTime = Time.time - jumpCooldown;
    }
    

    private void Update()
    {
        // Check input only on owner client
        if (photonView.isMine)
        {
            HandleInput();
        }
    }
    private void HandleInput()
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

        // Check active ability input
        HandleAbilityInput();
    }

    private void HandleAbilityInput()
    {
        if(Input.GetButtonDown(AxisA1))
        {
            abilityManager.TriggerAbility(0);
        }
        if (Input.GetButtonDown(AxisA2))
        {
            abilityManager.TriggerAbility(1);
        }
        if (Input.GetButtonDown(AxisA3))
        {
            abilityManager.TriggerAbility(2);
        }
        if (Input.GetButtonDown(AxisA4))
        {
            abilityManager.TriggerAbility(3);
        }
    }
    [PunRPC]
    private void RPC_FirePrimary()
    {
        //HACK
        pShoot = gameObject.GetComponent<PlayerShoot>();
        pShoot.shoot.Invoke();
    }

    public void StopMomentum()
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
        //Debug.Log("tryjump! " + maxJumpCount);
        if (jumpCount < maxJumpCount)
        {
            //Debug.Log("jump!");
            lastJumpTime = Time.time;
            motor.Jump();
            jumpCount++;
        }
    }
    private void GroundCheck()
    {
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        RaycastHit land;
        if (Physics.Raycast(transform.position, dwn, out land))
        {
            if (land.distance < .3f)
            {
                isGrounded = true;
                jumpCount = 0;
            }
            Debug.Log(land.transform.gameObject.name);
            Debug.Log(land.distance);
        }
    }
    private void OverHeadCheck()
    {
        Vector3 dir = transform.TransformDirection(Vector3.up);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit))
        {
            if (hit.distance < .3f)
            {
                isGrounded = true;
                jumpCount = 0;
            }
            Debug.Log(hit.transform.gameObject.name);
            Debug.Log(hit.distance);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //print("Collided with Object on layer: " + collision.gameObject.layer.ToString());
        if (collision.collider.gameObject.layer == groundLayer)
        {
            GroundCheck();
            //print("Jump Reset");
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