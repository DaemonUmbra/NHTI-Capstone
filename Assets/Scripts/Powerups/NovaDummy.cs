using UnityEngine;
using System.Collections;

public class NovaDummy : MonoBehaviour
{
    Transform Detonator;
    Powerups.Active_Nova Powerup;

    // Use this for initialization
    void Start()
    {
        Powerup = Detonator.GetComponent<Powerups.Active_Nova>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDetonator(Transform transform)
    {
        Detonator = transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(Powerup != null)
        {
            Powerup.OnHitPlayer(other.transform);
        }
    }
}
