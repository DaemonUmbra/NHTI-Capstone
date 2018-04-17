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
<<<<<<< HEAD
            Name = "Slime";
=======
            Cooldown = 7;
            _name = "Slime";
>>>>>>> 021b0cc552aa4304870a28e3df6d6e80a9d9819a
            //TODO: Slime Icon
            Tier = PowerupTier.Uncommon;
        }



        public override void OnAbilityAdd()
        {
<<<<<<< HEAD
            Cooldown = 7;
            Debug.Log(Name + " Added");
=======
            
            Debug.Log(_name + " Added");
>>>>>>> 021b0cc552aa4304870a28e3df6d6e80a9d9819a
            
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
            if (Active)
            {
                return;
            }
            if (onCooldown)
            {
                float remaining = duration - (Time.time - CDstart);
                Debug.Log("Slime is on cooldown, time remaining: " + remaining);
                return;
            }
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Debug.Log(player.name);
            GameObject slime = Resources.Load("Prefabs/SlimePool") as GameObject;

            //Instantiate(slime, (player.transform.forward * offset) + player.transform.position, transform.rotation);
            GameObject rayOrigin = GameObject.Find("BasicPlayer/Gun"); //Needs to be changed to local player when networked
            Vector3 mp = Input.mousePosition;
            mp.z = 10;
            Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(mp);
            Vector3 targetVector = mouseLocation;

            Ray snipeRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit endPoint;
            if (Physics.Raycast(snipeRay, out endPoint))
            {
                Debug.Log(endPoint.transform.gameObject.name);
                GameObject slimepool = Instantiate(slime, endPoint.point, transform.rotation);
                slimepool.transform.localEulerAngles = Vector3.zero;
                slimepool.name = "SP";
                Destroy(slimepool, 7f);
                Active = true;
            }

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