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
        private float force = 30f;
        private float offset = 2, duration = 7f;

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            Name = "Slime";
            //TODO: Slime Icon
            Icon = Resources.Load<Sprite>("Images/Slime");
            Tier = PowerupTier.Uncommon;

        }



        public override void OnAbilityAdd()
        {
            Cooldown = 7;
            Debug.Log(Name + " Added");
            
            base.OnAbilityAdd();
        }

        public override void OnUpdate()
        {
            if (!photonView.isMine)
            {
                return;
            }
            base.OnUpdate();
        }

        public override void OnAbilityRemove()
        {
            // Call base function
            base.OnAbilityRemove();
        }

        protected override void Activate()
        {
            if (!photonView.isMine)
            {
                return;
            }
            Vector3 mp = Input.mousePosition;
            Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(mp);
            Camera cam = transform.GetComponent<CameraController>().cam;
            GameObject _slime = PhotonNetwork.Instantiate("SlimeBall", transform.position + (transform.up * 2) + (transform.forward * 3), Quaternion.identity, 0);
            _slime.transform.LookAt(mouseLocation);
            Rigidbody rb = _slime.GetComponent<Rigidbody>();

            rb.velocity = (-_slime.transform.forward + (_slime.transform.up / 4)) * force;

            base.Activate();
        }
    }
}