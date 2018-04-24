using UnityEngine;

public class SlimeBall : Projectile
{
    Vector3 direction;
    GameObject plr;
    float force = 20;
    float startTime;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startTime = Time.time;
        Physics.IgnoreLayerCollision(11, 12, true);
        Debug.Log(Physics.GetIgnoreLayerCollision(11, 12));
        rb.velocity = (transform.forward + (transform.up / 5)) * force;
        //rb.AddForce((transform.forward + (transform.up / 5)) * force);
    }
    private void Update()
    {
        //Debug.Log(Time.time + "  e: " + startTime);
        if (Time.time >= startTime + 1)
        {
            Physics.IgnoreLayerCollision(11, 12, false);
            Debug.Log(Physics.GetIgnoreLayerCollision(11, 12));
        }
    }
    void ResetRB()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            direction = (collision.transform.position - transform.position);
            //direction = direction.normalized;
            // direction.y = Mathf.Abs(direction.y);
            direction.y = 1;
            plr = collision.gameObject;
            PlayerController hitController = plr.GetComponent<PlayerController>();
            Vector3 mult = Vector3.one;
            mult.y = .7f;
            mult.x = mult.z = .5f;

            // Only knockback on master client, position is synced
            if(PhotonNetwork.isMasterClient)
            {
                hitController.ApplyKnockBack(direction, 2, mult);
            }
        }
    }
}