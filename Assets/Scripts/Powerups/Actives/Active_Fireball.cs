using UnityEngine;

namespace Powerups
{
    public class Active_Fireball : ActiveAbility
    {
        private PlayerShoot pShoot;
        private Fireball fireball;

        public readonly Vector3 PosOffset = new Vector3(0, 2, 0);

        public readonly Vector3 RotOffset = new Vector3(0, 0, 0);

        private void Awake()
        {
            _name = "Fireball";
            Icon = Resources.Load<Sprite>("Images/Fireball");
            Tier = PowerupTier.Common;
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
            //fireball = Resources.Load<Projectile_Fireball>("Fireball");
            // Call base function
            

            if(photonView.isMine)
            {
                GameObject _proj = PhotonNetwork.Instantiate("Fireball", transform.position + PosOffset, Quaternion.LookRotation(transform.rotation.eulerAngles + RotOffset), 0);
            }
            base.Activate();
        }
    }
}