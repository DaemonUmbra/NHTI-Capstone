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

        PhotonView pv;

        public override void OnAbilityAdd()
        {
            pv.RPC("NYEH_AddAbility", PhotonTargets.All);
        }

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
            pShoot.shoot += Activate;
        }

        [PunRPC]
        void NYEH_RemoveAbility()
        {
            base.OnAbilityRemove();
            PlayerShoot pShoot = gameObject.GetComponent<PlayerShoot>();
            pShoot.shoot -= Activate;
        }

        [PunRPC]
        void NYEH_Activate()
        {
            Debug.Log("NYEH!");
            audioSource.PlayOneShot(nyeh, nyehVolume);
        }

        public override void OnAbilityRemove()
        {
            pv.RPC("NYEH_RemoveAbility", PhotonTargets.All);
        }

        public override void OnUpdate()
        {
            //NYEH! does not tick
        }

        public override void Activate()
        {
            pv.RPC("NYEH_Activate", PhotonTargets.All);
        }
    }
}
