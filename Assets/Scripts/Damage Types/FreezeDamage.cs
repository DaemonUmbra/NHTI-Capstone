using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeDamage : DamageType {
    
    // Default constructor
    public FreezeDamage(float _percentSlow, float _slowTime) 
    {
        SlowMovement slow = new SlowMovement(_percentSlow, _slowTime);
        OnHitEffects.Add(slow);
    }

    public override void ApplyDamageType(GameObject target)
    {
        PlayerController pc = target.GetComponent<PlayerController>();


        base.ApplyDamageType(target);
    }

}
