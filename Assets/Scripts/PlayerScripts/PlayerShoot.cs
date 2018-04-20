using Powerups;
using UnityEngine;

public class PlayerShoot : Photon.MonoBehaviour
{
    // Shooting delegate
    public delegate void DelShoot();
    public DelShoot shoot;
    public int shootMask = 1 << 11;
    Camera cam;

    public string directory = "Projectiles/";
    public Projectile projectile;

    public Transform OffsetPoint;
    public Transform DefaultAimPoint;
    public float range = 100;
    
    
    private void OnEnable()
    {
        if(photonView.isMine)
            shoot += DefaultShoot;
    }
    private void OnDisable()
    {
        if (photonView.isMine)
            shoot -= DefaultShoot;
    }
    private void Start()
    {
    }

    private void Update()
    {
        if(photonView.isMine)
        OffsetPoint.LookAt(DefaultAimPoint);
    }
    public void DefaultShoot()
    {
        photonView.RPC("RPC_Shoot", PhotonTargets.All, OffsetPoint.position, OffsetPoint.rotation.eulerAngles);
    }

    public void Shoot(Projectile proj)
    {
        if(photonView.isMine)
        {
            photonView.RPC("RPC_ShootProjectile", PhotonTargets.All, proj.name);
        }
        else
        {
            Debug.LogWarning("Can only shoot on your client");
        }
    }
    public void Shoot(string prefabName)
    {
        if (photonView.isMine)
        {
            photonView.RPC("RPC_ShootProjectile", PhotonTargets.All, prefabName);
        }
        else
        {
            Debug.LogWarning("Can only shoot on your client");
        }
    }

    [PunRPC]
    private void RPC_ShootProjectile(string projPrefab)
    {
        // Load projectile
        Projectile proj = Resources.Load<Projectile>(directory + projPrefab);
        // Set default aimpoint
        Vector3 aimPoint = DefaultAimPoint.position;

        // Raycast from camera center
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0));
        if(Physics.Raycast(ray, out hit, range))
        {
            // Set new aimpoint if hit
            aimPoint = hit.point;
        }
    }
}