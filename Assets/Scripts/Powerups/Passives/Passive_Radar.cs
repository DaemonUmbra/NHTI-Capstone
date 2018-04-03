using UnityEngine;

namespace Powerups
{
    [RequireComponent(typeof(AudioSource))]
    public class Passive_Radar : PassiveAbility
    {
        /*
        * Programmer Assigned: Steven Zachary
        * Power-up: Radar
        * Description: A soft beep, beeps more urgently as you approach a rare or higher powerup.
        */

        public Pickup[] pickUp;
        public AudioSource audioSource;
        public AudioClip bleep; // The bleeps, the sweeps and the creeps

        public float DistanceCheck;
        public float DistanceToTarget;
        public int ElementsInArray;
        public int Closest;

        // Use this for initialization
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            Closest = 0;
            Name = "Radar";
        }

        public override void OnAbilityAdd() // Function for adding ability to player
        {
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