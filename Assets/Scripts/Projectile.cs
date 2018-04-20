﻿using UnityEngine;
using UnityEngine.Collections;
using System.Collections.Generic;

// Enum of projectile types
//public enum ProjectileType { BULLET, LAZER };

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    protected GameObject _shooter;

    protected PlayerStats pStats;
    protected PlayerShoot pShoot;
    

    //[SerializeField]
    //protected ProjectileType type;
    protected List<Effect> onHitEffects = new List<Effect>();
    
    // Projectile stats
    [SerializeField]
    protected bool UsePlayerDamage = false;
    [SerializeField]
    protected float damage = 0f;
    [SerializeField]
    protected float bonusDmgMultiplier;
    [SerializeField]
    protected float speed = 100f;
    [SerializeField]
    protected float lifetime = 3; // Seconds

    protected Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }
    public virtual void Shoot(GameObject shooter)
    {
        Debug.Log("Inside Shoot");
        // Get shooter info
        _shooter = shooter;
        pStats = shooter.GetComponent<PlayerStats>();
        pShoot = _shooter.GetComponent<PlayerShoot>();
        CalculateDamage();

        rb = GetComponent<Rigidbody>();
        if (rb)
        rb.velocity = transform.forward * speed;

        // Destroy after lifetime seconds
        _collider.enabled = true;
        Destroy(gameObject, lifetime);
    }

    protected virtual void CalculateDamage()
    {
        if(UsePlayerDamage)
        {
            damage = pStats.Damage + damage;
        }

        damage += damage * bonusDmgMultiplier;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger enter");
        if (other.gameObject.tag == "Player")
        {
            OnPlayerHit(other);
        }
        else
        {
            // Destroy the projectile on other clients
            Destroy(gameObject);
        }
    }

    protected virtual void OnPlayerHit(Collider hitPlayer)
    {
        GameObject hit = hitPlayer.gameObject;
        PhotonView hitView = hit.GetPhotonView();
        PlayerStats hitStats = hit.GetComponent<PlayerStats>();
        
        if (hitPlayer.gameObject != _shooter && hitStats)
        {
            print("Player hit!");
            // Only deal damage on shooter client
            if(PhotonNetwork.isMasterClient)
                hitStats.TakeDamage(damage, _shooter);
            
            // Apply status effects on all clients
            foreach(Effect e in onHitEffects)
            {
                e.ApplyEffect(_shooter);
            }
            Destroy(gameObject);
        }
        
    }
}