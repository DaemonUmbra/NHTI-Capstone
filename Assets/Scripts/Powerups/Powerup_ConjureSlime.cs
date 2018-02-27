using System.Collections;
using UnityEngine;

namespace Powerups
{
    [RequireComponent(typeof(LineRenderer))]
    public class Powerup_ConjureSlime : ActiveAbility
    {
        private float CDstart;
        private bool onCooldown = false, CurrentlyActive = false;
        private PlayerShoot pShoot;

        public override void OnAbilityAdd()
        {
            // Set name
            Name = "ConjureSlime";
            Debug.Log(Name + " Added");

            // Add new shoot function to delegate
            pShoot = GetComponent<PlayerShoot>();
            if (pShoot)
            {
                Debug.Log("Slime Added to Shoot Delegate");
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