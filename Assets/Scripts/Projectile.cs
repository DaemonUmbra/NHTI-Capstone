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
    [SerializeField]
    ProjectileType type;
    [SerializeField]
    float damage;
    [SerializeField]
    public float speed;
    [SerializeField]
    float lifetime = 3; // Seconds

    public void Start()
    {
        // Get rigidbody reference
        rb = gameObject.GetComponent<Rigidbody>();

        if(!shooter)
        {
            Debug.LogWarning("Shooter must be manually set by calling IgnorePlayer on the projectile immediately after instantiation, this is to prevent future issues with multiplayer");
        }
        ///Commented out in the interest of future multiplayer
        //GameObject player = GameObject.Find("Player");
        //Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());

        // Debug.Log("Rotation: " + gameObject.transform.rotation);

        // Apply velocity
        rb.velocity = transform.forward * speed;

        // Destroy after a set numer of seconds
        Destroy(gameObject, lifetime);
    }

    // Ignore collision with player
    public void IgnorePlayer(Transform player)
    {
        shooter = player;
        Physics.IgnoreCollision(shooter.GetComponent<Collider>(), GetComponent<Collider>());
    }
}
