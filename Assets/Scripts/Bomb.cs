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
        ParticleSystem explosion = gameObject.GetComponent<ParticleSystem>();

        // Verify hit photon view
        if (hitView)
        {
            // Make sure the bullet isn't hitting it's own player
            if (hitView.owner != photonView.owner && hitStats && photonView.isMine)
            {
                // Apply damage to the player
                hitStats.TakeDamage(damage, _shooter);
                print("Player hit!");

                explosion.Play();

                RaycastHit hits;

                if(Physics.SphereCast(gameObject.transform.position, 5.0f, transform.forward, out hits, 5))
                {
                    if (!hitView)
                    {
                        PlayerStats newHitStats = hits.transform.gameObject.GetComponent<PlayerStats>();
                        newHitStats.TakeDamage((damage / 2.0f), _shooter);
                        PlayerController knockback = hits.transform.gameObject.GetComponent<PlayerController>();
                        knockback.RPC_KnockBack(Vector3.back, 5.0f, Vector3.one);
                    }
                }

                PhotonNetwork.Destroy(photonView);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onPlayerHit(other);
        }
        if (other.tag == "Environment")
        {
            RaycastHit hits;

            if (Physics.SphereCast(gameObject.transform.position, 5.0f, transform.forward, out hits, 5))
            {
                PlayerStats newHitStats = hits.transform.gameObject.GetComponent<PlayerStats>();
                newHitStats.TakeDamage((damage / 2.0f), _shooter);
            }

            PhotonNetwork.Destroy(photonView);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
