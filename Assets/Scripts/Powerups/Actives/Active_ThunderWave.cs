using UnityEngine;

namespace Powerups
{
    public class Powerup_ThunderWave : ActiveAbility
    {
        public float force = 700;
        public Vector3 explosionPos;
        public float radius = 20;
        public float up = 3;
        public float hitDistance = 20;

        public Vector3 origin;
        private Vector3 direction;

        public float sphereRadius = 10;
        public float maxDistance = 10;
        public LayerMask layerMask = 1;

        private PlayerShoot pShoot;

        #region Abstract Methods

        /// <summary>
        /// Called when abilities are added to a player.
        /// Make sure to set the ability name string!
        /// </summary>
        public override void OnAbilityAdd()
        {
            Cooldown = 2;
            Name = "Thunder Wave";
            Debug.Log(Name + " Added");
            
            pShoot = GetComponent<PlayerShoot>();
            if (pShoot)
            {
                
                pShoot.shoot += TryActivate;
            }

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
                                hit.rigidbody.AddExplosionForce(force, explosionPos, radius, up);
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