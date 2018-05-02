using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBall : Projectile
{
    public float chainRange = 8f;
    public float chainDamage = 5f;

    protected override void OnPlayerHit(PlayerStats hitPlayer)
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, chainRange, transform.forward);
        
        // Deal damage to each player hit with the spherecast
        foreach(RaycastHit hit in hits)
        {
            if(PhotonNetwork.isMasterClient)
            {
                PlayerStats hitStats = hit.collider.gameObject.GetComponent<PlayerStats>();
                hitStats.TakeDamage(chainDamage, _shooter);
            }
            // *** Visual effect needed
        }
        
    }
}
