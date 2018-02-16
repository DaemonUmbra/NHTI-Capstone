using UnityEngine;

namespace PUNTutorial
{
    public class HUD : MonoBehaviour
    {
        private static HUD instance;

        private void Awake()
        {
            if (instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}