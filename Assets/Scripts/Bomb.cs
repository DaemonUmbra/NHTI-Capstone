using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    protected override void OnPlayerHit(Collider hitPlayer)
    {
        GameObject hit = hitPlayer.gameObject;
        PhotonView hitView = hit.GetPhotonView();
        PlayerStats hitStats = hit.GetComponent<PlayerStats>();
        GameObject boom;

        // Verify hit photon view
        if (hitView)
        {
            // Make sure the bullet isn't hitting it's own player
            if (hitPlayer.gameObject != _shooter && hitStats)
            {
                // Apply damage to the player
                hitStats.TakeDamage(damage, _shooter);
                print("Player hit!");

                Explode();
                Destroy(gameObject);
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Environment"))
        {
            Explode();
        }
        base.OnTriggerEnter(other);
    }

    private void Explode()
    {
        GameObject boom = PhotonNetwork.Instantiate("NovaExplosion", transform.position, transform.rotation, 0);
        NovaDummy stats = boom.GetComponent<NovaDummy>();
        stats.ExplosionSize = 3.0f;
        stats.ExplosionDamage = 15.0f;
        stats.ExplosionForce = 4.5f;
        stats.SetOwner(_shooter);
    }
}
