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
            /* 

            pickUp = FindObjectsOfType<Pickup>();
            ElementsInArray = pickUp.Length;

            for(int i = 0; i < ElementsInArray; i++) // Find the object closest to the player
            {
                if(Vector3.Distance(transform.position, pickUp[i].transform.position) < Vector3.Distance(transform.position, pickUp[Closest].transform.position))
                {
                    Closest = i;
                }
            }

            DistanceToTarget = Vector3.Distance(transform.position, pickUp[Closest].transform.position); // Sets Distance to the closest power up every frame

            // ** Set volume based on distance from closest power up. The further away the quieter the volume **

            if(DistanceToTarget > 50)
            {
                audioSource.volume = 25f;
            }

            else if(DistanceToTarget < 50 && DistanceToTarget > 25)
            {
                audioSource.volume = 50f;
            }

            else if(DistanceToTarget < 25 && DistanceToTarget > 10)
            {
                audioSource.volume = 75f;
            }

            else if(DistanceToTarget < 10)
            {
                audioSource.volume = 100f;
            }

            */

            base.OnUpdate();
        }
    }
}