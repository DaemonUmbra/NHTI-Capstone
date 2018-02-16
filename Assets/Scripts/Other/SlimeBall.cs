using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    private float lifetime = 10f;
    private float force = 10f;

    private float explosionForce = 300;

    private Rigidbody rb;

    // Use this for initialization
    private void Start()
    {
        Destroy(gameObject, lifetime);
        rb = gameObject.GetComponent<Rigidbody>();

        Vector3 mp = Input.mousePosition;
        mp.z = 10;
        Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(mp);

        transform.LookAt(mouseLocation);

        rb.velocity = transform.forward * force;
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, .5f, Vector3.forward, 2f, 1);
        foreach (RaycastHit hit in hits)
        {
            Rigidbody rb = hit.rigidbody;
            if (rb != null)
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    Debug.Log("I hit :" + hit.transform.name);
                    hit.rigidbody.AddExplosionForce(explosionForce, transform.position, 1, 1);
                }
            }
        }
    }
}