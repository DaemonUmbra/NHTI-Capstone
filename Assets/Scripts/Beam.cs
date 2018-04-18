using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {

    float lastHitTime;
    float hitDelay = 1f;

	// Use this for initialization
	void Awake () {
        lastHitTime = Time.time - hitDelay;
	}
	

    private void OnTriggerStay(Collider other)
    {
        if(Time.time > lastHitTime + hitDelay)
        {
            if (other.transform.tag == "Player" && !other.transform.gameObject.GetComponent<PhotonView>().isMine)
            {
                Debug.Log("hIT WITH BEAM");
                PlayerStats stats;
                
                stats = other.transform.GetComponent<PlayerStats>();
                stats.TakeDamage(10, transform.parent.gameObject);
            }
            lastHitTime = Time.time;
        }
    }

    
}
