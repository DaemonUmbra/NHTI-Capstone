using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePool : MonoBehaviour {
    float lifetime = 7f;
    float force = 500f;

	// Use this for initialization
	void Start ()
    {
        //Destroy(gameObject, lifetime);
        Debug.Log(transform.position);
    }
    private void Update()
    {
        /*Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider hit in colliders)
        {
            Debug.Log(hit.gameObject.name);
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb == null) return;
            rb.AddExplosionForce(force, transform.position, 2f, 100f);
        }*/
    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.tag == "Player")
        {

            Vector3 dir = other.contacts[0].point - other.transform.position;
            Debug.Log(dir);
            Vector3 newdir = new Vector3(-dir.x, Mathf.Abs(dir.y), -dir.z);
            newdir = newdir.normalized;
            //PlayerController.CrowdControlled = true;
            GameObject localPlayer = other.collider.gameObject;
            
            Rigidbody localRB = localPlayer.GetComponent<Rigidbody>();
            //localRB.velocity = Vector3.zero;
            localRB.AddForce(newdir * force);
            /*
            Vector3 direction = other.contacts[0].point - other.transform.position;
            direction = direction.normalized;
            Vector3 trueDirection = new Vector3(direction.x, 0, direction.z);

            

            localRB.velocity = Vector3.zero;
            localRB.AddExplosionForce(force, transform.position, 3f);
            localRB.AddForce(trueDirection * force);
            localRB.AddForce(localPlayer.transform.up * force);*/

        }
    }
}
