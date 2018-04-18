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
    
    private void Awake()
    {
        shoot += DefaultShoot;
    }
    private void Start()
    {
        cam = GetComponent<CameraController>().cam;
        AimPoint = Instantiate(OffsetPoint, cam.transform);
        AimPoint.localPosition = new Vector3(0, 0, 50);
    }

    private void Update()
    {
        OffsetPoint.LookAt(AimPoint);
    }
    public void DefaultShoot()
    {
        if (photonView.isMine)
        {
            GameObject _proj = PhotonNetwork.Instantiate(projectile.name, OffsetPoint.position, OffsetPoint.rotation, 0);
        }
    }
}