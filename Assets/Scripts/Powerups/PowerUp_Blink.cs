using UnityEngine;

/*
 * Programmer Assigned: Steven Zachary
 * Power-up: Blink
 * Description: On button press the player teleports forward
 */

namespace Powerups
{
    public class PowerUp_Blink : ActiveAbility
    {
        [HideInInspector]
        public PlayerController playercontrol;

        private PlayerShoot pShoot;

        public float BlinkDistance = 12; // For debugging purposes. Once this has been determined, will be set to HideInInspector
                                    

        // Use this for initialization
        private void Start()
        {
        }

        public override void OnAbilityAdd()
        {
            // Set name
            Name = "Blink";
            Debug.Log(Name + " Added");
            Cooldown = 2.0f;
            playercontrol = GetComponent<PlayerController>();

            pShoot = GetComponent<PlayerShoot>();
            if (pShoot)
            {

                pShoot.shoot += TryActivate;
            }
            // Call base function
            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            if (pShoot)
            {
                pShoot.shoot -= TryActivate;
            }
            pShoot = null;

            // Call base function
            base.OnAbilityRemove();
        }

        public override void OnUpdate()
        {
            // Call base function
            base.OnAbilityAdd();
        }

        protected override void Activate()
        {
            Debug.Log("Blink!");
            // Checks if player is blocked by a wall or player, only allows activate if the raycast returns false
            if (photonView.isMine)
            {
                Vector3 destination = transform.position + transform.forward * BlinkDistance;
                RaycastHit hit;
                if (Physics.Linecast(transform.position, destination, out hit))
                {
                    destination = transform.position + transform.forward * (hit.distance - 1.5f);
                }

                destination.y = 1000;
                if (Physics.Raycast(destination, -Vector3.up, out hit))
                {
                    destination = hit.point;
                    destination.y += 2;
                    transform.position = destination;
                }
                //    if (Physics.Raycast(transform.position, transform.TransformDirection(transform.forward), BlinkDistance) == false)
                //    {
                //        if (photonView.isMine)
                //        {
                //            transform.position = transform.forward * BlinkDistance;
                //        }

                //        **Cooldown * *

                //}
            }
                base.Activate();
            
        }

    }
}