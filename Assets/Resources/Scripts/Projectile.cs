using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum of projectile types
public enum ProjectileType { BULLET, LAZER };

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

    Rigidbody rb;
    [SerializeField]
    Transform shooter;
    PlayerStats shooterStats;
    [SerializeField]
    ProjectileType type;

    public float damage;
    public float speed;

    PhotonView pv;

    [SerializeField]
    float lifetime = 3; // Seconds

    public void Start()
    {
        

        // Set shooterStats if shooter was set in inspector
        if(shooter)
        {
            shooterStats = shooter.GetComponent<PlayerStats>();
            if(shooterStats == null)
                Debug.LogError("No player stats found on shooter.");
        }

        PhotonView pv = PhotonView.Get(this);
        pv.RPC("Shoot", PhotonTargets.All);
    }

    [PunRPC]
    void Shoot()
    {
        // Get rigidbody reference
        rb = gameObject.GetComponent<Rigidbody>();

        // Apply velocity
        rb.velocity = transform.forward * speed;

        // Destroy after a set numer of seconds
        Destroy(gameObject, lifetime);
    }

    // Ignore collision with player
    public void IgnorePlayer(Transform player)
    {
        shooter = player;
        shooterStats = shooter.GetComponent<PlayerStats>();
        if(shooterStats == null)
            Debug.LogError("No player stats found on shooter.");

        Physics.IgnoreCollision(shooter.GetComponent<Collider>(), GetComponent<Collider>());
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        PlayerStats hitStats = hit.GetComponent<PlayerStats>();

        if(hitStats)
        {
            hitStats.TakeDamage(damage, shooter.gameObject, shooterStats.OnHitEffects);
            Destroy(gameObject);
        }
    }
}
