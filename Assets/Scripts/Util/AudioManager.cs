using UnityEngine;
using System.Collections.Generic;
using System;

public class AudioManager : Photon.MonoBehaviour
{
    [SerializeField]
    Dictionary<string, AudioSource> AudioSources;
    public const float defaultVolume = 0.5f;
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
            AudioSources[name].rolloffMode = AudioRolloffMode.Logarithmic;
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
    public void SetClip(string name, AudioClip clip)
    {
        if(photonView.isMine)
        photonView.RPC("AM_SetClip", PhotonTargets.All, new object[] { name, clip });
    }

    [PunRPC]
    public void AM_SetClip(string name, AudioClip clip)
    {
        GetExistingAudioSource(name).clip = clip;
    }

    public void PlayClip(string name)
    {
        if (photonView.isMine)
            photonView.RPC("AM_PlayClip", PhotonTargets.All, new object[] { name });
    }

    [PunRPC]
    public void AM_PlayClip(string name)
    {
        GetExistingAudioSource(name).Play();
    }

    public void StopClip(string name)
    {
        if (photonView.isMine)
            photonView.RPC("AM_StopClip", PhotonTargets.All, new object[] { name });
    }

    [PunRPC]
    public void AM_StopClip(string name)
    {
        GetExistingAudioSource(name).Stop();
    }

    public void SetVolume(string name, float volume)
    {
        if (photonView.isMine)
            photonView.RPC("AM_SetVolume", PhotonTargets.All, new object[] { name, volume });
    }

    [PunRPC]
    public void AM_SetVolume(string name, float volume)
    {
            GetExistingAudioSource(name).volume = volume;
    }

    public void PlayOneShot(string name, AudioClip clip, float? volume)
    {
        if (photonView.isMine)
            photonView.RPC("AM_PlayOneShot", PhotonTargets.All, new object[] { name, clip, volume });
    }

    [PunRPC]
    public void AM_PlayOneShot(string name, AudioClip clip, float volume = defaultVolume)
    {
        if (photonView.isMine)
            GetExistingAudioSource(name).PlayOneShot(clip, volume);
    }
}
