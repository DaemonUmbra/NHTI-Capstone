using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class Fireball : MonoBehaviour
    {
        public float health = 10.0f;
        int count = 0;

        public void OnTriggerEnter(Collider collision)
        {
            if (count != 0)
            {
                count = 0;
                InvokeRepeating("Damage", 0.0f, 1.0f);
            }
            Destroy(collision.gameObject);
        }
        void Damage()
        {
            if (count != 4)
            {
                health -= 0.5f;
                count++;
            }
            else
            {
                return;
            }
        }
    }
}