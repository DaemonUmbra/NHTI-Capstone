using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///summary
 /*
Developers and Contributors: Ian Cahoon
    
Information
    Name: Beam
    Type: Active
    Effect: Basic fire turns into a sustained beam
    Tier: Rare
  */
///summary

namespace Powerups
{
    public class Powerup_Beam : ActiveAbility
    {
        bool onCooldown = false, CurrentlyActive = false;
        PlayerShoot pShoot;
        GameObject rayOrigin;


        public override void OnAbilityAdd()
        {
            // Set name
            Name = "Beam";
            Debug.Log(Name + " Added");

            // Add new shoot function to delegate 
            pShoot = GetComponent<PlayerShoot>();
            if (pShoot)
            {
                Debug.Log("Beam Added to Shoot Delegate");
                pShoot.shoot += OnShoot;
            }
        }
        public override void OnUpdate()
        {
            if (Input.GetMouseButtonUp(0))
            {
                CurrentlyActive = false;
                LineRenderer Laser = GetLaser(rayOrigin);
                Laser.enabled = false;
            }
            if (CurrentlyActive)
            {
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


                VisualizeRaycast(rayOrigin, targetVector);
            }
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
            CurrentlyActive = true;
            foreach (Transform child in transform)
            {
                if (child.name == "Gun")
                {
                    rayOrigin = child.gameObject;
                }
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


            VisualizeRaycast(rayOrigin, targetVector);
        }
        void VisualizeRaycast(GameObject Origin, Vector3 targetLocation)
        {

            LineRenderer Laser = GetLaser(Origin);
            Laser.SetPosition(0, Origin.transform.position);
            Laser.SetPosition(1, targetLocation);

            Laser.enabled = true;
        }
        LineRenderer GetLaser(GameObject Origin)
        {
            LineRenderer Laser = Origin.GetComponent<LineRenderer>();
            return Laser;
        }

    }
}
