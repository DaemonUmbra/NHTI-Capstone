using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMovement : Effect {

    /// <summary>
    /// Amount to slow the player by as a decimal
    /// </summary>
    private float _slowAmount;
    
    // Default constructor
    public SlowMovement(float slowAmount, float lifetime)
    {
        _slowAmount = slowAmount;
        _lifetime = lifetime;
    }
    // Copy constructor
    public SlowMovement(SlowMovement jango)
    {
        _slowAmount = jango._slowAmount;
        Debug.Log(jango + "cloned.");
        
        _lifetime = jango.Lifetime;
        _tickTime = jango.TickTime;
        Debug.Log("Lifetime: " + jango.Lifetime + " | " + Lifetime);
        Debug.Log("Ticktime: " + jango.TickTime + " | " + TickTime);
    }

    
    public override void ApplyEffect(GameObject target)
    {
        // Ref the player stat class
        PlayerStats ps = target.GetComponent<PlayerStats>();
        if(!ps)
        {
            Debug.LogError("No Player Stats class in target: " + target);
            return;
        }

        // Copy this effect to the target
        SlowMovement slow = new SlowMovement(this);
        ps.AddEffect(slow);
    }

    public override void Activate()
    {
        // Slow the target
        PlayerStats ps = Owner.GetComponent<PlayerStats>();
        ps.WalkSpeed *= _slowAmount;

        base.Activate();
    }

    public override void RemoveEffect()
    {
        // Reverse effect
        PlayerStats ps = Owner.GetComponent<PlayerStats>();
        if (ps == null) {
            Debug.LogError("Stats script not found. Unable to remove effect");
            return;
        }
        float factor = 1 / _slowAmount; // Inverse of the slow percent
        if (factor < 100 && factor > 0.01)
            ps.WalkSpeed *= factor;

        // Call base to remove effect from player
        base.RemoveEffect();
    }
    

}
