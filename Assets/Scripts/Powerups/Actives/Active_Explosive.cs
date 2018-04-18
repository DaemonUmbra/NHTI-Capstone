using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Powerups {
    public class Active_Explosive : ActiveAbility {

        private PlayerShoot pShoot;
        private Bomb explosive;

        private void Awake()
        {
            Name = "Explosion";
            Icon = Resources.Load<Sprite>("Images/Explosive Rounds");
            Tier = PowerupTier.Uncommon;
            Cooldown = 2f;
        }

        public override void OnAbilityAdd()
        {

            Debug.Log(Name + " Added");
            pShoot = GetComponent<PlayerShoot>();
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
                GameObject _proj = PhotonNetwork.Instantiate("Bomb", pShoot.OffsetPoint.position, pShoot.OffsetPoint.rotation, 0);
            }
            base.Activate();
        }
    }
}