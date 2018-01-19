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

        Powerup_NYEH()
        {
            if(!gameObject.GetComponent<AudioSource>())
            {
                gameObject.AddComponent<AudioSource>();
            }
            audioSource = gameObject.GetComponent<AudioSource>();
            nyeh = Resources.Load("/Sounds/NYEH.wav") as AudioClip;
        }

        public override void Activate()
        {
            audioSource.PlayOneShot(nyeh);
        }

        public override void OnAbilityAdd()
        {
            throw new NotImplementedException();
        }

        public override void OnUpdate()
        {
            throw new NotImplementedException();
        }
    }
}
