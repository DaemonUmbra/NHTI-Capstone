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
        private void Awake()
        {
            Name = "NYEH!";
        }
        public override void OnAbilityAdd()
        {
            audioManager = gameObject.GetComponent<AudioManager>();
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
                switch(Random.Range(1, 3))
                {
                    case 1:
                        {
                            gameObject.GetComponent<AudioManager>().PlayOneShot(Name, "NYEH!", nyehVolume);
                            break;
                        }
                    case 2:
                        {
                            gameObject.GetComponent<AudioManager>().PlayOneShot(Name, "NYEH!2", nyehVolume);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                
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