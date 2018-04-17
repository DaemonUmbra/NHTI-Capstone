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
            Icon = Resources.Load<Sprite>("Images/NYEH");
            Tier = PowerupTier.Uncommon;
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
        protected void RPC_Activate_NYEH1()
        {
            Debug.Log(photonView.owner.NickName + ": NYEH!");
            gameObject.GetComponent<AudioManager>().PlayOneShot(Name, "NYEH!", nyehVolume);
        }

        [PunRPC]
        protected void RPC_Activate_NYEH2()
        {
            Debug.Log(photonView.owner.NickName + ": NYEH!");
            gameObject.GetComponent<AudioManager>().PlayOneShot(Name, "NYEH!2", nyehVolume);
        }

        public void TryActivate()
        {
            if (photonView.isMine)
            {
                switch (Random.Range(1, 3))
                {
                    case 1:
                        {
                            photonView.RPC("RPC_Activate_NYEH1", PhotonTargets.All);
                            break;
                        }
                    case 2:
                        {
                            photonView.RPC("RPC_Activate_NYEH2", PhotonTargets.All);
                            break;
                        }
                    default:
                        {
                            photonView.RPC("RPC_Activate_NYEH1", PhotonTargets.All);
                            break;
                        }
                }
                
            }
        }
    }
}