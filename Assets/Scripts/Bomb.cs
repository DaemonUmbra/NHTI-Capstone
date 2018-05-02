using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    protected override void OnPlayerHit(PlayerStats hitPlayer)
    {
        PhotonView hitView = hitPlayer.gameObject.GetPhotonView();

        // Make sure the bullet isn't hitting it's own player
        if (hitPlayer.gameObject != _shooter)
        {
            Explode();
            Destroy(gameObject);
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
