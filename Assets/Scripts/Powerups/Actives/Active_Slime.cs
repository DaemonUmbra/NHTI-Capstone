using UnityEngine;

///summary
 /*
Developers and Contributors: Ian Cahoon

Information
    Name: Slime
    Type: Active
    Effect: creates a small puddle of slime that plays can bounce off of
    Tier: Uncommon
  */
///summary

namespace Powerups
{
    public class Active_Slime : ActiveAbility
    {
        private float CDstart;
        private bool onCooldown = false, Active = false;
        private PlayerShoot pShoot;
        private float force = 20f;
        private float offset = 2, duration = 7f;

        private SlimeBall slime;
        private string prefab = "SlimeBall";

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            Name = "Slime";
            //TODO: Slime Icon
            Icon = Resources.Load<Sprite>("Images/Slime");
            slime = Resources.Load<SlimeBall>("Projectiles/" + prefab);
            Tier = PowerupTier.Uncommon;
            Cooldown = 7;
        }



        public override void OnAbilityAdd()
        {
            Debug.Log(Name + " Added");
            pShoot = GetComponent<PlayerShoot>();
            base.OnAbilityAdd();
        }

        public override void OnUpdate()
        {
            /*** You only need to do this check when you are changing player stats
             *   Or doing something else that is already networked. -BN
            if (!photonView.isMine)
            {
                return;
            }
            */
            base.OnUpdate();
        }

        public override void OnAbilityRemove()
        {
            // Call base function
            base.OnAbilityRemove();
        }

        protected override void Activate()
        {

            // You can use this now for projectile shooting, I made slimeball a projectile
            pShoot.Shoot(slime);
            base.Activate();
            
            
            /*
            if (!photonView.isMine)
            {
                return;
            }
            
            Vector3 mp = Input.mousePosition;
            Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(mp);
            Camera cam = transform.GetComponent<CameraController>().cam;
            Vector3 rot = pShoot.OffsetPoint.rotation.eulerAngles;
            GameObject _slime = PhotonNetwork.Instantiate("SlimeBall", transform.position + (transform.up * 1.5f) + (transform.forward * 3), Quaternion.Euler(rot), 0);
            //_slime.transform.LookAt(mouseLocation);
            Rigidbody rb = _slime.GetComponent<Rigidbody>();

            rb.velocity = (_slime.transform.forward + (_slime.transform.up / 5)) * force;
            */
            
        }
    }
}