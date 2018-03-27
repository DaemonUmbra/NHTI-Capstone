using Powerups;
using UnityEngine;

public class PlayerShoot : Photon.MonoBehaviour
{
    // Shooting delegate
    public delegate void Shoot();

    public Shoot shoot;

    public GameObject projectile;

    public readonly Vector3 PosOffset = new Vector3(0, 2, 0);

    public readonly Vector3 RotOffset = new Vector3(0, 0, 0);
    
    private void Awake()
    {
        shoot += DefaultShoot;
    }

    public void DefaultShoot()
    {
        if (photonView.isMine)
        {
            GameObject _proj = PhotonNetwork.Instantiate(projectile.name, transform.position + PosOffset, Quaternion.LookRotation(transform.rotation.eulerAngles + RotOffset), 0);
        }
    }
}