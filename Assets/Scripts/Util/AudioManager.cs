using UnityEngine;
using System.Collections.Generic;
using System;

public class AudioManager : Photon.MonoBehaviour
{
    [SerializeField]
    Dictionary<string, AudioSource> AudioSources;
    // Use this for initialization
    void Start()
    {
        AudioSources = new Dictionary<string, AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Gets a new managed audio source, returns null upon failure
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public AudioSource GetNewAudioSource(string name)
    {
        try
        {
            AudioSources.Add(name, gameObject.AddComponent<AudioSource>());
            return AudioSources[name];
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
            return null;
        }
    }

    /// <summary>
    /// Gets an existing managed audio source, returns null if none is found.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public AudioSource GetExistingAudioSource(string name)
    {
        try
        {
            return AudioSources[name];
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
            return null;
        }
    }

    public bool DeleteAudioSource(string name)
    {
        if (GetExistingAudioSource(name))
        {
            AudioSources.Remove(name);
            return true;
        }
        else
        {
            return false;
        }
    }
}
