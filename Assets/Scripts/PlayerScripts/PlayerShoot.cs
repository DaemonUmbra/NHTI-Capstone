using Powerups;
using UnityEngine;

public class PlayerShoot : Photon.MonoBehaviour
{
    // Shooting delegate
    public delegate void Shoot();

    public Shoot shoot;

    public GameObject projectile;
    
    private void Awake()
    {
        shoot += DefaultShoot;
    }

    public void DefaultShoot()
    {
        if (photonView.isMine)
        {
            GameObject _proj = PhotonNetwork.Instantiate(projectile.name, transform.position, transform.rotation, 0);
        }
    }
}