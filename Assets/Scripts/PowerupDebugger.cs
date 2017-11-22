using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupDebugger : MonoBehaviour {
    [HideInInspector]
    public AbilityManager Player = null;
    [HideInInspector]
    public Type SelectedPowerup;
    [HideInInspector]
    public Ability SelectedPlayerPowerup;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void AddAbility(Ability ability)
    {
        if (Player)
        {
            if (!Player.HasAbility(ability))
            {
                Player.AddAbility(ability);
            }
        }
    }
    public void RemoveAbility(Ability ability)
    {
        if (Player)
        {
            if (Player.HasAbility(ability))
            {
                Player.RemoveAbility(ability);
            }
        }
    }
}
