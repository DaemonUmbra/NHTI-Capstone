using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : BaseAbility
{

    PlayerShoot pShoot;

    public override void OnAbilityAdd()
    {
        // Set name
        Name = "SingleShot";
        Debug.Log(Name + " Added");

        // Add new shoot function to delegate 
        pShoot = GetComponent<PlayerShoot>();
        if (pShoot)
        {
            Debug.Log("Single Shot Added to Shoot Delegate");
            pShoot.shoot += OnShoot;
        }
    }

    public override void OnUpdate()
    {
        // Nothing yet
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

    public void OnShoot()
    {
        GameObject _proj = Instantiate(pShoot.projectile, transform.position, transform.rotation, transform);
    }
}
