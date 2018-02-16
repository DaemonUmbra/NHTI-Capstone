using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class RingOfBullets : ActiveAbility
    {
        // Number of bullets to shoot out
        private int BulletCount = 30;

        private PlayerShoot pShoot;

        // Called when ability is added to player
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
                pShoot.shoot += RPC_Activate;
            }

            // Call base function
            base.OnAbilityAdd();
        }

        // Called every frame
        public override void OnUpdate()
        {
            // Call base function
            base.OnUpdate();
        }

        public override void OnAbilityRemove()
        {
            // Remove shoot delegate
            if (pShoot)
            {
                pShoot.shoot -= RPC_Activate;
            }

            // Call base function
            base.OnAbilityRemove();
        }

        // Called on every client when the player shoots
        protected override void RPC_Activate()
        {
            base.RPC_Activate();

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

                GameObject b = PhotonNetwork.Instantiate(pShoot.projectile.name, transform.position, NewRotation, 0); // Make sure to set parent
                b.GetComponent<Projectile>().IgnorePlayer(gameObject);
                bullets.Add(b);
            }
            Debug.Log(bullets);
        }
    }
}