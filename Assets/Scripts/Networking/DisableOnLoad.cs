using System.Collections.Generic;
using UnityEngine;

public class DisableOnLoad : Photon.MonoBehaviour
{
    /// <summary>
    /// Scripts to be disabled for non-local players in Multiplayer
    /// </summary>
    public List<MonoBehaviour> ComponentsToDisable;

    /// <summary>
    /// Game objects to be disabled by non-local players in Multiplayer
    /// </summary>
    public List<GameObject> GameObjectsToDisable;

    private void Awake()
    {
        if (!photonView.isMine)
        {
            // Disable listed scripts
            if (ComponentsToDisable != null)
            {
                foreach (MonoBehaviour script in ComponentsToDisable)
                {
                    script.enabled = false;
                }
            }
            // Disable listed GameObjects
            if (GameObjectsToDisable != null)
            {
                foreach (GameObject obj in GameObjectsToDisable)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}