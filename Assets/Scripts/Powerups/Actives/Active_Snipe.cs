using System.Collections;
using UnityEngine;

///summary
 /*
Developers and Contributors: Ian Cahoon

Information
    Name: Snipe
    Type: Active
    Effect: Basic fire turns into hitscan lightning bolts with increase damage but decreased fire rate
    Tier: Rare
  */
///summary

namespace Powerups
{
    public class Active_Snipe : ActiveAbility
    {
        //private float CDstart;
        private bool CurrentlyActive = false;
        private PlayerShoot pShoot;
        private GameObject rayOrigin;

        private void Awake()
        {
            // Set name
            Name = "Snipe";
            //Set Sprite
            Icon = Resources.Load<Sprite>("Sniper");
        }


        public override void OnAbilityAdd()
        {
            Cooldown = 5;
            //Powerup added
            Debug.Log(Name + " Added");
          

            // Add new shoot function to delegate
            pShoot = GetComponent<PlayerShoot>();
            if (pShoot)
            {
                Debug.Log("Snipe Added to Shoot Delegate");
                pShoot.shoot += TryActivate;
            }

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
            if (!photonView.isMine)
            {
                return;
            }
            foreach (Transform child in transform)
            {
                if (child.name == "Gun")
                {
                    rayOrigin = child.gameObject;
                }
            }

            Vector3 mp = Input.mousePosition;
            mp.z = 999;
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

            //StartCoroutine(VisualizeRaycast(rayOrigin, targetVector));

            base.Activate();
        }

        //private IEnumerator VisualizeRaycast(GameObject Origin, Vector3 targetLocation)
       // {
            //LineRenderer snipeLaser = Origin.GetComponent<LineRenderer>();
            //snipeLaser.SetPosition(0, Origin.transform.position);
            //snipeLaser.SetPosition(1, targetLocation);

            //snipeLaser.enabled = true;
            //yield return new WaitForSeconds(.2f);
            //snipeLaser.enabled = false;
       // }
    }
}