using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Powerups
{
    public class Active_BubbleWand : ActiveAbility
    {
        private PlayerShoot pShoot;

        private void Awake()
        {
            Name = "Bubble Wand";
            Icon = Resources.Load<Sprite>("Images/Bubble Wand");
            Tier = PowerupTier.Uncommon;
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
                GameObject _proj = PhotonNetwork.Instantiate("Bubble", pShoot.OffsetPoint.position, pShoot.OffsetPoint.rotation, 0);
            }
            base.Activate();
        }
    }
}