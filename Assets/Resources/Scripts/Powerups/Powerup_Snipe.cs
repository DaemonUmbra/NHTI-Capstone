using System;
using System.Collections;
using System.Collections.Generic;
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
    public class Powerup_Snipe : ActiveAbility
    {
        private float CDstart;
        bool onCooldown = false, CurrentlyActive = false;
        PlayerShoot pShoot;
        GameObject rayOrigin;


        public override void OnAbilityAdd()
        {
            // Set name
            Name = "Snipe";
            Debug.Log(Name + " Added");

            // Add new shoot function to delegate 
            pShoot = GetComponent<PlayerShoot>();                                        
            if (pShoot)
            {
                Debug.Log("Snipe Added to Shoot Delegate");
                pShoot.shoot += OnShoot;
            }
        }
        public override void OnUpdate()
        {

        }
        public override void OnAbilityRemove()
        {
            // Remove shoot delegate
            if (pShoot)
            {
                pShoot.shoot -= OnShoot;
            }
            pShoot = null;

            // Call base function
            base.OnAbilityRemove();
        }
        public override void Activate()
        {
            throw new NotImplementedException();
        }
        public void OnShoot()
        {
            foreach(Transform child in transform)
            {
                if (child.name == "Gun")
                {
                    rayOrigin = child.gameObject;
                }
            }
            if (onCooldown)
            {
                Debug.Log("This ability is on cooldown" + "Start Time: " + CDstart);
                return;
            }
            //Debug.Log("Raycast Shot");
            
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


            StartCoroutine(VisualizeRaycast(rayOrigin, targetVector));
        }
        IEnumerator VisualizeRaycast(GameObject Origin, Vector3 targetLocation)
        {

            LineRenderer snipeLaser = Origin.GetComponent<LineRenderer>();
            snipeLaser.SetPosition(0, Origin.transform.position);
            snipeLaser.SetPosition(1, targetLocation);

            snipeLaser.enabled = true;
            yield return new WaitForSeconds(.2f);
            snipeLaser.enabled = false;

        }

    }
}
