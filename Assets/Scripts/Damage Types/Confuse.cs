using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confuse : Effect
{
    public Confuse(float lifetime)
    {
        _lifetime = lifetime;
    }

    public override void Activate()
    {
        base.Activate();
    }

    public override void ApplyEffect(GameObject target)
    {
        // Ref the player stat class
        PlayerStats ps = target.GetComponent<PlayerStats>();
        if (!ps)
        {
            Debug.LogError("No Player Stats class in target: " + target);
            return;
        }

        // Apply the effect
        Confuse confused = new Confuse(this._lifetime);
        ps.AddEffect(confused);
    }
}
