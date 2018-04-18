using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubbles : Projectile
{
    private new void Awake()
    {
        speed /= 5;
        base.Awake();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if(PhotonNetwork.isMasterClient)
        {
            if(other.gameObject.tag == "Bullet")
            {
                //PhotonNetwork.Destroy(other.gameObject.GetPhotonView());
            }
        }
    }
}
