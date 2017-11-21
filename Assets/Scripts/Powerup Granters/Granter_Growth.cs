using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Powerup_Granters
{
    [RequireComponent(typeof(Collider))]
    public class Granter_Growth : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<AbilityManager>().AddAbility<Powerups.Powerup_Growth>();
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<AbilityManager>().AddAbility<Powerups.Powerup_Growth>();
                Destroy(gameObject);
            }
        }

    }
}
