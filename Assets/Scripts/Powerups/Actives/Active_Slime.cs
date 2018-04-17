using UnityEngine;

///summary
 /*
Developers and Contributors: Ian Cahoon

Information
    Name: Slime
    Type: Active
    Effect: creates a small ball of slime that knocks back players;
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
        private float offset = 2, duration = 7f;
        private Vector3 Offset = new Vector3(0, 2, 0);

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            Cooldown = 7;
            _name = "Slime";
            //TODO: Slime Icon
            Tier = PowerupTier.Uncommon;
        }



        public override void OnAbilityAdd()
        {
            
            Debug.Log(_name + " Added");
            
            base.OnAbilityAdd();
        }

        public override void OnUpdate()
        {
            if (!photonView.isMine)
            {
                return;
            }
            if (Active)
            {
                GameObject pool = GameObject.Find("SP");
                if (pool != null)
                {
                    return;
                }
                Active = false;
                //onCooldown = true;
                //CDstart = Time.time;
            }
            /*if (onCooldown)
            {
                CoolDown(Time.time, duration);
            }*/

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
            GameObject slime = PhotonNetwork.Instantiate("SlimeBall", transform.position + Offset, Quaternion.identity, 0);

            base.Activate();
        }

        /*private void CoolDown(float currentTime, float duration)
        {
            if (currentTime >= CDstart + duration)
            {
                Debug.Log("Slime is off cooldown");
                onCooldown = false;
            }
        }*/
    }
}