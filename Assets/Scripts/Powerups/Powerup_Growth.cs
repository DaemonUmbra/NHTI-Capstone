﻿using UnityEngine;

namespace Powerups
{
    /// <summary>
    /// <para>Author: Colby Prince</para>
    /// <para>Power-Up Name: Growth</para>
    /// <para>Power-Up Category: Passive</para>
    /// <para>Power-Up Effect: Causes player to grow to a larger size, gaining a damage advantage and footfall AoE effect. Disadvantage would be easier to hit</para>
    /// <para>Power-Up Tier: Uncommon</para>
    /// <para>Power-Up Description:  Player model is larger,  gaining damage boost but same walk speed.</para>
    /// </summary>
    
    [RequireComponent(typeof(AudioSource))]
    public class Powerup_Growth : PassiveAbility
    {
        private Vector3 OriginalScale;
        public AudioClip FootFall;
        public float GrowthFactor = 2;
        public float DeadZone = .1f;

        private AudioSource AudioSource;
        private PlayerStats pStats;

        public float dmgMult = .5f;
        public float dmgAdd = 2f;

        public float LastFootfall = 0;

        public override void OnAbilityAdd()
        {
            pStats = gameObject.GetComponent<PlayerStats>();
            AudioSource = GetComponent<AudioSource>();
            FootFall = Resources.Load<AudioClip>("Audio/Growth_FootFall");
            AudioSource = GetComponent<AudioSource>();
            Name = "Growth";
            OriginalScale = transform.localScale;
            transform.localScale = OriginalScale * GrowthFactor;
            pStats.dmgMult *= dmgMult;
            pStats.dmgAdd += dmgAdd;
            base.OnAbilityAdd();

            /*** Handled by base class ***
            pv = PhotonView.Get(this);
            pv.RPC("Growth_AddAbility", PhotonTargets.All);
            */
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            //If moving
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > DeadZone || Mathf.Abs(Input.GetAxis("Vertical")) > DeadZone)
            {
                //If nto playing
                if (!AudioSource.isPlaying)
                {
                    AudioSource.Play();
                }
            }
            //If standing still
            else
            {
                //If playing
                if (AudioSource.isPlaying)
                {
                    AudioSource.Stop();
                }
            }
        }

        /*** Handled by base class ***
        [PunRPC]
        void Growth_AddAbility()
        {
            Name = "Growth";
            OriginalScale = transform.localScale;
            transform.localScale = OriginalScale * GrowthFactor;
        }
        [PunRPC]
        void Growth_RemoveAbility()
        {
            transform.localScale = OriginalScale;
        }
        */

        public override void OnAbilityRemove()
        {
            pStats.dmgMult /= dmgMult;
            pStats.dmgAdd -= dmgAdd;
            transform.localScale = OriginalScale;
            base.OnAbilityRemove();
        }
    }
}