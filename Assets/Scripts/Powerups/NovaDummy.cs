using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NovaDummy : Photon.MonoBehaviour
{
    private List<Transform> Affected = new List<Transform>();
    private Vector3 ScaleStepVector;
    private AudioSource audioSource;
    private AudioManager audioManager;

    public float ExplosionSize = 15f;
    public float ExplosionTime = 1f;
    public float ScaleStep = 1f;
    public float ExplosionForce = 17f;
    public float ExplosionDamage = 75f;

    // Use this for initialization
    void Start()
    {
        audioManager = gameObject.GetComponent<AudioManager>();
        audioSource = audioManager.GetNewAudioSource("Nova");
        audioManager.PlayOneShot("Nova", "NovaExplosion",.5f);
        ScaleStepVector = new Vector3(ScaleStep, ScaleStep, ScaleStep);
        transform.localScale = Vector3.zero;
        StartCoroutine(Explode(ExplosionSize));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Explode(float size)
    {
        bool Exploding = true;
        bool Stage1 = true;
        while (Exploding)
        {
            if (transform.localScale.magnitude < size && Stage1)
            {
                //Grow
                transform.localScale += ScaleStepVector;
            }
            else
            {
                if (transform.localScale == Vector3.zero)
                {
                    Affected.Clear();
                    Exploding = false;
                    StartCoroutine(WaitForSound());
                }
                if (transform.localScale.magnitude >= size) { Stage1 = false; }
                //Shrink
                transform.localScale -= ScaleStepVector;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator WaitForSound()
    {
        float StartTime = Time.time;
        while(Time.time <= StartTime + audioManager.ClipRegistry["NovaExplosion"].length)
        {
            yield return new WaitForEndOfFrame();
        }
            PhotonNetwork.Destroy(gameObject);
        if(gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (photonView.isMine)
        {
            if (!Affected.Contains(other.transform))
            {
                if (other.GetComponent<PlayerStats>() != null)
                {
                    other.GetComponent<PlayerStats>().TakeDamage(ExplosionDamage, gameObject);
                    Affected.Add(other.transform);
                }
                if (other.GetComponent<Rigidbody>() != null)
                {
                    other.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, transform.position, ExplosionSize);
                }
            }
        }
    }
}
