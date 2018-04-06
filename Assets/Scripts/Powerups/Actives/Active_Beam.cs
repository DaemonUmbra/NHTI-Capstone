﻿using UnityEngine;
using System.Collections;

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
    public class Active_Beam : ActiveAbility
    {
        private bool onCooldown = false, CurrentlyActive = false;
        private PlayerShoot pShoot;
        private GameObject rayOrigin;
        ModelManager modelManager;
        float lastActive = 0f;
        private void Awake()
        {
            // Set name
            Cooldown = 4.5f;
            Name = "Beam";
            Icon = Resources.Load<Sprite>("Images/Beam");
        }
        public override void OnAbilityAdd()
        {
            Debug.Log(Name + " Added");
            //modelManager = GetComponent<ModelManager>();
            //modelManager.SetModel("Beam");
            // Call base function
            base.OnAbilityAdd();
        }

        protected override void Activate()
        {
            StartCoroutine(Beam());
            
            base.Activate();
        }
        
        public override void OnUpdate()
        {
            //if (Input.GetMouseButtonUp(0))
            //{
            //    CurrentlyActive = false;
            //    LineRenderer Laser = GetLaser(rayOrigin);
            //    Laser.enabled = false;
            //}
            //if (CurrentlyActive)
            //{
            //    foreach (Transform child in transform)
            //    {
            //        if (child.name == "Gun")
            //        {
            //            rayOrigin = child.gameObject;
            //        }
            //    }
            //    Vector3 mp = Input.mousePosition;
            //    mp.z = 999;
            //    Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(mp);
            //    Vector3 targetVector = mouseLocation;

            //    Ray snipeRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            //    RaycastHit endPoint;

            //    if (Physics.Raycast(snipeRay, out endPoint))
            //    {
            //        Debug.Log(endPoint.transform.gameObject.name);
            //        targetVector = endPoint.point;
            //    }
            //    else
            //    {
            //        Debug.Log("no object was hit");
            //    }

            //    VisualizeRaycast(rayOrigin, targetVector);
            //}

            // Call base function
            base.OnUpdate();
        }
        IEnumerator Beam()
        {
            modelManager = GetComponent<ModelManager>();
            modelManager.SetModel("Beam");
            yield return new WaitForSecondsRealtime(Cooldown);
            modelManager.SetModel("Default");
            
        }

        public override void OnAbilityRemove()
        {
            // Call base function
            modelManager.SetModel("Default");
            base.OnAbilityRemove();
        }

       

       

       
    }
}