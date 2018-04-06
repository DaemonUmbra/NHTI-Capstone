using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ModelManager : Photon.MonoBehaviour
{
    PlayerStats pStats;
    private Dictionary<string, Transform> modelRegistry = new Dictionary<string, Transform>();
    private Dictionary<string, Transform> subModelRegistry = new Dictionary<string, Transform>();
    private List<string> activeSubModels = new List<string>();
    private string currentModel = "Default";

    public delegate void VoidDelegate();

    public class ModelChangeEventArgs: EventArgs
    {
        public string oldModelName;
        public Transform oldModel;
        public string newModelName;
        public Transform newModel;
    }

    private void OnModelChanged(string newModel, string oldModel)
    {
        EventHandler<ModelChangeEventArgs> handler = ModelChanged;
        ModelChangeEventArgs args = new ModelChangeEventArgs();
        args.oldModelName = oldModel;
        args.newModelName = newModel;
        args.newModel = modelRegistry[newModel];
        args.oldModel = modelRegistry[oldModel];
        ModelChanged(this, args);
    }

    public event EventHandler<ModelChangeEventArgs> ModelChanged;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        pStats = GetComponent<PlayerStats>();
        LoadModels();
        LoadSubModels();
    }

    private void LoadModels()
    {
        modelRegistry.Add("Default", Resources.Load<Transform>("PlayerModels/Default"));
        modelRegistry.Add("Aliens", Resources.Load<Transform>("PlayerModels/UFO"));
        modelRegistry.Add("Beam", Resources.Load<Transform>("PlayerModels/DefaultBeam"));
    }

    private void LoadSubModels()
    {
        subModelRegistry.Add("Beam", Resources.Load<Transform>("PlayerModels/Beam"));
    }

    public void SetModel(string modelName)
    {
        if (photonView.isMine)
        {
            Transform temp;
            if (modelRegistry.TryGetValue(modelName, out temp))
            {
                photonView.RPC("MM_SetModel", PhotonTargets.All, modelName);
                OnModelChanged(modelName,currentModel);
                currentModel = modelName;
            }
        }
    }

    [PunRPC]
    public void MM_SetModel(string modelName)
    {
        Debug.Log(photonView.owner + " requests change to model: " + modelName);
        Transform CurrentModel = transform.Find("Player Model");
        Destroy(CurrentModel.gameObject);
        Transform model = Instantiate(modelRegistry[modelName],transform);
        model.name = "Player Model";
    }

    public void AddSubModel(string subModelName)
    {
        if (photonView.isMine)
        {
            Transform temp;
            if (subModelRegistry.TryGetValue(subModelName, out temp))
            {
                photonView.RPC("MM_AddSubModel", PhotonTargets.All, subModelName);
                activeSubModels.Add(subModelName);
            }
        }
    }

    [PunRPC]
    public void MM_AddSubModel(string subModelName)
    {
        Debug.Log(photonView.owner + " requests addition of " + subModelName + " to their model");
        Transform CurrentModel = transform.Find("Player Model");
        Transform subModel = Instantiate(subModelRegistry[subModelName], CurrentModel);
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
            Transform CurrentModel = transform.Find("Player Model");
            Transform submodel = CurrentModel.Find(subModelName);
            if (submodel != null)
            {
                Destroy(submodel);
            }
        }
    }
}
