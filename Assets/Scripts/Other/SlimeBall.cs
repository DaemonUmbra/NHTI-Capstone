using UnityEngine;

public class SlimeBall : Projectile
{
    Vector3 direction;
    GameObject plr;
    float force = 20;
    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.velocity = (transform.forward + (transform.up / 5)) * force;
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