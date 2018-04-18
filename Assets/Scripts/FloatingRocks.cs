using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class FloatingRocks : MonoBehaviour
{
    public float speed = 5f;
    public float rateOfFire = -1.0f;
    float nextFire = 1.0f;
    

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 randomDirection = new Vector3(Random.value, Random.value, Random.value);

        nextFire = Time.time + rateOfFire;

        if (Time.time >= nextFire)
        {
            transform.Rotate(randomDirection, speed * Time.deltaTime);
        }
    
    
    }
}
