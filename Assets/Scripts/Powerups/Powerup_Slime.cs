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
    public class Powerup_Slime : ActiveAbility
    {
        private float CDstart;
        private bool onCooldown = false, Active = false;
        private PlayerShoot pShoot;
        private float offset = 2, duration = 7f;

        public override void OnAbilityAdd()
        {
            // Set name
            Name = "Slime";
            Debug.Log(Name + " Added");

            // Add new shoot function to delegate
            pShoot = GetComponent<PlayerShoot>();
            if (pShoot)
            {
                //Debug.Log("Slime Added to Shoot Delegate");
                pShoot.shoot += TryActivate;
            }

            base.OnAbilityAdd();
        }

        public override void OnUpdate()
        {
            if (Active)
            {
                GameObject pool = GameObject.Find("SP");
                if (pool != null)
                {
                    return;
                }
                Active = false;
                onCooldown = true;
                CDstart = Time.time;
            }
            if (onCooldown)
            {
                CoolDown(Time.time, duration);
            }

            base.OnUpdate();
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

        private void CoolDown(float currentTime, float duration)
        {
            if (currentTime >= CDstart + duration)
            {
                Debug.Log("Slime is off cooldown");
                onCooldown = false;
            }
        }
    }
}