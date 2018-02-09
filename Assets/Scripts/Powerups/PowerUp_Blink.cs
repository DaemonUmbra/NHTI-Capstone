﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Blink : ActiveAbility {

    [HideInInspector]
    public PlayerController playercontrol;
    public float BlinkDistance; // For debugging purposes. Once this has been determined, will be set to HideInInspector
    public bool CoolDown; 

	// Use this for initialization
	void Start () {
	}

    public override void OnAbilityAdd()
    {
        // Set name
        Name = "Blink";
        Debug.Log(Name + " Added");
        Cooldown = 5.0f;
        playercontrol = GetComponent<PlayerController>();
        CoolDown = false;
    }

    public override void OnAbilityRemove()
    {

    }

    public override void OnUpdate()
    {
    }

    public override void Activate()
    {
        if (CoolDown == false)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(transform.forward), BlinkDistance) == false)
            {
                transform.position += transform.forward * BlinkDistance;
                CoolDown = true;
                StartCoroutine(CooldownTimer());
            }
        }

        
    }

    IEnumerator CooldownTimer()
    {
        yield return new WaitForSecondsRealtime(Cooldown);
        CoolDown = false;
    }
}
