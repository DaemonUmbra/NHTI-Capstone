using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    protected override void onPlayerHit(Collider hitPlayer)
    {
        GameObject hit = hitPlayer.gameObject;
        PhotonView hitView = hit.GetPhotonView();
        PlayerStats hitStats = hit.GetComponent<PlayerStats>();
        GameObject boom;

        // Verify hit photon view
        if (hitView)
        {
            // Make sure the bullet isn't hitting it's own player
            if (hitPlayer != _shooter && hitStats)
            {
                // Apply damage to the player
                hitStats.TakeDamage(damage, _shooter);
                print("Player hit!");

                boom = PhotonNetwork.Instantiate("NovaExplosion", transform.position, transform.rotation, 0);
                NovaDummy stats = boom.GetComponent<NovaDummy>();
                stats.ExplosionSize = 3.0f;
                stats.ExplosionDamage = 15.0f;
                stats.ExplosionForce = 4.5f;

                Destroy(gameObject);
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (other.gameObject.tag == "Player")
            {
                onPlayerHit(other);
            }
            if (other.tag == "Environment")
            {
                GameObject boom = PhotonNetwork.Instantiate("NovaExplosion", transform.position, transform.rotation, 0);
                NovaDummy stats = boom.GetComponent<NovaDummy>();
                stats.ExplosionSize = 3.0f;
                stats.ExplosionDamage = 15.0f;
                stats.ExplosionForce = 4.5f;

                PhotonNetwork.Destroy(photonView);
            }
        }
    }
}
