using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeDamage : DamageType {

    /// <summary>
    /// Percent by which to slow the target
    /// </summary>
    public float PercentSlow;
    /// <summary>
    /// Time to slow the target for
    /// </summary>
    public float SlowTime;

    public override void ApplyDamageType(GameObject target)
    {
        PlayerController pc = target.GetComponent<PlayerController>();
       
        base.ApplyDamageType(target);
    }

}
