using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class AnimatorSettings : Photon.MonoBehaviour
{

    Animator anim;
    Animation an;
    AnimatorControllerLayer[] layers;

    // Use this for initialization
    void Start()
    {
        if (photonView.isMine)
        {
            anim = GetComponentInChildren<Animator>();
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