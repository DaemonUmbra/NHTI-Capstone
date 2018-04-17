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
        private float offset = 2, duration = 7f;

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            Name = "Slime";
            //TODO: Slime Icon
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


            base.Activate();
        }
    }
}