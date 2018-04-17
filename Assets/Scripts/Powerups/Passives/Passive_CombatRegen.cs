using System.Collections;
using UnityEngine;

/*
 * Programmer Assigned: Steven Zachary
 * Power-up: Combat Regeneration
 * Description: Constantly restores a small amount of health to the player after a set amount of time.
 */

namespace Powerups
{
    public class Passive_CombatRegen : PassiveAbility
    {
        [HideInInspector]
        public PlayerStats PS;
        public bool Healing;
        private void Awake()
        {
            Name = "Combat Regeneration";
            Icon = Resources.Load<Sprite>("Images/Combat Regen");
            Tier = PowerupTier.Rare;
        }
        public override void OnAbilityAdd()
        {
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
            PS.GainHp(3.0f);            
            yield return new WaitForSecondsRealtime(5);
            Healing = false;
        }
    }
}