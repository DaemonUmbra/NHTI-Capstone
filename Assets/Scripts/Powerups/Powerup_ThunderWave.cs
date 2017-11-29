using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class Powerup_ThunderWave : BaseAbility
    {

        [SerializeField]
        protected new string Name = "Thunder Wave";
        public string GetName { get { return Name; } }
        [SerializeField]
        protected bool active = true;
        public float force;
        public Vector3 explosionPos;
        public float radius;
        public float up;
        public float hitForce;
        public float hitDistance;

        #region Abstract Methods

        /// <summary>
        /// Called when abilities are added to a player.
        /// Make sure to set the ability name string!
        /// </summary>
        public override void OnAbilityAdd()
        {

        }
        /// <summary>
        /// Called by the ability manager on each update step. 
        /// Use this instead of Unity's Update()
        /// </summary>
        public override void OnUpdate()
        {
            explosionPos = transform.position;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Vector3 back = transform.TransformDirection(-Vector3.forward);
            Vector3 left = transform.TransformDirection(Vector3.left);
            Vector3 right = transform.TransformDirection(Vector3.right);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, fwd, out hit, hitDistance))
            {
                print("There is something in front of the object!");
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddExplosionForce(force, explosionPos, radius, up);

                    }
                }
            }
            if (Physics.Raycast(transform.position, back, out hit, hitDistance))
            {
                print("There is something behind the object!");
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(back * hitForce);

                    }
                }
            }
            if (Physics.Raycast(transform.position, right, out hit, hitDistance))
            {
                print("There is something right of the object!");
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(right * hitForce);

                    }
                }
            }
            if (Physics.Raycast(transform.position, left, out hit, hitDistance))
            {
                print("There is something left of the object!");
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(left * hitForce);

                    }
                }
            }
        }
        /// <summary>
        /// Called when an ability is removed from the player
        /// </summary>
        public override void OnAbilityRemove()
        {
            active = true;
        }

        #endregion

        // Access name

        // Access active status
        public bool IsActive { get { return active; } }

    }
}