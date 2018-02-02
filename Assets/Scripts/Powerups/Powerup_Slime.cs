using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class Powerup_Slime : ActiveAbility
    {
        private float CDstart;
        bool onCooldown = false, CurrentlyActive = false;
        PlayerShoot pShoot;
        float offset = 2;


        public override void OnAbilityAdd()
        {
            // Set name
            Name = "Slime";
            Debug.Log(Name + " Added");

            // Add new shoot function to delegate 
            pShoot = GetComponent<PlayerShoot>();
            if (pShoot)
            {
                Debug.Log("Slime Added to Shoot Delegate");
                pShoot.shoot += OnShoot;
            }
        }
        public override void OnUpdate()
        {

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
        public override void Activate()
        {
            GameObject ball = Resources.Load("Prefabs/SlimeBall") as GameObject;
            Instantiate(ball);
            throw new NotImplementedException();
        }
        public void OnShoot()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Debug.Log(player.name);
            GameObject ball = Resources.Load("Prefabs/SlimeBall") as GameObject;
            Instantiate(ball, (player.transform.forward * offset) + player.transform.position, transform.rotation);
            /*if (onCooldown)
            {
                Debug.Log("This ability is on cooldown" + "Start Time: " + CDstart);
                return;
            }*/

        }
        private void TriggerCoolDown()
        {
            CDstart = Time.fixedTime;
        }

    }
}
