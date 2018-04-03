﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ModelManager : Photon.MonoBehaviour
{
    private Dictionary<string, Transform> modelRegistry = new Dictionary<string, Transform>();

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        LoadModels();
    }

    private void LoadModels()
    {
        modelRegistry.Add("Default", Resources.Load<Transform>("PlayerModels/Default"));
        modelRegistry.Add("Aliens", Resources.Load<Transform>("PlayerModels/UFO"));
    }

    public void SetModel(string modelName)
    {
        if (photonView.isMine)
        {
            Transform temp;
            if (modelRegistry.TryGetValue("Aliens", out temp))
            {
                photonView.RPC("MM_SetModel", PhotonTargets.All, modelName);
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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}