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
            Name = "Fireball";
            Icon = Resources.Load<Sprite>("Images/Fireball");
            fireball = Resources.Load<Fireball>("Fireball");
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
            if(photonView.isMine)
            {
                photonView.RPC("RPC_ShootFireball", PhotonTargets.All, pShoot.OffsetPoint.position, pShoot.OffsetPoint.rotation.eulerAngles);
            }
            base.Activate();
        }

        [PunRPC]
        private void RPC_ShootFireball(Vector3 position, Vector3 rotation)
        {

            Fireball fb = Instantiate(fireball, position, Quaternion.Euler(rotation));
            fb.Shoot(gameObject);
        }
    }
}