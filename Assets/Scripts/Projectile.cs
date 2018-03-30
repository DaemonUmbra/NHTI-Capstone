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
        GetShooter();
        Shoot();
        onHitEffects = new List<Effect>();
    }

    public void Update()
    {
        if (photonView.isMine)
        {
            if (Time.time >= startTime + lifetime)
            {
                PhotonNetwork.Destroy(photonView);
            }
        }
        
    }

    // Find a reference to the shooter
    private void GetShooter()
    {
        // Finds tagged player who has the same owner as this bullet
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetPhotonView().owner == photonView.owner)
            {
                SetShooter(player);
                //print("Shooter found: " + player.GetPhotonView().owner);
            }
        }
    }
    // Set the shooter reference
    private void SetShooter(GameObject shooter)
    {
        _shooter = shooter;
        //HACK: Fixes mismatched rotation, but not correctly
        transform.localRotation = _shooter.transform.rotation;
        Physics.IgnoreCollision(_shooter.transform.Find("Player Model").GetComponent<Collider>(), GetComponent<Collider>());
        shooterStats = _shooter.GetComponent<PlayerStats>();
        onHitEffects = shooterStats.OnHitEffects;
    }

    private void Shoot()
    {
        // Set shooterStats if shooter was set in inspector
        if (_shooter)
        {
            if (shooterStats)
            {
                damage = shooterStats.Damage;
            }
            else
            {
                Debug.LogError("No player stats found on shooter.");
            }
        }

        // Set the start time of the bullet
        startTime = Time.time;

        // Get rigidbody reference
        rb = gameObject.GetComponent<Rigidbody>();

        // Apply velocity
        rb.velocity = transform.forward * speed;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onPlayerHit(other);
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
            if (hitView.owner != photonView.owner && hitStats && photonView.isMine)
            {
                // Apply damage to the player
                hitStats.TakeDamage(damage, _shooter);
                print("Player hit!");
                PhotonNetwork.Destroy(photonView);
                PhotonNetwork.Destroy(gameObject);
            }
        }
        if (hit.tag == "Environment")
        {
            PhotonNetwork.Destroy(photonView);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}