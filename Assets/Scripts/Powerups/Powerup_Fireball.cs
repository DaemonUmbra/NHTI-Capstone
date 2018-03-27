using UnityEngine;

namespace Powerups
{
    public class Powerup_Fireball : ActiveAbility
    {
        private PlayerShoot pShoot;
        private Projectile_Fireball fireball;

        private void Awake()
        {
            
            Name = "Fireball";
        }

        public override void OnAbilityAdd()
        {
            
            Debug.Log(Name + " Added");

            pShoot = GetComponent<PlayerShoot>();
            if (pShoot)
            {
                Debug.Log("Fireball Added to Shoot Delegate");
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
            //fireball = Resources.Load<Projectile_Fireball>("Fireball");
            // Call base function
            

            if(photonView.isMine)
            {
                GameObject _proj = PhotonNetwork.Instantiate("Fireball", transform.position, transform.rotation, 0);
            }
            base.Activate();
        }
    }
}