using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Projectile
{
    private new void Awake()
    {
        base.Awake();
        SlowMovement hitSlow = new SlowMovement();
        onHitEffects.Add(hitSlow);
    }
}
