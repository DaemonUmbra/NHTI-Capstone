using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NovaDummy : Photon.MonoBehaviour
{
    private Vector3 ScaleStepVector;
    private AudioSource audioSource;
    private AudioManager audioManager;
    public PlayerStats ownerStats;

    private float soundStart;

    public float ExplosionSize = 30f;
    public float ExplosionTime = 1f;
    public float ScaleStep = 1f;
    public float ExplosionForce = 17f;
    public float ExplosionDamage = 75f;

    // Use this for initialization
    void Start()
    {
        audioManager = gameObject.GetComponent<AudioManager>();
        soundStart = Time.time;
        ScaleStepVector = new Vector3(ScaleStep, ScaleStep, ScaleStep);
        transform.localScale = Vector3.zero;
        StartCoroutine(Explode(ExplosionSize));
    }

    IEnumerator Explode(float size)
    {
        audioSource = audioManager.GetNewAudioSource("Nova");
        audioManager.PlayOneShot("Nova", "NovaExplosion", 1f);
        ExplosionState NovaState = ExplosionState.Expanding;
        bool soundPlaying = true;
        bool Exploding = true;
        while (Exploding)
        {
            soundPlaying = Time.time <= soundStart + audioManager.ClipRegistry["NovaExplosion"].length;
            switch (NovaState)
            {
                case ExplosionState.Expanding:
                    {
                        //Transformation
                        transform.localScale += ScaleStepVector;
                        //Check for next stage
                        if (transform.localScale.magnitude >= size) { NovaState = ExplosionState.Shrinking; }
                        break;
                    }
                case ExplosionState.Shrinking:
                    {
                        //Transformation
                        transform.localScale -= ScaleStepVector;
                        //Check for next stage
                        if(transform.localScale.magnitude <= Vector3.zero.magnitude)
                        {
                            transform.localScale = Vector3.zero;
                            NovaState = ExplosionState.Ending;
                        }
                        break;
                    }
                case ExplosionState.Ending:
                    {
                        if (!soundPlaying)
                        {
                            PhotonNetwork.Destroy(gameObject);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            //if (transform.localScale.magnitude < size && Stage1)
            //{
            //    //Grow
            //    
            //}
            //else
            //{
            //    if (transform.localScale == Vector3.zero)
            //    {
            //        Affected.Clear();
            //        Exploding = false;
            //        StartCoroutine(WaitForSound());
            //    }
            //    
            //    //Shrink
            //    transform.localScale -= ScaleStepVector;
            //}
            yield return new WaitForEndOfFrame();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other);
        if (PhotonNetwork.isMasterClient)
        {
            if (other.GetComponent<PlayerStats>() != null)
            {
                other.GetComponent<PlayerStats>().TakeDamage(ExplosionDamage, gameObject);
            }
            if (other.GetComponent<Rigidbody>() != null)
            {
                other.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, transform.position, ExplosionSize);
            }
        }
    }

    private enum ExplosionState
    {
        Invalid,
        Expanding,
        Shrinking,
        Ending
    }
}
