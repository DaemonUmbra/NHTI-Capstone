using UnityEngine;

namespace Powerups
{
    [RequireComponent(typeof(AudioSource))]
    internal class Powerup_NYEH : ActiveAbility
    {
        private AudioSource audioSource;
        private AudioClip nyeh;

        public float nyehVolume = 0.5f;

        public override void OnAbilityAdd()
        {
            //*** Handled by base ca pv.RPC("NYEH_AddAbility", PhotonTargets.All);

            Name = "NYEH!";
            //Handled by the RequireComponent
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
            pShoot.shoot -= Activate;
        }

        protected override void Activate()
        {
            Debug.Log("NYEH!");
            audioSource.PlayOneShot(nyeh, nyehVolume);
        }
    }
}