﻿using System;
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
                pShoot.shoot += RPC_Activate;
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
                pShoot.shoot -= RPC_Activate;
            }
            pShoot = null;

            // Call base function
            base.OnAbilityRemove();
        }
        
        protected override void RPC_Activate()
        {
            base.RPC_Activate();
            GameObject _proj = PhotonNetwork.Instantiate(pShoot.projectile.name, transform.position, transform.rotation,0);
            _proj.GetComponent<Projectile>().IgnorePlayer(gameObject);
        }
    }
}