using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode] //coolest shit ever


    // Seed my own RNG
public class FloatingRocks : MonoBehaviour
{
    private void Awake()
    {
        
    }

    public Vector3 rotateVec = Vector3.up;
    public float speed = 45.0f;

    private void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);//Yaw
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);//Roll
        transform.Rotate(Vector3.left, speed * Time.deltaTime);//Pitch

    }



}


//   public GameObject[] floatingObjs;

//   public float speed = 5f;
//   public float rate; 
//   float nextFire = 1.0f;
//   public Vector3 axis;
//   public Vector3 randomDirection;

//   // Use this for initialization
//   void Start ()
//   {

//}

//   private void Awake()
//   {
//       rate = Random.value;
//      // if (floatingObjs == null)
//           floatingObjs = GameObject.FindGameObjectsWithTag("floating");
//   }
//   // Update is called once per frame
//   void Update ()
//   {       
//       nextFire = Time.time + rate;

//       for (int i = 0; i < floatingObjs.Length; i++ )
//       {
//           Random.seed += (i);// i*i\

//           if (Time.time >= nextFire)
//           {
//               randomDirection = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
//               axis = randomDirection;
//           }

//           floatingObjs[i].transform.Rotate(axis, speed);//* Time.deltaTime);
//       }        
//   }