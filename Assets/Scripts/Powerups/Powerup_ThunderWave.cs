using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class Powerup_ThunderWave : ActiveAbility
    {
        
        public float force = 500;
        public Vector3 explosionPos;
        public float radius = 20;
        public float up = 1;
        public float hitForce = 500;
        public float hitDistance = 20;

        public Vector3 origin;
        private Vector3 direction;

        public float sphereRadius = 5;
        public float maxDistance = 0;
        public LayerMask layerMask= 1;

        #region Abstract Methods

        /// <summary>
        /// Called when abilities are added to a player.
        /// Make sure to set the ability name string!
        /// </summary>
        public override void OnAbilityAdd()
        {
            Name = "Thunder Wave";
            Debug.Log(Name + " Added");
            base.OnAbilityAdd();
        }
        

        protected override void RPC_Activate() // Will need to be activated by something once we decide how players will trigger abilities.
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Vector3 back = transform.TransformDirection(-Vector3.forward);
            Vector3 left = transform.TransformDirection(Vector3.left);
            Vector3 right = transform.TransformDirection(Vector3.right);

            origin = transform.position;
            direction = Vector3.forward;

            explosionPos = transform.position;

            RaycastHit[] hits = Physics.SphereCastAll(origin, sphereRadius, direction, maxDistance, layerMask);
            foreach (RaycastHit hit in hits)
            {
                Debug.Log("I hit :" + hit.transform.name);
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.rigidbody != null)
                    {
                        if (hit.transform.gameObject != this.gameObject)
                        {
                            hit.rigidbody.AddExplosionForce(force, explosionPos, radius, up);
                        }
                    }
                }
            }

            base.RPC_Activate();
        }
        /// <summary>
        /// Called when an ability is removed from the player
        /// </summary>
        public override void OnAbilityRemove()
        {
            base.OnAbilityRemove();
        }
        #endregion

        

    }
}