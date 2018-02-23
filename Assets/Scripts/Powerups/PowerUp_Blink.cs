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

        public float BlinkDistance; // For debugging purposes. Once this has been determined, will be set to HideInInspector
                                    

        // Use this for initialization
        private void Start()
        {
        }

        public override void OnAbilityAdd()
        {
            // Set name
            Name = "Blink";
            Debug.Log(Name + " Added");
            Cooldown = 5.0f;
            playercontrol = GetComponent<PlayerController>();
           

            // Call base function
            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
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
            // Checks if player is blocked by a wall or player, only allows activate if the raycast returns false
            if (Physics.Raycast(transform.position, transform.TransformDirection(transform.forward), BlinkDistance) == false)
            {
                transform.position += transform.forward * BlinkDistance;
                // **Cooldown**

            }

            base.Activate();
        }

    }
}