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

        public void OnShoot()
        {
            Debug.Log("NYEH!");
            audioSource.PlayOneShot(nyeh, nyehVolume);
        }

        public override void OnAbilityAdd()
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
            pShoot.shoot += OnShoot;
        }

        public override void OnAbilityRemove()
        {
            base.OnAbilityRemove();
            PlayerShoot pShoot = gameObject.GetComponent<PlayerShoot>();
            pShoot.shoot -= OnShoot;
        }

        public override void OnUpdate()
        {
            //NYEH! does not tick
        }

        public override void Activate()
        {
            //When is Activate() called again?
            throw new NotImplementedException();
        }
    }
}
