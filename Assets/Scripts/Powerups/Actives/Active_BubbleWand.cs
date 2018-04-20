using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Powerups
{
    public class Active_BubbleWand : ActiveAbility
    {
        private PlayerShoot pShoot;
        private PlayerStats pStats;
        private Bubbles bubble;

        private string prefab = "Bubble";

        private void Awake()
        {
            Name = "Bubble Wand";
            Icon = Resources.Load<Sprite>("Images/Bubble Wand");
            bubble = Resources.Load<Bubbles>("Projectiles/" + prefab);
            Tier = PowerupTier.Uncommon;
        }

        public override void OnAbilityAdd()
        {

            Debug.Log(Name + " Added");
            pShoot = GetComponent<PlayerShoot>();
            pStats = GetComponent<PlayerStats>();

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
            pShoot.Shoot(prefab);
            base.Activate();
        }
    }
}