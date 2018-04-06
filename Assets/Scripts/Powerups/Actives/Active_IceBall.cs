using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class Active_IceBall : ActiveAbility
    {
        private PlayerShoot pShoot;
        private Ice iceball;

        private void Awake()
        {
            Name = "Ice Ball";
            //TODO: Iceball Icon
            Tier = PowerupTier.Uncommon;
        }

        public override void OnAbilityAdd()
        {
            Debug.Log(Name + " Added");

            // Call base function
            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            // Call base function
            base.OnAbilityRemove();
        }

        protected override void Activate()
        {
            if (photonView.isMine)
            {
                GameObject _proj = PhotonNetwork.Instantiate("Bullet 1", transform.position, transform.rotation, 0);
            }
            base.Activate();
        }
    }
}