using System.Collections;
using UnityEngine;

namespace Powerups
{
    internal class Passive_NYEH : PassiveAbility
    {
        private AudioManager audioManager;
        private AudioSource audioSource;
        private AudioClip nyeh;

        public float nyehVolume = 1f;

        public override void OnAbilityAdd()
        {
            audioManager = gameObject.GetComponent<AudioManager>();
            Name = "NYEH!";
            audioSource = audioManager.GetNewAudioSource(Name);
            audioSource.playOnAwake = false;
            PlayerShoot pShoot = gameObject.GetComponent<PlayerShoot>();
            pShoot.shoot += TryActivate;
            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            audioManager.DeleteAudioSource(Name);
            PlayerShoot pShoot = gameObject.GetComponent<PlayerShoot>();
            pShoot.shoot -= TryActivate;
            base.OnAbilityRemove();
        }

        [PunRPC]
        protected void RPC_Activate_NYEH()
        {
            {
                Debug.Log(photonView.owner.NickName + ": NYEH!");
                gameObject.GetComponent<AudioManager>().PlayOneShot(Name, "NYEH!", nyehVolume);
            }
        }

        public void TryActivate()
        {
            if (photonView.isMine)
            {
                photonView.RPC("RPC_Activate_NYEH", PhotonTargets.All);
            }
        }
    }
}