using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;
        PhotonView playerView = player.GetPhotonView();
        PlayerStats pStats = player.GetComponent<PlayerStats>();
        if (playerView && pStats)
        {
            pStats.Kill();
        }
    }
}
