using UnityEngine;

/*
 * Programmer Assigned: Steven Zachary
 * Power-up: Boost
 * Description: Grants the player the ability to dash forward. Short-moderate cooldown.
 */

namespace Powerups
{
    public class Powerup_Boost : ActiveAbility
    {
        [HideInInspector]
        public PlayerController PC;

        public float BoostSpeed;

        public override void OnAbilityAdd()
        {
            Name = "Boost";
            Debug.Log(Name + "  Added");
            PC = GetComponent<PlayerController>();

            // Call base function
            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            // Call base function
            base.OnAbilityRemove();
        }

        protected override void RPC_Activate()
        {
            transform.Translate(transform.forward * BoostSpeed);

            // Call base function
            base.RPC_Activate();
        }
    }
}