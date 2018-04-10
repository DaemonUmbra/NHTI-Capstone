
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups {
    public class Active_Nova : ActiveAbility
    {
        public float ExplosionSize = 15f;
        public float ExplosionTime = 1f;
        public float ScaleStep = 1f;
        public float ExplosionForce = 5f;
        public float ExplosionDamage = 5f;
        private AbilityManager AbilityManager;
        private Vector3 ScaleStepVector;
        private GameObject Explosion;
        private List<Transform> Affected = new List<Transform>();

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            Name = "Nova";
            //TODO: Nova Icon
            Tier = PowerupTier.Rare;
        }

        public override void OnAbilityAdd()
        {
            base.OnAbilityAdd();
            AbilityManager = GetComponent<AbilityManager>();
            ScaleStepVector = new Vector3(ScaleStep, ScaleStep, ScaleStep);
        }
        protected override void Activate()
        {
            base.Activate();
            if (photonView.isMine)
            {
                photonView.RPC("RPC_Nova_Explosion", PhotonTargets.All);
            }
        }

        [PunRPC]
        public void RPC_Nova_Explosion()
        {
            Explosion = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            NovaDummy dummy = Explosion.AddComponent<NovaDummy>();
            dummy.SetDetonator(transform);
            Explosion.SetActive(false);
            Explosion.transform.position = transform.position;
            Explosion.name = "Nova Explosion";
            Collider ExplosionCollider = Explosion.GetComponent<SphereCollider>();
            ExplosionCollider.isTrigger = true;
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
                if (Explosion.transform.localScale.magnitude < size && Stage1)
                {
                    //Grow
                    Explosion.transform.localScale += ScaleStepVector;
                }
                else
                {
                    if(Explosion.transform.localScale == Vector3.zero)
                    {
                        Affected.Clear();
                        Destroy(Explosion);
                        Exploding = false;
                    }
                    if (Explosion.transform.localScale.magnitude >= size) { Stage1 = false; }
                    //Shrink
                    Explosion.transform.localScale -= ScaleStepVector;
                }
                yield return new WaitForEndOfFrame();
            }
            //AbilityManager.RemoveAbility(this);
        }

        public void OnHitPlayer(Transform other)
        {
            if (photonView.isMine)
            {
                if (other.GetComponent<PlayerStats>() != null)
                {
                    other.GetComponent<PlayerStats>().TakeDamage(ExplosionDamage, gameObject);
                    Affected.Add(other.transform);
                }
                if (other.GetComponent<Rigidbody>() != null)
                {
                    other.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, Explosion.transform.position, ExplosionSize);
                }
            }
        }
    }
}