using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubbles : Projectile
{
    public override void Shoot(GameObject shooter)
    {
        base.Shoot(shooter);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        // If this hits a projectile, destroy both projectiles
        if(other.CompareTag("Projectile"))
        {
            Destroy(other);
            Destroy(gameObject);
        }
        base.OnTriggerEnter(other);
    }
}
