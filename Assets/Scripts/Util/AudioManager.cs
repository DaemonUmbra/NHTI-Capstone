using UnityEngine;
using System.Collections.Generic;
using System;
using ExitGames.Client.Photon;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public partial class AudioManager : Photon.MonoBehaviour
{
    public Dictionary<string, AudioSource> AudioSources { get; private set; }
    public const float defaultVolume = 0.5f;

    //HACK: Wasteful in terms of memory, every player object has an instance of every sound
    private Dictionary<string, AudioClip> clipRegistry = new Dictionary<string, AudioClip>();

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        LoadClips();
    }

    private void LoadClips()
    {
        clipRegistry.Add("NYEH!", Resources.Load<AudioClip>("Sounds/NYEH"));
        clipRegistry.Add("NYEH!2", Resources.Load<AudioClip>("Sounds/NYEH2"));
        clipRegistry.Add("Blink", Resources.Load<AudioClip>("Sounds/blink"));
        clipRegistry.Add("Shockwave", Resources.Load<AudioClip>("Sounds/ShockwaveTemp"));
        clipRegistry.Add("Beam", Resources.Load<AudioClip>("Sounds/Beam4Sec"));
        clipRegistry.Add("Jump", Resources.Load<AudioClip>("Sounds/HighJump"));
    }

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
            AudioSources[name].rolloffMode = AudioRolloffMode.Linear;
            AudioSources[name].spatialBlend = 1;
            AudioSources[name].spread = 360;
            AudioSources[name].velocityUpdateMode = AudioVelocityUpdateMode.Auto;
            AudioSources[name].maxDistance = 75;
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
        if (photonView.isMine)
        {
            float[] data = new float[clip.samples];
            clip.GetData(data, 0);
            photonView.RPC("AM_SetClip", PhotonTargets.All, new object[] { name, data });
        }
    }

    [PunRPC]
    public void AM_SetClip(string name, string clipName)
    {
        try
        {
            GetExistingAudioSource(name).clip = clipRegistry[clipName];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void PlayClip(string name)
    {
        if (photonView.isMine)
        {
            photonView.RPC("AM_PlayClip", PhotonTargets.All, new object[] { name });
        }
    }

    [PunRPC]
    public void AM_PlayClip(string name)
    {
        GetExistingAudioSource(name).Play();
    }

    public void StopClip(string name)
    {
        if (photonView.isMine)
        {
            photonView.RPC("AM_StopClip", PhotonTargets.All, new object[] { name });
        }
    }

    [PunRPC]
    public void AM_StopClip(string name)
    {
        GetExistingAudioSource(name).Stop();
    }

    public void SetVolume(string name, float volume)
    {
        if (photonView.isMine)
        {
            photonView.RPC("AM_SetVolume", PhotonTargets.All, new object[] { name, volume });
        }
    }

    [PunRPC]
    public void AM_SetVolume(string name, float volume)
    {
            GetExistingAudioSource(name).volume = volume;
    }

    public void PlayOneShot(string name, string clipName, float? volume)
    {
        if (photonView.isMine)
        {
            photonView.RPC("AM_PlayOneShot", PhotonTargets.All, new object[] { name, clipName, volume });
        }
    }

    [PunRPC]
    public void AM_PlayOneShot(string name, string clipName, float volume = defaultVolume)
    {
        try
        {
            GetExistingAudioSource(name).PlayOneShot(clipRegistry[clipName], volume);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
