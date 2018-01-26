using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum of projectile types
public enum ProjectileType { BULLET, LAZER };

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

    Rigidbody rb;

    [SerializeField]
    ProjectileType Type;
    [SerializeField]
    public float Damage; 
    [SerializeField]
    public float Speed = 10f;
    [SerializeField]
    public float Lifetime = 3f; // Seconds

    public GameObject Owner;
    private PlayerStats ownerStats;
    public List<Effect> OnHitEffects;
    

    public void Start()
    {
        // Set owner to the parent obj
        Owner = transform.parent.gameObject;

        // Get components
        rb = gameObject.GetComponent<Rigidbody>();
        ownerStats = Owner.GetComponent<PlayerStats>();
        OnHitEffects = ownerStats.OnHitEffects;
        Damage = ownerStats.BaseDamage;

        // Ignore collision with owner
        if (Owner)
            Physics.IgnoreCollision(Owner.GetComponent<Collider>(), GetComponent<Collider>());
        else
            Debug.LogError("Projectile has no owner.");

        // Apply velocity
        rb.velocity = transform.forward * Speed;

        // Destroy after a set number of seconds
        Destroy(gameObject, Lifetime);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerStats targetStats = collision.gameObject.GetComponent<PlayerStats>();
            if(targetStats)
            {
                targetStats.TakeDamage(Damage, Owner, OnHitEffects);
                ownerStats.ReportHit(collision.gameObject);
                Destroy(this);
            }
        }
    }
    
}
