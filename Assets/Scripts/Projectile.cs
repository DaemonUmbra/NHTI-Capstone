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
    private float lifetime = 3; // Seconds

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

    [PunRPC]
    private void Shoot()
    {
        // Get rigidbody reference
        rb = gameObject.GetComponent<Rigidbody>();

        // Apply velocity
        rb.velocity = transform.forward * speed;

        // Destroy after a set numer of seconds
        Destroy(gameObject, lifetime);
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

            photonView.RPC("RPC_Destroy", PhotonTargets.All);
        }
    }

    [PunRPC]
    private void RPC_Destroy()
    {
        Destroy(gameObject);
    }
}