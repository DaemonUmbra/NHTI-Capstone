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
            throw new NotImplementedException();
        }
        public void OnShoot()
        {
            if (onCooldown)
            {
                Debug.Log("This ability is on cooldown" + "Start Time: " + CDstart);
                return;
            }
            Vector3 mp = Input.mousePosition;
            mp.z = 10;
            Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(mp);
            Vector3 targetVector = mouseLocation;
        }
        IEnumerator TriggerCoolDown()
        {
            onCooldown = true;
            CDstart = Time.fixedTime;
            yield return new WaitForSeconds(5);
            onCooldown = false;
        }

    }
}
