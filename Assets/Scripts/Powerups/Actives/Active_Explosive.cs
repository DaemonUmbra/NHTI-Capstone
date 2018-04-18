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
            explosive = Resources.Load<Bomb>("Bomb");
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
                //GameObject _proj = PhotonNetwork.Instantiate("Bomb", pShoot.OffsetPoint.position, pShoot.OffsetPoint.rotation, 0);
                photonView.RPC("RPC_ShootBomb", PhotonTargets.All, pShoot.OffsetPoint.position, pShoot.OffsetPoint.rotation.eulerAngles);
            }
            base.Activate();
        }
        [PunRPC]
        private void RPC_ShootBomb(Vector3 position, Vector3 rotation)
        {
            Bomb b = Instantiate(explosive, position, Quaternion.Euler(rotation));
            b.Shoot(gameObject);
        }
    }
}