using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Powerups {
    public class Active_Explosive : ActiveAbility {

        private PlayerShoot pShoot;

        private void Awake()
        {

            Name = "Explosion";
        }

        public override void OnAbilityAdd()
        {

            Debug.Log(Name + " Added");

            pShoot = GetComponent<PlayerShoot>();
            if (pShoot)
            {
                Debug.Log("Explosion Added to Shoot Delegate");
                pShoot.shoot += TryActivate;
            }
            // Call base function
            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            // Remove shoot delegate
            if (pShoot)
            {
                pShoot.shoot -= TryActivate;
            }
            pShoot = null;

            // Call base function
            base.OnAbilityRemove();
        }

        protected override void Activate()
        {
            if (photonView.isMine)
            {
                GameObject _proj = PhotonNetwork.Instantiate("Explosive", transform.position, transform.rotation, 0);
            }
            base.Activate();
        }
    }
}