﻿using UnityEngine;

public class SlimeBall : Photon.MonoBehaviour
{
    private float lifetime = 7f;
    private float force = 10f;
    GameObject plr;
    Vector3 direction;

    // Use this for initialization
    private void Start()
    {
        //Destroy(gameObject, lifetime);
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        rb.velocity = transform.forward * force;
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            direction = (collision.transform.position - transform.position);
            //direction = direction.normalized;
            direction.y = Mathf.Abs(direction.y);
            plr = collision.gameObject;
            PlayerController hitController = plr.GetComponent<PlayerController>();
            Vector3 mult = Vector3.one;
            mult.y = 1;
            mult.x = mult.z = .3f;
            hitController.ApplyKnockBack(direction, 2, mult);
            //hitController.ApplyKnockBack(direction, 4, mult);
        }
    }
}