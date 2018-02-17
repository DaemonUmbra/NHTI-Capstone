using UnityEngine;

public class Pickup : Photon.MonoBehaviour
{
    private BaseAbility _ability;
    GameObject spawner;
    
    // Use this for initialization
    private void Start()
    {
        _ability = GetComponent<BaseAbility>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(this.gameObject != null)
        {
            spawner = gameObject.transform.parent.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView pv = PhotonView.Get(this);

        if (other.CompareTag("Player"))
        {
            //AbilityManager aManager = other.GetComponent<AbilityManager>();
            //aManager.AddAbility(_ability);
           
            PhotonNetwork.Destroy(gameObject);
            if (photonView.isMine)
                PhotonNetwork.Destroy(photonView);
            PowerupSpawner pSpawn = spawner.GetComponent<PowerupSpawner>();
            pSpawn.hasPickup = false;
           
        }
    }
  
}


