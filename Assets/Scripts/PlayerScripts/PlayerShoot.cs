using Powerups;
using UnityEngine;

public class PlayerShoot : Photon.MonoBehaviour
{
    // Shooting delegate
    public delegate void Shoot();

    public Shoot shoot;
    Camera cam;

    public GameObject projectile;

    public Transform OffsetPoint;
    public Transform AimPoint;
    
    
    private void OnEnable()
    {
        shoot += DefaultShoot;
    }
    private void OnDisable()
    {
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
        if (photonView.isMine)
        {
            GameObject _proj = PhotonNetwork.Instantiate(projectile.name, OffsetPoint.position, OffsetPoint.rotation, 0);
        }
    }
}