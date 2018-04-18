using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ModelManager : Photon.MonoBehaviour
{
    PlayerStats pStats;
    private Dictionary<string, Transform> subModelRegistry = new Dictionary<string, Transform>();
    private List<string> activeSubModels = new List<string>();

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        pStats = GetComponent<PlayerStats>();
        LoadSubModels();
    }

    private void LoadSubModels()
    {
        subModelRegistry.Add("Beam", Resources.Load<Transform>("PlayerModels/Beam"));
    }

    public void AddSubModel(string subModelName)
    {
        if (photonView.isMine)
        {
            Transform temp;
            if (subModelRegistry.TryGetValue(subModelName, out temp))
            {
                photonView.RPC("MM_AddSubModel", PhotonTargets.All, subModelName);
            }
        }
    }

    [PunRPC]
    public void MM_AddSubModel(string subModelName)
    {
        Debug.Log(photonView.owner + " requests addition of " + subModelName + " to their model");
        Transform subModel = Instantiate(subModelRegistry[subModelName], transform);
        activeSubModels.Add(subModelName);
        subModel.name = subModelName;
    }

    public void RemoveSubModel(string subModelName)
    {
        if (photonView.isMine)
        {
            photonView.RPC("MM_RemoveSubModel", PhotonTargets.All, subModelName);
        }
    }

    [PunRPC]
    public void MM_RemoveSubModel(string subModelName)
    {
        if (activeSubModels.Contains(subModelName))
        {
           
            Transform submodel = transform.Find(subModelName);
            if (submodel != null)
            {
                activeSubModels.Remove(subModelName);
                Destroy(submodel.gameObject);
            }
        }
    }
}
