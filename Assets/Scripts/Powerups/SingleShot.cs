using UnityEngine;

namespace Powerups
{
    public class SingleShot : ActiveAbility
    {
        private PlayerShoot pShoot;

        public override void OnAbilityAdd()
        {
            // Set name
            Name = "SingleShot";
            Debug.Log(Name + " Added");

            // Add new shoot function to delegate
            pShoot = GetComponent<PlayerShoot>();
            if (pShoot)
            {
                Debug.Log("Single Shot Added to Shoot Delegate");
                pShoot.shoot += TryActivate;
            }
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
            base.Activate();
            GameObject _proj = PhotonNetwork.Instantiate(pShoot.projectile.name, transform.position, transform.rotation, 0);
            _proj.GetComponent<Projectile>().IgnorePlayer(gameObject);
        }
    }
}