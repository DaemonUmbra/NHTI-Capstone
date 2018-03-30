using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    private new void Awake()
    {
        base.Awake();
    }

    protected override void onPlayerHit(Collider hitPlayer)
    {
        GameObject hit = hitPlayer.gameObject;
        PhotonView hitView = hit.GetPhotonView();
        PlayerStats hitStats = hit.GetComponent<PlayerStats>();
        // Verify hit photon view
        if (hitView)
        {
            // Make sure the bullet isn't hitting it's own player
            if (hitView.owner != photonView.owner && hitStats && photonView.isMine)
            {
                // Apply damage to the player
                hitStats.TakeDamage(damage, hitStats.gameObject, onHitEffects);
                print("Player hit!");

                

                PhotonNetwork.Destroy(photonView);
                PhotonNetwork.Destroy(gameObject);
            }
        }
        if (hit.tag == "Environment")
        {
            PhotonNetwork.Destroy(photonView);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
