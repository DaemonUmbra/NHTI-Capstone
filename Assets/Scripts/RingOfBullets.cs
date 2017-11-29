using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{

    public class RingOfBullets : ActiveAbility
    {

        // Number of bullets to shoot out
        int BulletCount = 30;
        PlayerShoot pShoot;

        public override void OnAbilityAdd()
        {
            // Set name
            Name = "RingOfBullets";
            Debug.Log(Name + " Added");

            // Add new shoot function to delegate 
            pShoot = GetComponent<PlayerShoot>();
            if (pShoot)
            {
                Debug.Log("Ring Shoot Added to Shoot Delegate");
                pShoot.shoot += Activate;
            }
        }
        // Called every frame
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
            Debug.Log("Trying to Shoot a Ring!");
            List<GameObject> bullets = new List<GameObject>();
            // Spawn a ring of bullets
            for (int i = 0; i < BulletCount; ++i)
            {

                // Create a bullet, rotate by a fraction of 360

                // Integer Division sucks. 
                float NewAngle = (float)i / (float)BulletCount * 360.0f;
                // Debug.Log("NewAngle: " + NewAngle);
                Quaternion NewRotation = Quaternion.Euler(0, NewAngle, 0);
                // Debug.Log("NewRotation: " + NewRotation);

                GameObject b = Instantiate(pShoot.projectile, transform.position, NewRotation);
                bullets.Add(b);
            }
            Debug.Log(bullets);
        }
    }
}