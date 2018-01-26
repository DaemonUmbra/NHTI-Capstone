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
        rb.velocity = transform.forward * force;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody playerRB = collision.gameObject.GetComponent<Rigidbody>();
            //playerRB.AddForce(collision.gameObject.transform.up * 300f);
            playerRB.AddForce(collision.gameObject.transform.up * 260f);
            
        }
    }
}
