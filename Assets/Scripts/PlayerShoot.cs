using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    // Shooting delegate
    public delegate void Shoot();
    public Shoot shoot;

    [SerializeField]
    public GameObject projectile;

	// Use this for initialization
	void Start () {
        AbilityManager aManager = GetComponent<AbilityManager>();

        if (aManager)
        {
            aManager.AddAbility<SingleShot>();
        }
	}

    private void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            AbilityManager aManager = GetComponent<AbilityManager>();
            if(aManager.HasAbility<SingleShot>())
            {
                aManager.AddAbility<RingOfBullets>();
                aManager.RemoveAbility<SingleShot>();
            }
            else if (aManager.HasAbility<RingOfBullets>())
            {
                aManager.AddAbility<SingleShot>();
                aManager.RemoveAbility<RingOfBullets>();
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AbilityManager aManager = GetComponent<AbilityManager>();
            
            if (aManager.HasAbility<SingleShot>())
            {
                aManager.RemoveAbility<SingleShot>();
            }
            if (aManager.HasAbility<RingOfBullets>())
            {
                aManager.RemoveAbility<RingOfBullets>();
            }

            if (aManager.HasAbility<Snipe>())
            {
                aManager.RemoveAbility<Snipe>();
                aManager.AddAbility<SingleShot>();
            }
            else
            {
                aManager.AddAbility<Snipe>();
            }
            
        }
    }






}
