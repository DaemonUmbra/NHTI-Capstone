using Powerups;
using UnityEngine;

public class PlayerShoot : Photon.MonoBehaviour
{
    // Shooting delegate
    public delegate void DelShoot();
    public DelShoot delShoot;
    Camera cam;

    public string directory = "Projectiles/";
    public Projectile projectile;

    [SerializeField]
    private Transform shootPoint;
    private Transform DefaultAimPoint;
    public float range = 100;
    
    private void OnEnable()
    {
        if(photonView.isMine)
            delShoot += DefaultShoot;
    }
    private void OnDisable()
    {
        if (photonView.isMine)
            delShoot -= DefaultShoot;
    }
    private void Start()
    {
        // Ignore raycasts ONLY on the local client
        if(photonView.isMine)
        {
            gameObject.layer = 2;
        }
        
        cam = GetComponent<CameraController>().cam;
        if(photonView.isMine)
        {
            // Create an aim point as a child of the camera
            Vector3 newPoint = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, range));
            DefaultAimPoint = Instantiate(shootPoint, cam.transform);
        }
    }
    
    private void DefaultShoot()
    {
        Shoot(projectile);
    }

    public void Shoot()
    {
        if(photonView.isMine)
        {
            delShoot.Invoke();
        }
        
    }
    public void Shoot(Projectile proj)
    {
        // Only shoot on the controlled client because the others don't have a camera
        if (photonView.isMine)
        {
            // Set default aimpoint
            Vector3 aimPoint = DefaultAimPoint.position;

            // Raycast from camera center
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0));
            if (Physics.Raycast(ray, out hit, range))
            {
                // Set new aimpoint if hit
                aimPoint = hit.point;
            }
            // Aim at the aiming point
            shootPoint.LookAt(aimPoint);
            // Call the shoot rpc
            photonView.RPC("RPC_ShootProjectile", PhotonTargets.All, proj.name, shootPoint.position, shootPoint.rotation.eulerAngles);
        }
        else
        {
            Debug.LogWarning("Can only shoot on your client");
        }
    }
    public void Shoot(string projName)
    {
        // Only shoot on the controlled client because the others don't have a camera
        if (photonView.isMine)
        {
            // Set default aimpoint
            Vector3 aimPoint = DefaultAimPoint.position;

            // Raycast from camera center
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0));
            if (Physics.Raycast(ray, out hit, range))
            {
                // Set new aimpoint if hit
                aimPoint = hit.point;
                Debug.Log("Aiming at: " + hit.collider.name);
            }
            // Aim at the aiming point
            shootPoint.LookAt(aimPoint);
            // Call the shoot rpc
            photonView.RPC("RPC_ShootProjectile", PhotonTargets.All, projName, shootPoint.position, shootPoint.rotation.eulerAngles);
        }
        else
        {
            Debug.LogWarning("Can only shoot on your client");
        }
    }

    [PunRPC]
    private void RPC_ShootProjectile(string projPrefab, Vector3 shootPos, Vector3 shootRot)
    {
        // Load and instantiate projectile prefab
        Projectile pref = Resources.Load<Projectile>(directory + projPrefab);
        Projectile proj = Instantiate(pref, shootPos, Quaternion.Euler(shootRot));
        // Call the projectile shoot function
        proj.Shoot(gameObject);
    }
    
}