using System.Collections;
using UnityEngine;

namespace Powerups
{
    public class Active_ConjureSlime : ActiveAbility
    {
        private float CDstart;
        private bool onCooldown = false, CurrentlyActive = false;
        private PlayerShoot pShoot;

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            _name = "ConjureSlime";
            //TODO: Conjure Slime Icon
            Tier = PowerupTier.Uncommon;
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
            if (onCooldown)
            {
                Debug.Log("This ability is on cooldown" + "Start Time: " + CDstart);
                return;
            }
            //Debug.Log("Raycast Shot");
            GameObject rayOrigin = GameObject.Find("Player/Gun"); //Needs to be changed to local player when networked
            Vector3 mp = Input.mousePosition;
            mp.z = 10;
            Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(mp);
            Vector3 targetVector = mouseLocation;

            Ray snipeRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit endPoint;

            if (Physics.Raycast(snipeRay, out endPoint))
            {
                Debug.Log(endPoint.transform.gameObject.name);
                targetVector = endPoint.point;
            }
            else
            {
                Debug.Log("no object was hit");
            }

            StartCoroutine(VisualizeRaycast(rayOrigin, targetVector));

            // Call base class
            base.Activate();
        }

        private IEnumerator VisualizeRaycast(GameObject Origin, Vector3 targetLocation)
        {
            StartCoroutine(TriggerCoolDown());

            LineRenderer snipeLaser = Origin.GetComponent<LineRenderer>();
            snipeLaser.SetPosition(0, Origin.transform.position);
            snipeLaser.SetPosition(1, targetLocation);

            snipeLaser.enabled = true;
            yield return new WaitForSeconds(.2f);
            snipeLaser.enabled = false;
        }

        private IEnumerator TriggerCoolDown()
        {
            onCooldown = true;
            CDstart = Time.fixedTime;
            yield return new WaitForSeconds(12);
            onCooldown = false;
        }
    }
}
