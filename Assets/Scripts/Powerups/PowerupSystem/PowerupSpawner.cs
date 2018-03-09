using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Powerups;
using System;

public class PowerupSpawner : Photon.MonoBehaviour
{
    float lastPickupTime;
    float spawnDelay;

    //private static bool IsSetup;
    public List<string> AvailablePowerupStrings;


    public float MinDelay = 3f;
    public float MaxDelay = 10f;

    public bool hasPickup;
    public GameObject pickUp;
    

    // Use this for initialization
   
    private void Start()
    {
        spawnDelay = UnityEngine.Random.Range(MinDelay, MaxDelay);
        lastPickupTime = Time.time - spawnDelay;
    }

    

    // Update is called once per frame
    private void Update()
    {
        if(PhotonNetwork.isMasterClient)
        {
            if(Time.time > lastPickupTime + spawnDelay)
            {
                SpawnPowerup();
            }
        }
    }
    public void SpawnPowerup()
    {
        if (hasPickup == false)
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
            var pickup = PhotonNetwork.Instantiate(pickUp.name, this.transform.position, this.transform.rotation, 0);


            photonView.RPC("RPC_SpawnPowerup", PhotonTargets.All, spawnDelay, pickup.GetPhotonView().viewID, powerName);
        }
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


}