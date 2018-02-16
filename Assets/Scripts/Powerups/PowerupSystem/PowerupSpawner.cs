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

    private void OnTriggerEnter(Collider other)
    {
        PhotonView pv = PhotonView.Get(this);
        
        if (other.gameObject.tag == "Player")
        {
            
            Debug.Log("Pick UP!");
            pv.RPC("DestroyPickup", PhotonTargets.All, pickUp.GetPhotonView().viewID);

            
            hasPickup = false;
        }
        
    }
    [PunRPC]
    void DestroyPickup(int viewID)
    {
        //FIND GAMEOBJECT from its VIEW ID
        PhotonView srcViewID = PhotonView.Find(viewID);
        GameObject srcObj = null;

        if (srcViewID != null)
            srcObj = srcViewID.gameObject;
        PhotonNetwork.Destroy(srcObj);
    }
}