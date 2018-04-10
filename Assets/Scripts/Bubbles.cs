using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubbles : Projectile
{

    private new void Awake()
    {
        base.Awake();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onPlayerHit(other);
        }
        if (other.tag == "Environment")
        {
            PhotonNetwork.Destroy(photonView);
            PhotonNetwork.Destroy(gameObject);
        }
        if(other.gameObject.tag == "Bullet")
        {
            PhotonNetwork.Destroy(other.gameObject.GetPhotonView());
            PhotonNetwork.Destroy(other.gameObject);
        }
    }
}
