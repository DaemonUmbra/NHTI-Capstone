using UnityEngine;

namespace PUNTutorial
{
    public class SpawnPoint : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(false);
        }
    }
}