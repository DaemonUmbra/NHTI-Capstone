using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode] //coolest shit ever


    // Seed my own RNG
public class FloatingRocks : MonoBehaviour
{
    public float speed = 5f;
    public float rate; 
    float nextFire = 1.0f;
    public Vector3 axis;
    public Vector3 randomDirection;

    // Use this for initialization
    void Start ()
    {
        
	}

    private void Awake()
    {
        rate = Random.value;
    }
    // Update is called once per frame
    void Update ()
    {
        

        nextFire = Time.time + rate;

            if (Time.time >= nextFire)
            {
                randomDirection = new Vector3(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                axis = randomDirection;
            }

            transform.Rotate(axis, speed * Time.deltaTime);
        
    }
}
