using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float MovementSpeed;
    public float JumpHeight;

    public bool HasJumped;

    [HideInInspector]
    public Rigidbody RB;

	// Use this for initialization
	void Start () {
        RB = GetComponent<Rigidbody>();
        HasJumped = false;
	}
	
	// Update is called once per frame
	void Update () {
        MovementInput();
        Jump();
        Fire();
	}

    void MovementInput ()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * MovementSpeed;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * MovementSpeed;

        transform.Translate(x, 0, y);
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && HasJumped == false)
        {
            Debug.Log("Jump");
            HasJumped = true;
            RB.AddForce(0, JumpHeight, 0);
        }
    }

    void Fire()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Fire");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            HasJumped = false;
        }

    }
}
