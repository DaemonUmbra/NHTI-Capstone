using Powerups;
using UnityEngine;

public class PlayerShoot : Photon.MonoBehaviour
{
    // Shooting delegate
    public delegate void Shoot();
    public Shoot shoot;

    Camera cam;

    public Projectile projectile;

    public Transform OffsetPoint;
    public Transform AimPoint;
    
    
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
        if(photonView.isMine)
        {
            cam = GetComponent<CameraController>().cam;
            AimPoint = Instantiate(OffsetPoint, cam.transform);
            AimPoint.localPosition = new Vector3(0, 0, 50);
        }
    }

    private void Update()
    {
        if(photonView.isMine)
        {
            OffsetPoint.LookAt(AimPoint);
        }
        
    }
    public void DefaultShoot()
    {
        photonView.RPC("RPC_Shoot", PhotonTargets.All, OffsetPoint.position, OffsetPoint.rotation.eulerAngles);
    }

    [PunRPC]
    private void RPC_Shoot(Vector3 position, Vector3 rotation)
    {
        Projectile proj = Instantiate(projectile, position, Quaternion.Euler(rotation));
        proj.Shoot(gameObject);
    }
}