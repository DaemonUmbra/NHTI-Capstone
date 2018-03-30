using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class Active_RingOfBullets : ActiveAbility
    {
        // Number of bullets to shoot out
        private int BulletCount = 40;

        private PlayerShoot pShoot;

        private void Awake()
        {
            // Set name
            Name = "Ring Of Bullets";
            Cooldown = 5f;
        }
        // Called when ability is added to player
        public override void OnAbilityAdd()
        {
            Debug.Log(Name + " Added");

            // Add new shoot function to delegate
            pShoot = GetComponent<PlayerShoot>();
            

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
            // Call base function
            base.OnAbilityRemove();
        }

        // Called on every client when the player shoots
        protected override void Activate()
        {
            base.Activate();

            if (photonView.isMine)
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

                    GameObject b = PhotonNetwork.Instantiate(pShoot.projectile.name, transform.position, NewRotation, 0);
                    //b.GetComponent<Projectile>().IgnorePlayer(gameObject);
                    bullets.Add(b);
                }
            }

        }
    }
}