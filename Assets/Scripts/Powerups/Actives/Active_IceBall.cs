using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class Active_IceBall : ActiveAbility
    {
        private PlayerShoot pShoot;
        private Ice iceball;

        string prefab = "Ice";
        private void Awake()
        {
            Name = "Ice Ball";
            Icon = Resources.Load<Sprite>("Images/Ice Ball");
            iceball = Resources.Load<Ice>("Projectiles/" + prefab);
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
            pShoot.Shoot(iceball);
            base.Activate();
        }
    }
}