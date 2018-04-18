using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode] coolest shit ever

public class FloatingRocks : MonoBehaviour
{
    public float speed = 5f;
    public float rateOfFire = -1.0f;
    float nextFire = 1.0f;
    Vector3 axis = Vector3.up;

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        
        nextFire = Time.time + rateOfFire;

        if (Time.time >= nextFire)
        {
            Vector3 randomDirection = new Vector3(Random.value, Random.value, Random.value);
            axis = randomDirection;
        }

        transform.Rotate(axis, speed * Time.deltaTime);
    }
}
