using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Blink : ActiveAbility {

    [HideInInspector]
    public PlayerController playercontrol;
    public float BlinkDistance; // For debugging purposes. Once this has been determined, will be set to HideInInspector

	// Use this for initialization
	void Start () {
	}

    public override void OnAbilityAdd()
    {
        // Set name
        Name = "Blink";
        Debug.Log(Name + " Added");
        playercontrol = GetComponent<PlayerController>();
        BlinkDistance = 30;
    }

    public override void OnAbilityRemove()
    {
        // Call base function
        base.OnAbilityRemove();
    }

    public override void OnUpdate()
    {
        throw new NotImplementedException();
    }

    public override void Activate()
    {
        transform.position += transform.forward * BlinkDistance;
    }
}
