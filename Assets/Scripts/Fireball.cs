using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    private void Awake()
    {
        BurnDamage hitBurn = new BurnDamage(5f, 3f);
        onHitEffects.Add(hitBurn);
    }
}