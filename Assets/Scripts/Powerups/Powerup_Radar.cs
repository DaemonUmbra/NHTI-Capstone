using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Radar : PassiveAbility {

 /*
 * Programmer Assigned: Steven Zachary
 * Power-up: Radar
 * Description: A soft beep, beeps more urgently as you approach a rare or higher powerup.                
 */

    // Use this for initialization
    void Start () {
		
	}

    public override void OnAbilityAdd() // Function for adding ability to player
    {
        Name = "Radar";
        Debug.Log(Name + " added!");
        base.OnAbilityAdd();
    }

    public override void OnAbilityRemove() // Function for removing ability from player
    {
        base.OnAbilityRemove();
    }

    public override void OnUpdate() // Update function
    {
        base.OnUpdate();
    }
}
