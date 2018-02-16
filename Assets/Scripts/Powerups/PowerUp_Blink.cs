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
                                    //public bool CoolDown;

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
            //CoolDown = false;   *** Cooldown handled by base class ***

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

        protected override void RPC_Activate()
        {
            //if (CoolDown == false) // Checks if powerup has been used
            // Checks if player is blocked by a wall or player, only allows activate if the raycast returns false
            if (Physics.Raycast(transform.position, transform.TransformDirection(transform.forward), BlinkDistance) == false)
            {
                transform.position += transform.forward * BlinkDistance;
                // *** Cooldown handled by base class ***
                // CoolDown = true;
                // StartCoroutine(CooldownTimer());
            }

            base.RPC_Activate();
        }

        /*** Cooldown handled by base class ***
        IEnumerator CooldownTimer()
        {
            yield return new WaitForSecondsRealtime(Cooldown);
            CoolDown = false;
        }
        */
    }
}