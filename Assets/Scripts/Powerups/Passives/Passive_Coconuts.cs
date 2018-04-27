using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    [RequireComponent(typeof(AudioSource))]
    public class Passive_Coconuts : PassiveAbility
    {

        public float SpeedBoost = .25f;
        public float DeadZone = .2f;

        private AudioSource AudioSource;
        private AudioClip CoconutSound;
        private PlayerStats PlayerStats;
        private void Awake()
        {
            Name = "Coconuts";
            //TODO: Coconuts Icon
            Tier = PowerupTier.Uncommon;
        }
        
        public override void OnAbilityAdd()
        {
            Name = "A Lovely Pair of Coconuts";
            CoconutSound = Resources.Load<AudioClip>("Audio/Coconuts");
            AudioSource = GetComponent<AudioSource>();
            PlayerStats = GetComponent<PlayerStats>();
            PlayerStats.AddSpeedBoost(Name, SpeedBoost);
            AudioSource.loop = true;
            AudioSource.clip = CoconutSound;
            AudioSource.Play();
            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            AudioSource.Stop();
            PlayerStats.RemoveSpeedBoost(Name);
            base.OnAbilityRemove();
        }
    }
}
