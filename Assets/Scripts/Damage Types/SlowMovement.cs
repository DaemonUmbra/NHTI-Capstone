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
        _lifetime = jango.Lifetime;
        _tickTime = jango.TickTime;
    }


    public override void ApplyEffect(GameObject target)
    {
        // Copy this effect to the target
        SlowMovement slow = target.AddComponent<SlowMovement>();
        slow = new SlowMovement(this);
        slow.Activate();
        // Slow the target
        PlayerMotor pm = target.GetComponent<PlayerMotor>();
        pm.AdjustSpeed(slow._percentSlow);
    }

    public override void RemoveEffect()
    {
        // Reverse the adjustment by dividing by the slow percent
        PlayerMotor pm = GetComponent<PlayerMotor>();
        float factor = 1 / _percentSlow; // Inverse of the slow percent
        if(factor < 100)
            pm.AdjustSpeed(factor);
        Destroy(this);
    }
    

}
