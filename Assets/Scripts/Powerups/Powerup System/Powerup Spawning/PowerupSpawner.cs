using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Powerups;
using System;

public class PowerupSpawner : Photon.MonoBehaviour
{
    float spawnDelay;

    public SpawnPool spawnPool;
    
    public float MinDelay = 3f;
    public float MaxDelay = 10f;

    public bool hasPickup;
    public float lastPickupTime;
    public GameObject pickupPrefab;

    // Use this for initialization
    private void Start()
    {
        spawnDelay = UnityEngine.Random.Range(MinDelay, MaxDelay);
        lastPickupTime = Time.time - spawnDelay;
        hasPickup = false;

        if (PhotonNetwork.isMasterClient)
        { 
            if (spawnPool)
            {
                GameObject pool = PhotonNetwork.Instantiate(spawnPool.name, transform.position, transform.rotation, 0);
                spawnPool.transform.SetParent(transform);
                photonView.RPC("RPC_SetSpawnPool", PhotonTargets.All, spawnPool.photonView.viewID);
            }

        }   
    }

    // Update is called once per frame
    private void Update()
    {
        if(PhotonNetwork.isMasterClient)
        {
            // Check if enough time has passed to spawn a powerup
            if(Time.time > lastPickupTime + spawnDelay && hasPickup == false)
            {
                SpawnPowerup();
            }

            // Set the last pickup time to the current time if a pickup is currently held
            if(hasPickup)
            {
                lastPickupTime = Time.time;
            }
        }
    }
    public void SpawnPowerup()
    {
        if (hasPickup == false && PhotonNetwork.isMasterClient)
        {
            // Reset spawn delay
            spawnDelay = UnityEngine.Random.Range(MinDelay, MaxDelay);

            BaseAbility powerup = spawnPool.RandomPowerup();
            var pickup = PhotonNetwork.Instantiate(pickupPrefab.name, transform.position, transform.rotation, 0);
            

            photonView.RPC("RPC_SpawnPowerup", PhotonTargets.All, spawnDelay, pickup.GetPhotonView().viewID, powerup.GetType().ToString());
        }
    }
    public void CollectPowerup()
    {
        photonView.RPC("RPC_CollectPowerup", PhotonTargets.All);
    }

    [PunRPC]
    private void RPC_SetSpawnPool(int poolId)
    {
        PhotonView poolView = PhotonView.Find(poolId);
        spawnPool = poolView.GetComponent<SpawnPool>();
        spawnPool.Init();
    }
    private void RPC_SpawnPowerup(float newDelay, int powerupId, string powerupType)
    {
        spawnDelay = newDelay;
        lastPickupTime = Time.time;
        Type thisType = Type.GetType(powerupType);

        // Find pickup by photon ID
        GameObject pickup = PhotonView.Find(powerupId).gameObject;
        

        if (hasPickup == false)
        {
            pickup.AddComponent(thisType);
            pickup.transform.SetParent(transform);
            Pickup pUp = pickup.GetComponent<Pickup>();
            hasPickup = true;
        }
    }

    [PunRPC]
    private void RPC_CollectPowerup()
    {
        lastPickupTime = Time.time;
        hasPickup = false;
    }

}