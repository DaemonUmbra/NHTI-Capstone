using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Powerups;

public class Pickup : Photon.PunBehaviour
{
    public BaseAbility _ability;

    // Use this for initialization
    //private void Start()
    //{
    //    //_ability = GetComponent<BaseAbility>();
    //    ////aMan = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityManager>();
    //    ////List<KeyValuePair<string, BaseAbility>> list = aMan.AbilityList.ToList();
    //    ////foreach(KeyValuePair<string,BaseAbility> power in list)
    //    ////{
    //    ////    Debug.Log(power);
    //    ////}
    //    //List<string> AvailablePowerupStrings = new List<string>();
    //    //Type[] Types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
    //    //Dictionary<string, Type> AbilityDict = new Dictionary<string, Type>();
    //    //foreach (Type type in Types)
    //    //{
    //    //    if (type.Namespace == "Powerups" && type.IsSubclassOf(typeof(BaseAbility)))
    //    //    {
    //    //        AvailablePowerupStrings.Add(type.Name);
    //    //        AbilityDict.Add(type.Name, type);
    //    //    }
    //    //}

    //}

    private void Awake()
    {
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        // Only recieve the powerup on the client that picked it up
        // It is then synced to all other clients when the powerup is added
        PhotonView pv = other.GetComponent<PhotonView>();
        if (pv.isMine)
        {
            _ability = GetComponent<BaseAbility>();
            Debug.Log(_ability);
            if (other.gameObject.tag == "Player")
            {
                // Add ability to player
                AbilityManager aManager = other.GetComponent<AbilityManager>();
                aManager.AddAbility(_ability);
                // Reset the spawner
                PowerupSpawner pSpawn = GetComponentInParent<PowerupSpawner>();
                pSpawn.CollectPowerup();
                // Destroy pickup
                PhotonNetwork.Destroy(photonView);
            }
        }
    }

    
}


