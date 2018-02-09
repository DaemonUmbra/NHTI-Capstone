using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_SelfConfusion : MonoBehaviour
{
    Confuse confusion;

	void Start ()
    {
        confusion = new Confuse(5.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown("i"))
        {
            confusion.ApplyEffect(gameObject);
            confusion.Activate();
        }

        if (Input.GetKeyDown("o"))
        {
            confusion.RemoveEffect();
        }
    }
}
