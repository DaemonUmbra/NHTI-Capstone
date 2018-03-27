using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Fireball : Projectile
{
    private new void Awake()
    {
        base.Awake();
        BurnDamage hitBurn = new BurnDamage(1f, 3f);
        onHitEffects.Add(hitBurn);
    }
}