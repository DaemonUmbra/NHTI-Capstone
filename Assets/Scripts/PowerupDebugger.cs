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
    public BaseAbility SelectedPlayerPowerup;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void AddAbility(BaseAbility ability)
    {
        if (Player)
        {
            if (!Player.HasAbility(ability))
            {
                Player.AddAbility(ability);
            }
        }
    }
    public void RemoveAbility(BaseAbility ability)
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
