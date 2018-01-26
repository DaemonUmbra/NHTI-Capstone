using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class SingleShot : ActiveAbility
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
                pShoot.shoot += Activate;
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
                pShoot.shoot -= Activate;
            }
            pShoot = null;

            // Call base function
            base.OnAbilityRemove();
        }

        public override void Activate()
        {
            GameObject proj = Instantiate(pShoot.projectile, transform.position, transform.rotation, transform);
        }
    }
}