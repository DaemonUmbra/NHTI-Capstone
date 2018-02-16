using UnityEngine;

namespace Powerups
{
    public class Powerup_Fireball : ActiveAbility
    {
        private PlayerShoot pShoot;

        public override void OnAbilityAdd()
        {
            Name = "Fireball";
            Debug.Log(Name + " Added");

            pShoot = GetComponent<PlayerShoot>();
            if (pShoot)
            {
                Debug.Log("Fireball Added to Shoot Delegate");
                pShoot.shoot += Activate;
            }
            // Call base function
            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            // Remove shoot delegate
            if (pShoot)
            {
                pShoot.shoot -= Activate;
            }
            pShoot = null;

            // Call base function
            base.OnAbilityRemove();
        }

        protected override void RPC_Activate()
        {
            // Call base function
            base.RPC_Activate();
        }
    }
}