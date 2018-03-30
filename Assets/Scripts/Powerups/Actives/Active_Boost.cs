using System.Collections;
using UnityEngine;

/*
 * Programmer Assigned: Steven Zachary
 * Power-up: Boost
 * Description: Grants the player the ability to dash forward. Short-moderate cooldown.
 */

namespace Powerups
{
    public class Active_Boost : ActiveAbility
    {
        [HideInInspector]
        public PlayerStats PS;

        public float WalkSpeed;
        public bool Boosted = false;
        

        public override void OnAbilityAdd()
        {
            Cooldown = 5;
            Name = "Boost";
            Debug.Log(Name + "  Added");
            PS = GetComponent<PlayerStats>();
            Boosted = false;

            // Call base function
            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            // Call base function
            base.OnAbilityRemove();
        }

        protected override void Activate()
        {
            Debug.Log("Activate Test - Boost");

            if (Boosted == false)
            {
                StartCoroutine(FastTime());
            }

            // Call base function
            base.Activate();

        }

        IEnumerator FastTime()
        {
            Boosted = true;
            WalkSpeed = PS.WalkSpeed;
            PS.AddSpeedMultipler(Name, 2);
            yield return new WaitForSecondsRealtime(1.0f);
            PS.RemoveSpeedMultiplier(Name);
            yield return new WaitForSecondsRealtime(3.0f);
            Boosted = false;            
        }
    }
}