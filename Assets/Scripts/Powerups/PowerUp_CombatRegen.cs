using System.Collections;
using UnityEngine;

/*
 * Programmer Assigned: Steven Zachary
 * Power-up: Combat Regeneration
 * Description: Constantly restores a small amount of health to the player after a set amount of time.
 */

namespace Powerups
{
    public class Powerup_CombatRegen : PassiveAbility
    {
        [HideInInspector]
        public PlayerStats PS;

        public override void OnAbilityAdd()
        {
            Name = "Combat Regeneration";
            Debug.Log(Name + " Added");
            PS = GetComponent<PlayerStats>();
            StartCoroutine(Regen()); // Begins the regen process

            // Call base function
            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            // Call base function
            base.OnAbilityRemove();
        }

        private IEnumerator Regen()
        {
            while (active) // While the player holds the power up, player gains 10 HP every 20 seconds
            {                
                PS.GainHp(10.0f);
                Debug.Log("You gained 10 HP!");
                yield return new WaitForSecondsRealtime(10);                
            }
        }
    }
}