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
        PlayerMotor pm = target.GetComponent<PlayerMotor>();
        
        
        base.ApplyEffect(target);
    }

    public override void RemoveEffect()
    {
        throw new NotImplementedException();
    }
    

}
