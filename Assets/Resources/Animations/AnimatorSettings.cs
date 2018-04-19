using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimatorSettings : Photon.MonoBehaviour
{

    public Animator anim;
    

    // Use this for initialization
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            anim = GetComponent<Animator>();

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)
                || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                anim.SetBool("IsWalking", true);
            }
            else
            {
                anim.SetBool("IsWalking", false);
            }

            if (Input.GetKey(KeyCode.Mouse0) || Input.GetButtonDown("Fire1"))
            {
                anim.SetBool("IsAttacking", true);
            }
            else
            {
                anim.SetBool("IsAttacking", false);
            }
        }
    }
}