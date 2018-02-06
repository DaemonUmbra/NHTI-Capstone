using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Programmer Assigned: Steven Zachary
 * Power-up: Boost
 * Description: Grants the player the ability to dash forward. Short-moderate cooldown.
 */

public class PowerUp_Boost : ActiveAbility {

    [HideInInspector]
    public PlayerController PC;
    public float BoostSpeed;


    public override void OnAbilityAdd()
    {
        Name = "Boost";
        Debug.Log(Name + "  Added");
        PC = GetComponent<PlayerController>();
    }

    public override void OnAbilityRemove()
    {
        // Call base function
        base.OnAbilityRemove();
    }

    public override void Activate()
    {
        transform.Translate(transform.forward * BoostSpeed);
    }

    public override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
