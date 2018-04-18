using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    private new void Awake()
    {
        base.Awake();
        BurnDamage hitBurn = new BurnDamage(1f, 3f);
        onHitEffects.Add(hitBurn);
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
                //List<Effect> onHits = _shooter.GetComponent<PlayerStats>().OnHitEffects;
                hitStats.TakeDamage(damage, _shooter, "burn");
                print("Player hit!");
                PhotonNetwork.Destroy(photonView);
            }
        }

    }
}