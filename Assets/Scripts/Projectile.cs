using UnityEngine;

// Enum of projectile types
public enum ProjectileType { BULLET, LAZER };

[RequireComponent(typeof(Rigidbody))]
public class Projectile : Photon.MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private GameObject shooter;

    private PlayerStats shooterStats;

    [SerializeField]
    private ProjectileType type;

    public float damage = 0f;
    public float speed = 10f;

    [SerializeField]
<<<<<<< HEAD
    float lifetime = 3; // Seconds
    float startTime = 0f;
=======
    private float lifetime = 3; // Seconds
>>>>>>> 7cdc5fcbfd2a9e56f2fd3397a3458ff715820213

    public void Start()
    {
        // Set shooterStats if shooter was set in inspector
        if (shooter)
        {
            shooterStats = shooter.GetComponent<PlayerStats>();
            if (shooterStats == null)
            {
                Debug.LogError("No player stats found on shooter.");
            }
            else
            {
                damage = shooterStats.EffectiveDamage;
            }
        }

        photonView.RPC("Shoot", PhotonTargets.All);
    }

    public void Update()
    {
        if(Time.time >= startTime + lifetime)
        {
            PhotonNetwork.Destroy(photonView);
        }
    }

    [PunRPC]
    private void Shoot()
    {
        // Get rigidbody reference
        rb = gameObject.GetComponent<Rigidbody>();

        // Apply velocity
        rb.velocity = transform.forward * speed;
    }

    // Ignore collision with player
    public void IgnorePlayer(GameObject player)
    {
        shooter = player;
        shooterStats = shooter.GetComponent<PlayerStats>();
        if (shooterStats == null)
            Debug.LogError("No player stats found on shooter.");

        Physics.IgnoreCollision(shooter.GetComponent<Collider>(), GetComponent<Collider>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        PlayerStats hitStats = hit.GetComponent<PlayerStats>();

        if (hitStats)
        {
            hitStats.TakeDamage(damage, shooter, shooterStats.OnHitEffects);

            PhotonNetwork.Destroy(photonView);
        }
    }
<<<<<<< HEAD
}
=======

    [PunRPC]
    private void RPC_Destroy()
    {
        Destroy(gameObject);
    }
}
>>>>>>> 7cdc5fcbfd2a9e56f2fd3397a3458ff715820213
