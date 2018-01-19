using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMovement : Effect {

    private float _percentSlow;
    
    // Default constructor
    public SlowMovement(float percentSlow, float lifetime)
    {
        _percentSlow = percentSlow;
        _lifetime = lifetime;
    }
    // Copy constructor
    public SlowMovement(SlowMovement jango)
    {
        _percentSlow = jango._percentSlow;
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
        PlayerMotor pm = Owner.GetComponent<PlayerMotor>();
        pm.AdjustSpeed(_percentSlow);

        base.Activate();
    }

    public override void RemoveEffect()
    {
        // Reverse effect
        PlayerMotor pm = Owner.GetComponent<PlayerMotor>();
        if (pm == null) {
            Debug.LogError("Motor not found. Unable to remove effect");
            return;
        }
        float factor = 1 / _percentSlow; // Inverse of the slow percent
        if(factor < 100 && factor > 0.01)
            pm.AdjustSpeed(factor);

        // Call base to remove effect from player
        base.RemoveEffect();
    }
    

}
