using UnityEngine;
using Powerups;

public class Pickup : Photon.MonoBehaviour
{
    private AbilityManager aMan;
    private BaseAbility _ability;
    public GameObject spawner;
    
    // Use this for initialization
    private void Start()
    {
        _ability = GetComponent<BaseAbility>();
        
    }

    private void Awake()
    {
        if(gameObject != null)
        {
            gameObject.AddComponent<PowerUp_Blink>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView pv = PhotonView.Get(this);

        if (other.gameObject.tag == "Player")
        {
            AbilityManager aManager = other.GetComponent<AbilityManager>();
           
            aManager.AddAbility(_ability);
            PowerupSpawner pSpawn = spawner.GetComponent<PowerupSpawner>();
            pSpawn.hasPickup = false;
            PhotonNetwork.Destroy(gameObject);
            if (photonView.isMine)
                PhotonNetwork.Destroy(photonView);
            
            
           
        }
    }
  
}


