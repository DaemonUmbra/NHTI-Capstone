using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Fireball : ActiveAbility
{
    PlayerShoot pShoot;

    public override void OnAbilityAdd()
    {
        Name = "Fireball";
        Debug.Log(Name + " Added");

        pShoot = GetComponent<PlayerShoot>();
        if (pShoot)
        {
            Debug.Log("Fireball Added to Shoot Delegate");
            pShoot.shoot += OnShoot;
        }
    }

    public override void OnAbilityRemove()
    {
        // Remove shoot delegate
        if (pShoot)
        {
            pShoot.shoot -= OnShoot;
        }
        pShoot = null;

        // Call base function
        base.OnAbilityRemove();
    }

    public override void OnUpdate()
    {

    }

    public override void Activate()
    {
        throw new NotImplementedException();
    }

    public void OnShoot()
    {
        
    }
}