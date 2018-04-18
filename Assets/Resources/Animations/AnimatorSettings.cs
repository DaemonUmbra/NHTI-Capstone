using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimatorSettings : Photon.MonoBehaviour
{

    public Animator anim;
    

    // Use this for initialization
    void Start()
    {
        if (photonView.isMine)
        {
            anim = GetComponent<Animator>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S))
        {
            anim.SetBool("IsWalking", true);
        }
        else 
        {
            anim.SetBool("IsWalking", false);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            anim.SetBool("IsAttacking", true);
        }
        else
        {
            anim.SetBool("IsAttacking", false);
        }


    }
}