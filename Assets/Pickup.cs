using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    BaseAbility _ability;
	// Use this for initialization
	void Start () {
        _ability = GetComponent<BaseAbility>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            AbilityManager aManager = other.GetComponent<AbilityManager>();
            aManager.AddAbility(_ability);
            Destroy(gameObject);
        }
    }
}
