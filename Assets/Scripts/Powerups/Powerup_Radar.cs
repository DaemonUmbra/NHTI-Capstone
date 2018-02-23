using UnityEngine;

namespace Powerups
{
    public class Powerup_Radar : PassiveAbility
    {
        /*
        * Programmer Assigned: Steven Zachary
        * Power-up: Radar
        * Description: A soft beep, beeps more urgently as you approach a rare or higher powerup.
        */

        public Pickup[] pickUp;
        public AudioSource audioSource;
        public AudioClip bleep; // The bleeps, the sweeps and the creeps

        // Use this for initialization
        private void Start()
        {
            pickUp = FindObjectsOfType<Pickup>();
            audioSource = GetComponent<AudioSource>();
        }

        public override void OnAbilityAdd() // Function for adding ability to player
        {
            Name = "Radar";
            Debug.Log(Name + " added!");
            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove() // Function for removing ability from player
        {
            base.OnAbilityRemove();
        }

        public override void OnUpdate() // Update function
        {
            base.OnUpdate();
        }
    }
}