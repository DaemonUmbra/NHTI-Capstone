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
    /// <summary>
    /// Copy constructor, used to copy abilities to a player
    /// </summary>
    /// <param name="jango"></param>
    public SlowMovement(SlowMovement jango)
    {
        _slowAmount = jango._slowAmount;
        Debug.Log(jango + "cloned.");
        
        _lifetime = jango.Lifetime;
        _tickTime = jango.TickTime;
        Debug.Log("Lifetime: " + jango.Lifetime + " | " + Lifetime);
        Debug.Log("Ticktime: " + jango.TickTime + " | " + TickTime);
    }

    /// <summary>
    /// Copies the effect to a target. Must transfer all values over
    /// a copy constructor helps with this.
    /// </summary>
    /// <param name="target"></param>
    public override void ApplyEffect(GameObject target)
    {
        // Ref the player stat class
        PlayerStats ps = target.GetComponent<PlayerStats>();
        if(!ps)
        {
            Debug.LogError("No Player Stats class in target: " + target.name);
            return;
        }

        // Copy this effect to the target. A copy constructor helps with this
        SlowMovement slow = new SlowMovement(this);

        // The copy is added to the player
        ps.AddEffect(slow);
        // ps.AddEffect(this) // WRONG will add the original not a copy
    }

    /// <summary>
    /// Slows the player when the ability is added to a player
    /// </summary>
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
