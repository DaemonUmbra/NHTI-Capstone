using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(AbilityManager))]
[RequireComponent(typeof(PlayerShoot))]
[RequireComponent(typeof(AudioManager))]
public class PlayerController : Photon.MonoBehaviour
{
    // Serialization stuff
    Vector3 position;
    Quaternion rotation;

    // Crowd control
    private bool CrowdControlled = false;
    private float CCStartTime, duration;
    // Layers
    [SerializeField]
    private int groundLayer = 8;

    // Counts and cooldowns
    [SerializeField]
    private float jumpCooldown;
    private float lastJumpTime;
    public int maxJumpCount = 1;
    private int jumpCount = 0;

    // Local components
    private PlayerMotor motor;
    private PlayerShoot pShoot;
    private AbilityManager abilityManager;
    private CameraController camController;

    // Flags
    public bool canWallJump = false;
    private bool onRamp = false;
    private bool isGrounded = false;
    private bool debounce = false;
    
    // Controls
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
    // Movement input axes names
    string AxisHorizLook = "Mouse X";
    string AxisVerticalLook = "Mouse Y";
    // Controller type
    ControlType cType = ControlType.KeyboardMouse;
    string ps4Name = "Wireless Controller";
    string xboxName = "Controller (Xbox One For Windows)";

    /// <summary>
    /// Horizontal look speed in degrees/second
    /// </summary>
    public float hLookSpeed = 20;
    /// <summary>
    /// Vertical look speed in degrees/second
    /// </summary>
    public float vLookSpeed = 20;
    /// <summary>
    /// Maximum vertical look angle in degrees. 90 means you can look completely up and down.
    /// </summary>
    public float maxVerticalLook = 80;

    float hRot;
    float vRot;

    

    private void Awake()
    {
        motor = GetComponent<PlayerMotor>();
        pShoot = GetComponent<PlayerShoot>();
        abilityManager = GetComponent<AbilityManager>();
        camController = GetComponent<CameraController>();
        lastJumpTime = Time.time - jumpCooldown;
        hRot = transform.rotation.eulerAngles.x;
        vRot = transform.rotation.eulerAngles.y;
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
        // Validates the controls
        CheckControlType();

        // Get movement input
        Vector3 inputVel = Vector3.zero;

        float xAxis = Input.GetAxis("Horizontal");
        if (InvertX) xAxis *= -1;

        float yAxis = Input.GetAxis("Vertical");
        if (InvertY) yAxis *= -1;

        Vector2 input = new Vector2(xAxis, yAxis);

        if (!CrowdControlled)
        {
            motor.SetInput(input); // Apply input
        }
        else
        {
            CCWearOff(Time.time, duration, false);
            return;
        }

        // Check for jump
        if (Input.GetButtonDown("Jump"))
        {
            photonView.RPC("TryJump", PhotonTargets.All);
        }
        // Check for shooting
        if (Input.GetButtonDown("Fire1"))
        {
            photonView.RPC("RPC_FirePrimary", PhotonTargets.All);
        }

        // Check active ability input
        HandleAbilityInput();
        // Check camera look input
        HandleLookInput();
    }

    private void CheckControlType()
    {
        string[] joysticks = Input.GetJoystickNames();

        if(joysticks.Length > 0)
        {
            foreach (string j in joysticks)
            {
                if (j == xboxName)
                {
                    cType = ControlType.XBoxOne;
                    AxisHorizLook = "XboxRSHorizontal";
                    AxisVerticalLook = "XboxRSVertical";
                }
                else if (j == ps4Name)
                {
                    cType = ControlType.PS4;
                    AxisHorizLook = "PS4RSHorizontal";
                    AxisVerticalLook = "PS4RSVertical";
                }
            }
            
        }
        else
        {
            cType = ControlType.KeyboardMouse;
            AxisHorizLook = "Mouse X";
            AxisVerticalLook = "Mouse Y";
        }
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
    private void HandleLookInput()
    {
        float vLook = Input.GetAxis(AxisVerticalLook) * vLookSpeed * Time.deltaTime;
        float hLook = Input.GetAxis(AxisHorizLook) * hLookSpeed * Time.deltaTime;

        hRot += hLook;
        vRot -= vLook;

        // Clamp vertical rotation
        if (vRot > maxVerticalLook)
            vRot = maxVerticalLook;
        else if (vRot < -maxVerticalLook)
            vRot = -maxVerticalLook;

        // Rotate player and set camera rotation
        transform.rotation = Quaternion.Euler(0, hRot, 0);
        camController.camRotation = Quaternion.Euler(vRot, hRot, 0);
        
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
        //motor.SetInput(Vector3.zero);
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
    [PunRPC]
    public void RPC_KnockBack(Vector3 direction, float force, Vector3 velocityMultiplier)
    {
        direction = direction.normalized;
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        Vector3 vel = new Vector3(direction.x * velocityMultiplier.x, direction.y * velocityMultiplier.y, direction.z * velocityMultiplier.z);

        rb.AddForce(vel * force, ForceMode.Impulse);
    }
    public void ApplyKnockBack(Vector3 dir, float force, Vector3 mult)
    {
        photonView.RPC("RPC_KnockBack", PhotonTargets.All, dir, 20f, mult);
    }
    public void ApplyCrowdControl(float start, float duration)
    {

    }
    private void GroundCheck()
    {
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        RaycastHit land;
        if (Physics.Raycast(transform.position, dwn, out land))
        {
            if (land.distance < 0.3f)
            {
                isGrounded = true;
                jumpCount = 0;
            }
        }
    }
    private void OverHeadCheck(GameObject hitPart)
    {
        
        Vector3 dir = transform.TransformDirection(Vector3.up);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit))
        {
            if (hit.transform.gameObject != null)
            {
                if (hit.transform.gameObject != hitPart)
                {
                    isGrounded = true;
                    jumpCount = 0;
                }
            }
        }
        else
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //print("Collided with Object on layer: " + collision.gameObject.layer.ToString());
        if (collision.collider.gameObject.layer == groundLayer)
        {

            if (canWallJump == true)
            {
                GameObject groundHit = collision.collider.gameObject;
                OverHeadCheck(groundHit);
            }
            else
            {
                GroundCheck();
            }
            //print("Jump Reset");
        }
        if (collision.gameObject.tag == "Ramp")
        {
            Debug.Log("onramp");
            onRamp = true;
        }
        else
        {
            onRamp = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == groundLayer)
        {
            isGrounded = false;
        }
        onRamp = false;
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


public enum ControlType { KeyboardMouse, XBoxOne, PS4 }