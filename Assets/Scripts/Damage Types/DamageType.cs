﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base damage type class. 
/// Derive all damage type classes from this.
/// </summary>
public abstract class DamageType {

    // Effects passed to the target
    protected List<Effect> OnHitEffects;
    string TypeName = "NO_NAME";

    public DamageType()
    {

    }
    public DamageType(List<Effect> effects)
    {
        OnHitEffects = effects;
    }

    // Call this function when a player takes damage. Pass in the hit character.
    public virtual void ApplyDamageType(GameObject target)
    {
        // Applies the on hit effects to the target
        foreach (Effect e in OnHitEffects)
        {
            e.ApplyEffect(target);
        }
    }
}
