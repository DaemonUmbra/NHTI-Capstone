using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PowerupSpawner : Photon.MonoBehaviour
{
    float timer;
    int spawnDelay = 4;

    public bool hasPickup;
    public GameObject pickUp;

    // Use this for initialization
   
    private void Start()
    {
       
    }

    // Update is called once per frame
    private void Update()
    {
        
        timer += Time.deltaTime;
        if (timer > spawnDelay && hasPickup == false)
        {
            PhotonNetwork.Instantiate(pickUp.name, this.transform.position, this.transform.rotation,0);
            timer = 0;
            hasPickup = true;
        }
    }


}