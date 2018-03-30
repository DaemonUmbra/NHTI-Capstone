using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Powerups;
using System;

public class PowerupSpawner : Photon.MonoBehaviour
{
    
    float spawnDelay;

    //private static bool IsSetup;
    public List<string> AvailablePowerupStrings;


    public float MinDelay = 3f;
    public float MaxDelay = 10f;

    public bool hasPickup;
    public float lastPickupTime;
    public GameObject pickUp;
    
    

    // Use this for initialization
   
    private void Start()
    {
        spawnDelay = UnityEngine.Random.Range(MinDelay, MaxDelay);
        lastPickupTime = Time.time - spawnDelay;
        hasPickup = false;
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

            // Choose a new powerup to spawn
            AvailablePowerupStrings = new List<string>();

            Type[] Types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
            Dictionary<string, Type> AbilityDict = new Dictionary<string, Type>();
            foreach (Type type in Types)
            {
                if (type.Namespace == "Powerups" && type.IsSubclassOf(typeof(BaseAbility)))
                {
                    AvailablePowerupStrings.Add(type.Name);
                    // AbilityDict.Add(type.Name, type);
                }
            }
            //Powers = AvailablePowerupStrings;
            string powerName = AvailablePowerupStrings[UnityEngine.Random.Range(0, AvailablePowerupStrings.Count)];
            
            var pickup = PhotonNetwork.Instantiate(pickUp.name, transform.position, transform.rotation, 0);


            photonView.RPC("RPC_SpawnPowerup", PhotonTargets.All, spawnDelay, pickup.GetPhotonView().viewID, powerName);
        }
    }
    public void CollectPowerup()
    {
        photonView.RPC("RPC_CollectPowerup", PhotonTargets.All);
    }
    [PunRPC]
    private void RPC_SpawnPowerup(float newDelay, int powerupId, string powerupName)
    {
        spawnDelay = newDelay;
        lastPickupTime = Time.time;
        Type thisType = ReflectionUtil.GetAbilityTypeFromName(powerupName);

        // Find pickup by photon ID
        GameObject pickup = PhotonView.Find(powerupId).gameObject;
        pickup.AddComponent(thisType);
      

        if (hasPickup == false)
        {
            pickup.transform.SetParent(this.transform);
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