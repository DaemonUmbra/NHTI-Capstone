using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class Passive_HighJump : PassiveAbility
    {
        public float JumpBoost = 2;
        private PlayerController pCon;
        private AudioManager audioManager;
        private AudioSource audioSource;
        private AudioClip jumpClip;
         
        public float jumpVolume = 1f;

        private void Awake()
        {
            Name = "High Jump";
            Icon = Resources.Load<Sprite>("Images/High Jump");
            Tier = PowerupTier.Uncommon;

       
        }
        public override void OnAbilityAdd()
        {
            
            if (photonView.isMine)
            {
                GetComponent<PlayerMotor>().JumpMultiplier += JumpBoost;
                pCon = gameObject.GetComponent<PlayerController>();
            }
            audioManager = gameObject.GetComponent<AudioManager>();
            audioSource = audioManager.GetNewAudioSource(Name);
            audioSource.playOnAwake = false;

            base.OnAbilityAdd();
        }

        public override void OnUpdate()
        {
            if (Input.GetKey(KeyCode.Space) && pCon.isGrounded == true)
            {
                photonView.RPC("RPC_Activate_HighJump", PhotonTargets.All);
            }
        }

        public override void OnAbilityRemove()
        {
            if (photonView.isMine)
            {
                GetComponent<PlayerMotor>().JumpMultiplier -= JumpBoost;
            }
            audioManager.DeleteAudioSource(Name);

            base.OnAbilityRemove();
        }

        [PunRPC]
        protected void RPC_Activate_HighJump()
        {
            {
                gameObject.GetComponent<AudioManager>().PlayOneShot(Name, "Jump", jumpVolume);
            }
        }
    }
}
