using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_FeatherFall : ActiveAbility {

    public float maxVelocity;
    private Rigidbody rb;
    public override void Activate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnAbilityAdd()
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y > maxVelocity && rb.velocity.y < -0.1f)
        {
            Vector3 ogVelocity = rb.velocity;
            
            Vector3 clamp = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
            Debug.Log("Da bug Check");
            rb.velocity =  new Vector3(ogVelocity.x, clamp.y, ogVelocity.z);
        }
    }
}

