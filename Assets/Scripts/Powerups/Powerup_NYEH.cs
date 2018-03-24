using System.Collections;
using UnityEngine;

namespace Powerups
{
    internal class Powerup_NYEH : ActiveAbility
    {
        private AudioManager audioManager;
        private AudioSource audioSource;
        private AudioClip nyeh;

        public float nyehVolume = 1f;

        public override void OnAbilityAdd()
        {
            //*** Handled by base ca pv.RPC("NYEH_AddAbility", PhotonTargets.All);
            audioManager = gameObject.GetComponent<AudioManager>();
            Name = "NYEH!";
            Cooldown = 0f;
            audioSource = audioManager.GetNewAudioSource(Name);
            audioSource.playOnAwake = false;
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
            audioManager.DeleteAudioSource(Name);
            PlayerShoot pShoot = gameObject.GetComponent<PlayerShoot>();
            pShoot.shoot -= TryActivate;
            base.OnAbilityRemove();
        }

        protected override void Activate()
        {
            base.Activate();
            {
                Debug.Log(photonView.owner.NickName + ": NYEH!");
                gameObject.GetComponent<AudioManager>().PlayOneShot(Name, "NYEH!", nyehVolume);
            }
        }
    }
}