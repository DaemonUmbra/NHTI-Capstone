using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Powerups
{
    class Powerup_NYEH : ActiveAbility
    {
        AudioSource audioSource;
        AudioClip nyeh;

        public void Awake()
        {
            if (!gameObject.GetComponent<AudioSource>())
            {
                gameObject.AddComponent<AudioSource>();
            }
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            nyeh = Resources.Load("/Sounds/NYEH.wav") as AudioClip;
        }

        public void OnShoot()
        {
            Debug.Log("NYEH!");
            audioSource.PlayOneShot(nyeh);
        }

        public override void OnAbilityAdd()
        {
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
            
        }

        public override void Activate()
        {
            throw new NotImplementedException();
        }
    }
}
