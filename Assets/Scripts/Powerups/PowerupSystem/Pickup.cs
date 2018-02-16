using UnityEngine;

public class Pickup : Photon.MonoBehaviour
{
    private BaseAbility _ability;
    private PowerupSpawner pSpawn;
    // Use this for initialization
    private void Start()
    {
        _ability = GetComponent<BaseAbility>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView pv = PhotonView.Get(this);

        if (other.CompareTag("Player"))
        {
            AbilityManager aManager = other.GetComponent<AbilityManager>();
            aManager.AddAbility(_ability);
            pv.RPC("DestroyPickup", PhotonTargets.All, this.gameObject);
            PhotonNetwork.Destroy(gameObject);
           
        }
    }
    [PunRPC]
    void DestroyPickup(GameObject go)
    {
        PhotonNetwork.Destroy(go);
    }
}


