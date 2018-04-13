using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBall : Projectile
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
                //List<Effect> onHits = _shooter.GetComponent<PlayerStats>().OnHitEffects;
                hitStats.TakeDamage(damage, _shooter);
                print("Player hit!");

                RaycastHit otherHit;

                if(Physics.Raycast(gameObject.transform.position, transform.forward, out otherHit))
                {
                    PlayerStats newHitStats = otherHit.transform.gameObject.GetComponent<PlayerStats>();
                    newHitStats.TakeDamage(damage, _shooter);
                }

                PhotonNetwork.Destroy(photonView);
                PhotonNetwork.Destroy(gameObject);
            }
        }

    }
}
