﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PowerupSpawner : Photon.MonoBehaviour
{
    float timer;
    int spawnDelay;

    public bool hasPickup;
    public GameObject pickUp;

    // Use this for initialization
   
    private void Start()
    {
       
    }

    // Update is called once per frame
    private void Update()
    {
        spawnDelay = Random.Range(3, 10);
        timer += Time.deltaTime;
        if (timer > spawnDelay && hasPickup == false)
        {
            var pickup = PhotonNetwork.Instantiate(pickUp.name, this.transform.position, this.transform.rotation,0);
            pickup.transform.SetParent(this.transform);
            timer = 0;
            hasPickup = true;
        }
    }


}