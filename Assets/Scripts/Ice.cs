using UnityEngine;

namespace Powerups
{
    public class Ice : MonoBehaviour
    {
        public float speed;

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        public void OnTriggerEnter(Collider collision)
        {
            speed /= 2.0f;

            float time = Time.deltaTime;

            if (time >= 8.0f)
            {
                speed *= 2.0f;
            }
        }
    }
}