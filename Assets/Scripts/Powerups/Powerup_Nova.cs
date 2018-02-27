using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups {
    public class Powerup_Nova : ActiveAbility
    {
        public float ExplosionSize = 2f;
        public float ExplosionTime = 1f;
        public float ScaleStep = .1f;
        public float ExplosionForce = 5f;
        public float ExplosionDamage = 5f;
        private AbilityManager AbilityManager;
        private Vector3 ScaleStepVector;
        private List<GameObject> Affected;
        private GameObject Explosion;

        public override void OnAbilityAdd()
        {
            base.OnAbilityAdd();
            AbilityManager = GetComponent<AbilityManager>();
            ScaleStepVector = new Vector3(ScaleStep, ScaleStep, ScaleStep);
        }
        protected override void Activate()
        {
            base.Activate();
            Explosion = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Explosion.transform.position = transform.parent.localPosition;
            Explosion.SetActive(false);
            Explosion.name = "Nova Explosion";
            Explosion.AddComponent<SphereCollider>();
            Explosion.transform.localScale = Vector3.zero;
            Explosion.SetActive(true);
            StartCoroutine(Explode(Explosion, ExplosionSize, ExplosionTime));
        }
        
        IEnumerator Explode(GameObject Explosion, float size, float time)
        {
            bool Exploding = true;
            bool Stage1 = true;
            while (Exploding)
            {
                Explosion.transform.position = transform.parent.localPosition;
                if (Explosion.transform.localScale.magnitude < size && Stage1)
                {
                    //Grow
                    Explosion.transform.localScale += ScaleStepVector;
                }
                else
                {
                    if(Explosion.transform.localScale == Vector3.zero)
                    {
                        Destroy(Explosion);
                        Exploding = false;
                    }
                    if (Explosion.transform.localScale.magnitude >= size) { Stage1 = false; }
                    //Shrink
                    Explosion.transform.localScale -= ScaleStepVector;
                }
                yield return new WaitForEndOfFrame();
            }
            AbilityManager.RemoveAbility(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<PlayerStats>() != null)
            {
                other.GetComponent<PlayerStats>().TakeDamage(ExplosionDamage, Explosion);
            }
            if (other.GetComponent<Rigidbody>() != null)
            {
                other.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, Explosion.transform.position, ExplosionSize);
            }
        }
    }
}
