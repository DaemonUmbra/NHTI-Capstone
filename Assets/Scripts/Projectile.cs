using UnityEngine;
using UnityEngine.Collections;
using System.Collections.Generic;

// Enum of projectile types
//public enum ProjectileType { BULLET, LAZER };

[RequireComponent(typeof(Rigidbody))]
public class Projectile : Photon.MonoBehaviour
{
    private Rigidbody rb;
    protected GameObject _shooter;

    protected PlayerStats shooterStats;
    protected PlayerShoot pShoot;
    

    //[SerializeField]
    //protected ProjectileType type;
    protected List<Effect> onHitEffects;
    

    protected float damage = 0f;
    protected float speed = 100f;

    [SerializeField]
    protected float lifetime = 3; // Seconds
    private float startTime = 0f;

    public void Awake()
    {
        //onHitEffects = new List<Effect>();
        rb = GetComponent<Rigidbody>();

        Destroy(gameObject, lifetime);
    }
    

    public void Shoot(GameObject shooter)
    {
        // Get shooter info
        _shooter = shooter;
        shooterStats = shooter.GetComponent<PlayerStats>();
        pShoot = _shooter.GetComponent<PlayerShoot>();
        Physics.IgnoreCollision(shooter.GetComponent<Collider>(), GetComponent<Collider>());
        damage = shooterStats.Damage;
        // Set the start time of the bullet
        startTime = Time.time;

        //Debug.Log(_shooter);
        transform.rotation = pShoot.OffsetPoint.rotation;
        // Apply velocity
        rb = GetComponent<Rigidbody>();
        if(rb)
        rb.velocity = transform.forward * speed;
    }


    protected virtual void OnTriggerEnter(Collider other)
    {
        // Only deal damage on the shooter client
        if (_shooter.GetPhotonView().isMine)
        {
            if (other.gameObject.tag == "Player")
            {
                onPlayerHit(other);
            }
        }
        else
        {
            Destroy(gameObject);
        }        
    }

    protected virtual void onPlayerHit(Collider hitPlayer)
    {
        GameObject hit = hitPlayer.gameObject;
        PhotonView hitView = hit.GetPhotonView();
        PlayerStats hitStats = hit.GetComponent<PlayerStats>();
        // Verify hit photon view
        if (hitView)
        {
            // Make sure the bullet isn't hitting it's own player
            if (hitPlayer != _shooter && hitStats)
            {
                hitStats.TakeDamage(damage, _shooter);
                print("Player hit!");
                Destroy(gameObject);
            }
        }
       
    }
}