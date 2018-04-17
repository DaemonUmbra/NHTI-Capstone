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
    private Rigidbody rig;

    // Flags
    public bool canWallJump = false;
    private bool onRamp = false;
    private bool isGrounded = false;
    private bool debounce = false;
<<<<<<< HEAD
    private bool OnWall = false;
    private char wallDir;
    private GameObject wall;
=======
>>>>>>> 7269adcff004d2163ecd2da9704f56cea9bb6df3
    
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
    // Controller input axes and type
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

    private float mLookSpeed = 20;
    private float jLookSpeed = 100;

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
        rig = transform.GetComponent<Rigidbody>();
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

        rig.constraints = RigidbodyConstraints.None;

        Vector2 input = new Vector2(xAxis, yAxis);
        if (input == Vector2.zero)
        {
            if(isGrounded && onRamp)
            {
                
                rig.constraints = RigidbodyConstraints.FreezePosition;
            }
        }
        if (!OnWall)
        {
            if (!CrowdControlled)
            {
                motor.SetInput(input); // Apply input
            }
            else
            {
                motor.SetInput(Vector3.zero);
            }
        }
        else
        {
<<<<<<< HEAD
            if (!CrowdControlled)
            {
                //Debug.Log("Direction of wall: " + wallDir);
                //motor.SetInput(Vector3.zero);
                OnWall = WallCheck(wall);
                switch (wallDir)
                {
                    case 'f':
                        //Debug.Log(input.y);
                        if (input.y > 0)
                        {
                            input.y = 0;
                        }
                        break;
                    case 'b':
                        if (input.y < 0)
                        {
                            input.y = 0;
                        }
                        break;
                    case 'r':
                        if (input.x > 0)
                        {
                            input.x = 0;
                        }
                        break;
                    case 'l':
                        if (input.x < 0)
                        {
                            input.x = 0;
                        }
                        break;
                }
                motor.SetInput(input);
            }
            else
            {
                motor.SetInput(Vector3.zero);
            }       
=======
            CCWearOff(Time.time, duration, false);
            return;
>>>>>>> 7269adcff004d2163ecd2da9704f56cea9bb6df3
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
                    hLookSpeed = jLookSpeed;
                    vLookSpeed = jLookSpeed;
                }
                else if (j == ps4Name)
                {
                    cType = ControlType.PS4;
                    AxisHorizLook = "PS4RSHorizontal";
                    AxisVerticalLook = "PS4RSVertical";
                    hLookSpeed = jLookSpeed;
                    vLookSpeed = jLookSpeed;
                }
            }
            
        }
        else
        {
            cType = ControlType.KeyboardMouse;
            AxisHorizLook = "Mouse X";
            AxisVerticalLook = "Mouse Y";
            hLookSpeed = mLookSpeed;
            vLookSpeed = mLookSpeed;
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
        Debug.Log("Horizontal Axis: " + Input.GetAxis(AxisVerticalLook));
        Debug.Log("Vertical: " + Input.GetAxis(AxisHorizLook));
        float vLook = Input.GetAxis(AxisVerticalLook) * vLookSpeed * Time.deltaTime;
        float hLook = Input.GetAxis(AxisHorizLook) * hLookSpeed * Time.deltaTime;

        hRot += hLook;
        if(cType == ControlType.KeyboardMouse)
        {
            vRot -= vLook;
        }
        else
        {
            vRot += vLook;
        }
        

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
        rig.constraints = RigidbodyConstraints.None;
        onRamp = false;
        if (jumpCount < maxJumpCount)
        {
            //Debug.Log("jump!");
            lastJumpTime = Time.time;
            isGrounded = false;
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
<<<<<<< HEAD
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(vel, ForceMode.Impulse);
=======

        rb.AddForce(vel * force, ForceMode.Impulse);
>>>>>>> 7269adcff004d2163ecd2da9704f56cea9bb6df3
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
<<<<<<< HEAD
    private bool WallCheck(GameObject hitObj)
    {
        if (hitObj == null)
        {
            return false;
        }
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 bk = transform.TransformDirection(Vector3.back);
        Vector3 rgt = transform.TransformDirection(Vector3.right);
        Vector3 lft = transform.TransformDirection(Vector3.left);
        float dist = 2;
        RaycastHit ter;
        if (Physics.Raycast(transform.position, fwd, out ter, dist))
        {
            if (ter.transform.gameObject == hitObj && ter.transform.gameObject.tag != "Ramp")
            {
                wallDir = 'f';
                wall = hitObj;
                return true;
            }
        }
        if (Physics.Raycast(transform.position, bk, out ter, dist))
        {
            if (ter.transform.gameObject == hitObj && ter.transform.gameObject.tag != "Ramp")
            {
                wallDir = 'b';
                wall = hitObj;
                return true;
            }
        }
        if (Physics.Raycast(transform.position, rgt, out ter, dist))
        {
            if (ter.transform.gameObject == hitObj && ter.transform.gameObject.tag != "Ramp")
            {
                wallDir = 'r';
                wall = hitObj;
                return true;
            }
        }
        if (Physics.Raycast(transform.position, lft, out ter, dist))
        {
            if (ter.transform.gameObject == hitObj && ter.transform.gameObject.tag != "Ramp")
            {
                wallDir = 'l';
                wall = hitObj;
                return true;
            }
        }
        return false;
    }
=======
>>>>>>> 7269adcff004d2163ecd2da9704f56cea9bb6df3
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
        GameObject hit = collision.gameObject;
        //print("Collided with Object on layer: " + collision.gameObject.layer.ToString());
<<<<<<< HEAD
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        CrowdControlled = false;
        OnWall = WallCheck(hit);
        if (collision.gameObject.layer == groundLayer)
=======
        if (collision.collider.gameObject.layer == groundLayer)
>>>>>>> 7269adcff004d2163ecd2da9704f56cea9bb6df3
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
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ramp")
        {
            //Debug.Log("onramp");
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
        OnWall = false;
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