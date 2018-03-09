using System.Collections;
using UnityEngine;

namespace Powerups
{
    internal class Powerup_NYEH : ActiveAbility
    {
        private AudioSource audioSource;
        private AudioClip nyeh;

        public float nyehVolume = 1f;

        public override void OnAbilityAdd()
        {
            //*** Handled by base ca pv.RPC("NYEH_AddAbility", PhotonTargets.All);

            Name = "NYEH!";
            Cooldown = 0f;
            audioSource = gameObject.GetComponent<AudioSource>();
            if (!audioSource)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            audioSource.playOnAwake = false;
            //GetComponent<PhotonView>().ObservedComponents.Add(audioSource);
            nyeh = Resources.Load("Sounds/NYEH") as AudioClip;
            if (!nyeh)
            {
                Debug.LogWarning("NYEH not found in /Resources/Sounds/ folder!");
            }
            PlayerShoot pShoot = gameObject.GetComponent<PlayerShoot>();
            pShoot.shoot += TryActivate;
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
            pShoot.shoot -= TryActivate;
        }

        protected override void Activate()
        {
            base.Activate();
            ;// if (photonView.isMine)
            {
                Debug.Log("NYEH!");
                gameObject.GetComponent<AudioSource>().PlayOneShot(nyeh, nyehVolume);
            }
        }
    }
}