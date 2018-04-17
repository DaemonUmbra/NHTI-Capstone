using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Powerups {
    public class Active_Explosive : ActiveAbility {

        private PlayerShoot pShoot;
        private Bomb explosive;

        public readonly Vector3 PosOffset = new Vector3(0, 2, 0);

        public readonly Vector3 RotOffset = new Vector3(0, 0, 0);

        private void Awake()
        {
            _name = "Explosion";
            Icon = Resources.Load<Sprite>("Images/Explosive Rounds");
            Tier = PowerupTier.Uncommon;
        }

        public override void OnAbilityAdd()
        {

            Debug.Log(_name + " Added");

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
                GameObject _proj = PhotonNetwork.Instantiate("Bullet 2", transform.position + PosOffset, Quaternion.LookRotation(transform.rotation.eulerAngles + RotOffset), 0);
            }
            base.Activate();
        }
    }
}