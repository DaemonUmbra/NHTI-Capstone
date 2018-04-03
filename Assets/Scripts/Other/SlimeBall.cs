using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    private float lifetime = 10f;
    private float force = 10f;
    float kbStartTime;
    GameObject plr;
    private bool knockbacked = false;
    private Rigidbody rb;
    Vector3 direction;

    // Use this for initialization
    private void Start()
    {
        //Destroy(gameObject, lifetime);
        rb = gameObject.GetComponent<Rigidbody>();

        Vector3 mp = Input.mousePosition;
        mp.z = 10;
        Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(mp);

        transform.LookAt(mouseLocation);

       // rb.velocity = transform.forward * force;
    }

    // Update is called once per frame
    private void Update()
    {
        if (knockbacked)
        {
            knockback(plr, direction, 20, 1);
        }
    }
    private void knockback(GameObject player, Vector3 dir, float force, float duration)
    {
        float t = Time.time;
        if (t < kbStartTime + duration)
        {
            player.transform.Translate(dir * force * Time.deltaTime);
        }
        else
        {
            knockbacked = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            direction = -(transform.position - other.transform.position);
            direction = direction.normalized;
            direction.y = Mathf.Abs(direction.y);

            kbStartTime = Time.time;
            plr = other.gameObject;
            knockbacked = true;
        }
    }
}