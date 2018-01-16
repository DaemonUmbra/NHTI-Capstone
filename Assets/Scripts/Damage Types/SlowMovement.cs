using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMovement : Effect {

    private float PercentSlow;
    
    // Default constructor
    public SlowMovement(float _percentSlow, float _lifetime)
    {
        PercentSlow = _percentSlow;
        Lifetime = _lifetime;
    }


    public override void ApplyEffect(GameObject target)
    {
        base.ApplyEffect(target);   // Call the base first

        // Add this effect to the target
        SlowMovement effect = target.AddComponent<SlowMovement>();
        
        PlayerMotor pm = GetComponent<PlayerMotor>();
        pm.AdjustSpeed(PercentSlow);

        // Copy this version of the effect to the target. Do this last!
        effect = this;
    }

    public override void RemoveEffect()
    {
        // Reverse the adjustment by dividing by the slow percent
        PlayerMotor pm = GetComponent<PlayerMotor>();
        pm.AdjustSpeed(1 / PercentSlow); // Inverse of the slow percent
        Destroy(this); // Delete the game object
    }
    

}
