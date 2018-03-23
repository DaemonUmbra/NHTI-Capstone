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
        public bool Healing;

        public override void OnAbilityAdd()
        {
            Name = "Combat Regeneration";
            Debug.Log(Name + " Added");
            PS = GetComponent<PlayerStats>();
            Healing = false;

            // Call base function
            base.OnAbilityAdd();
        }

        public override void OnUpdate()
        {
            if(Healing == false)
            {
                StartCoroutine(Regen());
            }
            base.OnUpdate();
        }


        public override void OnAbilityRemove()
        {
            // Call base function
            base.OnAbilityRemove();
        }

        private IEnumerator Regen()
        {
            Healing = true;
            PS.GainHp(1.0f);            
            yield return new WaitForSecondsRealtime(1);
            Healing = false;
        }
    }
}