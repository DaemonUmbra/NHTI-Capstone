using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_SelfSlow : MonoBehaviour {

    SlowMovement slow;

    // Use this for initialization
    void Start () {
        slow = new SlowMovement(0.25f, 3f);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space"))
        {
            Debug.Log("Slowing down...");
            slow.ApplyEffect(gameObject);
        }
	}
}
