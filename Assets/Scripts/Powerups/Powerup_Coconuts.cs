using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    [RequireComponent(typeof(AudioSource))]
    public class Powerup_Coconuts : PassiveAbility
    {

        public float SpeedBoost = .25f;
        public float DeadZone = .2f;

        private AudioSource AudioSource;
        private AudioClip CoconutSound;
        private PlayerStats PlayerStats;

        public override void OnUpdate()
        {
            ////If moving
            //if (Mathf.Abs(Input.GetAxis("Horizontal")) > DeadZone || Mathf.Abs(Input.GetAxis("Vertical")) > DeadZone)
            //{
            //    //If nto playing
            //    if (!AudioSource.isPlaying)
            //    {
            //        AudioSource.Play();
            //    }
            //}
            ////If standing still
            //else
            //{
            //    //If playing
            //    if (AudioSource.isPlaying)
            //    {
            //        AudioSource.Stop();
            //    }
            //}
            //base.OnUpdate();
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
