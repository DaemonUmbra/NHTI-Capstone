using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    float lifetime = 10f;
    float force = 10f;
    Rigidbody rb;
    // Use this for initialization
    void Start ()
    {
        Destroy(gameObject, lifetime);
        rb = gameObject.GetComponent<Rigidbody>();

        Vector3 mp = Input.mousePosition;
        mp.z = 10;
        Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(mp);

        transform.LookAt(mouseLocation);

        rb.velocity = transform.forward * force;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

}
