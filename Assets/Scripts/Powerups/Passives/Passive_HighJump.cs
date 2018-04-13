using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class Passive_HighJump : PassiveAbility
    {
        public float JumpBoost = 2;
        PlayerController controller;
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
                controller = gameObject.GetComponent<PlayerController>();
            }
            audioManager = gameObject.GetComponent<AudioManager>();
            audioSource = audioManager.GetNewAudioSource(Name);
            audioSource.playOnAwake = false;
            
            base.OnAbilityAdd();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded == true)
            {
                photonView.RPC("RPC_Activate_HighJump", PhotonTargets.All);
            }
        }

        public override void OnAbilityRemove()
        {
            base.OnAbilityRemove();
            if (photonView.isMine)
            {
                GetComponent<PlayerMotor>().JumpMultiplier -= JumpBoost;
            }
        }


        [PunRPC]
        protected void RPC_Activate_HighJump()
        {
            {
                Debug.Log(photonView.owner.NickName + ": jump!");
                gameObject.GetComponent<AudioManager>().PlayOneShot(Name, "Highjump", jumpVolume);
            }
        }
    }
}
