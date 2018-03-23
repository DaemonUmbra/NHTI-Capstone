using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBurn : Projectile
{
    public new void Start()
    {
        base.Start();
    }

    public new void Update()
    {
        base.Update();
    }
    private void OnTriggerEnter(Collider collision)
    {
        GameObject hit = collision.gameObject;
        PhotonView hitView = hit.GetPhotonView();
        PlayerStats hitStats = hit.GetComponent<PlayerStats>();
        // Verify hit photon view
        if (hitView)
        {
            // Make sure the bullet isn't hitting it's own player
            if (hitView.owner != photonView.owner && hitStats && photonView.isMine)
            {
                // Apply damage to the player
                hitStats.TakeDamage(damage);

                print("Player hit by fireball!");
                PhotonNetwork.Destroy(photonView);
            }
        }
    }
}