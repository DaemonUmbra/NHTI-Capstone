using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Powerups
{
    [RequireComponent(typeof(AudioSource))]
    class Powerup_NYEH : ActiveAbility
    {
        AudioSource audioSource;
        AudioClip nyeh;

        public float nyehVolume = 0.5f;

        public override void OnAbilityAdd()
        {
            //*** Handled by base ca pv.RPC("NYEH_AddAbility", PhotonTargets.All);

            Name = "NYEH!";
            if (!gameObject.GetComponent<AudioSource>())
            {
                gameObject.AddComponent<AudioSource>();
            }
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            nyeh = Resources.Load("Sounds/NYEH") as AudioClip;
            if (!nyeh)
            {
                Debug.LogWarning("NYEH not found in /Resources/Sounds/ folder!");
            }
            PlayerShoot pShoot = gameObject.GetComponent<PlayerShoot>();
            pShoot.shoot += RPC_Activate;

            base.OnAbilityAdd();
        }

        /*** Handled by base class
        [PunRPC]
        void NYEH_AddAbility()
        {
            Name = "NYEH!";
            if (!gameObject.GetComponent<AudioSource>())
            {
                gameObject.AddComponent<AudioSource>();
            }
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            nyeh = Resources.Load("Sounds/NYEH") as AudioClip;
            if (!nyeh)
            {
                Debug.LogWarning("NYEH not found in /Resources/Sounds/ folder!");
            }
            PlayerShoot pShoot = gameObject.GetComponent<PlayerShoot>();
            pShoot.shoot += RPC_Activate;
        }

        [PunRPC]
        void NYEH_RemoveAbility()
        {
            base.OnAbilityRemove();
            PlayerShoot pShoot = gameObject.GetComponent<PlayerShoot>();
            pShoot.shoot -= RPC_Activate;
        }

        [PunRPC]
        void NYEH_Activate()
        {
            Debug.Log("NYEH!");
            audioSource.PlayOneShot(nyeh, nyehVolume);
        }
        */

        public override void OnAbilityRemove()
        {
            base.OnAbilityRemove();
            PlayerShoot pShoot = gameObject.GetComponent<PlayerShoot>();
            pShoot.shoot -= RPC_Activate;
        }

        protected override void RPC_Activate()
        {
            Debug.Log("NYEH!");
            audioSource.PlayOneShot(nyeh, nyehVolume);
        }
    }
}
