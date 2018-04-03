using UnityEngine;

namespace Powerups
{
    public class Active_ThunderWave : ActiveAbility
    {
        public float upforce = 15;

        public float forwardforce = 500;
        public Vector3 explosionPos;
        public float radius = 20;
        public float up = .1f;
        public float hitDistance = 20;

        public Vector3 origin;
        private Vector3 direction;

        public float sphereRadius = 10;
        public float maxDistance = 10;
        public LayerMask layerMask = 1;

        private PlayerShoot pShoot;

        private void Awake()
        {
            Cooldown = 2;
            Name = "Thunder Wave";
        }

        #region Abstract Methods

        /// <summary>
        /// Called when abilities are added to a player.
        /// Make sure to set the ability name string!
        /// </summary>
        public override void OnAbilityAdd()
        {
            Debug.Log(Name + " Added");

            base.OnAbilityAdd();
        }

        protected override void Activate() // Will need to be activated by something once we decide how players will trigger abilities.
        {
            origin = transform.position;
            direction = Vector3.forward;

            explosionPos = transform.position;

            RaycastHit[] hits = Physics.SphereCastAll(origin, sphereRadius, direction, maxDistance, layerMask);
            foreach (RaycastHit hit in hits)
            {


                if (hit.rigidbody != null && hit.transform.gameObject.tag == "Player")
                {
                    if (hit.transform.gameObject != this.gameObject)
                    {
                        Quaternion rot = Quaternion.Euler(-45, 0, 0);
                        Vector3 forceDir = rot * Vector3.up;
                        hit.rigidbody.AddForce(forceDir * upforce, ForceMode.Impulse);


                        //hit.rigidbody.AddForce(transform.up * upforce);
                        //hit.rigidbody.AddForce(transform.forward * forwardforce);
                        Debug.Log("I hit:" + hit.transform.gameObject.name);
                    }
                }

            }
            base.Activate();
        }

        public override void OnUpdate()
        {
            // Call base function
            base.OnUpdate();
        }

        /// <summary>
        /// Called when an ability is removed from the player
        /// </summary>
        public override void OnAbilityRemove()
        {
            base.OnAbilityRemove();
        }

        #endregion Abstract Methods
    }
}