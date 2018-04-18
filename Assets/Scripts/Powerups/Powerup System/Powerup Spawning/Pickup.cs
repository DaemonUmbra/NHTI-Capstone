using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Powerups;

public class Pickup : Photon.PunBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Only recieve the powerup on the client that picked it up
        // It is then synced to all other clients when the powerup is added
        PhotonView pv = other.GetComponent<PhotonView>();

        //HACK NullReferenceExceptions galore without this check
        if (pv == null)
            return;
        if (PhotonNetwork.isMasterClient)
        {
            BaseAbility ability = GetComponent<BaseAbility>();
            Debug.Log(ability);
            if (other.gameObject.tag == "Player")
            {
                // Add ability to player
                AbilityManager aManager = other.GetComponent<AbilityManager>();
                aManager.AddAbility(ability);
                // Reset the spawner
                PowerupSpawner pSpawn = GetComponentInParent<PowerupSpawner>();
                pSpawn.CollectPowerup();
                
                // Destroy pickup
                PhotonNetwork.Destroy(photonView);
            }
        }
    }
}


