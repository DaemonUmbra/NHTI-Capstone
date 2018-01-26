using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum of projectile types
public enum ProjectileType { BULLET, LAZER };

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

    Rigidbody rb;

    [SerializeField]
    ProjectileType type;
    [SerializeField]
    float damage;
    [SerializeField]
    float speed;
    [SerializeField]
    float lifetime = 3; // Seconds

    public void Start()
    {
        // Get rigidbody reference
        rb = gameObject.GetComponent<Rigidbody>();

        // Ignore collision with player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());

        // Debug.Log("Rotation: " + gameObject.transform.rotation);

        // Apply velocity
        rb.velocity = transform.forward * speed;

        // Destroy after a set numer of seconds
        Destroy(gameObject, lifetime);
    }
}
