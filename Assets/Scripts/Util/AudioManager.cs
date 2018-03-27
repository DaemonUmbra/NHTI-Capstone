using UnityEngine;
using System.Collections.Generic;
using System;
<<<<<<< HEAD

public class AudioManager : Photon.MonoBehaviour
{
    [SerializeField]
    Dictionary<string, AudioSource> AudioSources;
=======
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
    }

>>>>>>> 57f82a68aaeb6a4e40a6ebb45f160b01ce1fb3da
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
<<<<<<< HEAD
=======
            AudioSources[name].rolloffMode = AudioRolloffMode.Linear;
            AudioSources[name].spatialBlend = 1;
            AudioSources[name].spread = 360;
            AudioSources[name].velocityUpdateMode = AudioVelocityUpdateMode.Auto;
            AudioSources[name].maxDistance = 150;
>>>>>>> 57f82a68aaeb6a4e40a6ebb45f160b01ce1fb3da
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
<<<<<<< HEAD
        photonView.RPC("AM_SetClip", PhotonTargets.All, new object[] { name, clip });
    }

    [PunRPC]
    public void AM_SetClip(string name, AudioClip clip)
    {
        GetExistingAudioSource(name).clip = clip;
=======
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
>>>>>>> 57f82a68aaeb6a4e40a6ebb45f160b01ce1fb3da
    }

    public void PlayClip(string name)
    {
<<<<<<< HEAD
        photonView.RPC("AM_PlayClip", PhotonTargets.All, new object[] { name });
=======
        if (photonView.isMine)
        {
            photonView.RPC("AM_PlayClip", PhotonTargets.All, new object[] { name });
        }
>>>>>>> 57f82a68aaeb6a4e40a6ebb45f160b01ce1fb3da
    }

    [PunRPC]
    public void AM_PlayClip(string name)
    {
        GetExistingAudioSource(name).Play();
    }

    public void StopClip(string name)
    {
<<<<<<< HEAD
        photonView.RPC("AM_StopClip", PhotonTargets.All, new object[] { name });
=======
        if (photonView.isMine)
        {
            photonView.RPC("AM_StopClip", PhotonTargets.All, new object[] { name });
        }
>>>>>>> 57f82a68aaeb6a4e40a6ebb45f160b01ce1fb3da
    }

    [PunRPC]
    public void AM_StopClip(string name)
    {
        GetExistingAudioSource(name).Stop();
    }

    public void SetVolume(string name, float volume)
    {
<<<<<<< HEAD
        photonView.RPC("AM_SetVolume", PhotonTargets.All, new object[] { name, volume });
=======
        if (photonView.isMine)
        {
            photonView.RPC("AM_SetVolume", PhotonTargets.All, new object[] { name, volume });
        }
>>>>>>> 57f82a68aaeb6a4e40a6ebb45f160b01ce1fb3da
    }

    [PunRPC]
    public void AM_SetVolume(string name, float volume)
    {
<<<<<<< HEAD
        GetExistingAudioSource(name).volume = volume;
    }

    public void PlayOneShot(string name, AudioClip clip, float? volume)
    {
        photonView.RPC("AM_PlayOneShot", PhotonTargets.All, new object[] { name, clip, volume });
    }

    [PunRPC]
    public void AM_PlayOneShot(string name, AudioClip clip, float volume = 0.5f)
    {
        GetExistingAudioSource(name).PlayOneShot(clip, volume);
=======
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
>>>>>>> 57f82a68aaeb6a4e40a6ebb45f160b01ce1fb3da
    }
}
