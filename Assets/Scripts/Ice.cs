using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Projectile
{
    private void Awake()
    {
        SlowMovement slow = new SlowMovement();
        onHitEffects.Add(slow);
    }
}
