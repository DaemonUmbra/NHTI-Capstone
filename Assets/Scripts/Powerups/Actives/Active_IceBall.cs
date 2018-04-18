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
            Icon = Resources.Load<Sprite>("Images/Ice Ball");
            iceball = Resources.Load<Ice>("Iceball");
            Tier = PowerupTier.Common;
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
                //GameObject _proj = PhotonNetwork.Instantiate("Iceball", pShoot.OffsetPoint.position, pShoot.OffsetPoint.rotation, 0);
                photonView.RPC("RPC_ShootIceball", PhotonTargets.All, pShoot.OffsetPoint.position, pShoot.OffsetPoint.rotation.eulerAngles);
            }
            base.Activate();
        }

        [PunRPC]
        private void RPC_ShootIceball(Vector3 position, Vector3 rotation)
        {

            Ice ice = Instantiate(iceball, position, Quaternion.Euler(rotation));
            ice.Shoot(gameObject);
        }
    }
}