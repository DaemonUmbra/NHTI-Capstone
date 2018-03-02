using System.Collections;
using UnityEngine;

/*
 * Programmer Assigned: Steven Zachary
 * Power-up: Panic Loafers
 * Description: After being damaged movement speed is increased by X% for Y seconds,
 *              each additional stack of this powerup increases the X and Y values.
 */

namespace Powerups
{
    public class Powerup_PanicLoafers : PassiveAbility
    {
        [HideInInspector]
        public PlayerStats PS;

        public float WalkSpeed;
        public float Health;

        public int Instances;

        public override void OnAbilityAdd()
        {
            Name = "Panic Loafers"; // Setting Name of power up
            Debug.Log(Name + " Added");
            PS = GetComponent<PlayerStats>();
            WalkSpeed = PS.WalkSpeed; // Get current walkspeed to save for later use
            Health = PS.CurrentHp; // Get current health to keep track of damage

            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            PS.WalkSpeed = WalkSpeed; // In the event the powerup is removed part way through Panic Time
            base.OnAbilityRemove(); // Remove the ability
        }

        public override void OnUpdate()
        {
            if (PS.CurrentHp < Health) // If player is damaged
            {
                Health = PS.CurrentHp; // Health variable is set to player's current health
                if (Instances == 0)
                {
                    WalkSpeed = PS.WalkSpeed; // WalkSpeed is set to the player's WalkSpeed at the time of being damaged
                }
                PS.WalkSpeed = PS.WalkSpeed + 7; 
                Instances = Instances + 1; // Adds 1 to instances
                if (Instances == 1)
                {
                    StartCoroutine(PanicTime()); // Begin coroutine for timer
                }
            }
            else if (PS.CurrentHp > Health) // If player gains health
            {
                Health = PS.CurrentHp; // Health variable is set to player's current health
            }

            base.OnUpdate();
        }

        private IEnumerator PanicTime() // Timer for running
        {
            for (int i = 0; i < Instances; i++)
            {
                yield return new WaitForSecondsRealtime(3); // Wait for 3 seconds
            }
            Instances = 0;
            PS.WalkSpeed = WalkSpeed; // Set player walkspeed back to normal
        }
    }
}