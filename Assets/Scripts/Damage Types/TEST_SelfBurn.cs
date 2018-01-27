using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_SelfBurn : MonoBehaviour
{

    //NOTE: CLEARLY UNFINISHED.

    BurnDamage burn;

	// Use this for initialization
	void Start ()
    {
        burn = new BurnDamage(10f, 3f);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown("b"))
        {
            burn.ApplyEffect(gameObject);
        }
	}
}
