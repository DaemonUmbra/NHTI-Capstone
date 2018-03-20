using UnityEngine;

// Enum of projectile types
public enum ProjectileType { BULLET, LAZER };

[RequireComponent(typeof(Rigidbody))]
public class Projectile : Photon.MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private GameObject _shooter;

    private PlayerStats shooterStats;

    [SerializeField]
    private ProjectileType type;

    public float damage = 0f;
    public float speed = 10f;

    [SerializeField]
    private float lifetime = 3; // Seconds
    private float startTime = 0f;

    public void Start()
    {
        GetShooter();
        Shoot();
    }

    public void Update()
    {
        if (photonView.isMine)
        {
            if (Time.time >= startTime + lifetime)
            {
                PhotonNetwork.Destroy(photonView);
            }
        }
    }

    // Find a reference to the shooter
    private void GetShooter()
    {
        // Finds tagged player who has the same owner as this bullet
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetPhotonView().owner == photonView.owner)
            {
                SetShooter(player);
                //print("Shooter found: " + player.GetPhotonView().owner);
            }
        }
    }
    // Set the shooter reference
    private void SetShooter(GameObject shooter)
    {
        _shooter = shooter;
        shooterStats = _shooter.GetComponent<PlayerStats>();
    }

    private void Shoot()
    {
        // Set shooterStats if shooter was set in inspector
        if (_shooter)
        {
            if (shooterStats)
            {
                damage = shooterStats.EffectiveDamage;
            }
            else
            {
                Debug.LogError("No player stats found on shooter.");
            }
        }

        // Set the start time of the bullet
        startTime = Time.time;

        // Get rigidbody reference
        rb = gameObject.GetComponent<Rigidbody>();

        // Apply velocity
        rb.velocity = transform.forward * speed;
    }


    private void OnTriggerEnter(Collider other)
    {
        GameObject hit = other.gameObject;
        PhotonView hitView = hit.GetPhotonView();
        PlayerStats hitStats = hit.GetComponent<PlayerStats>();
        // Verify hit photon view
        if(hitView)
        {
            // Make sure the bullet isn't hitting it's own player
            if (hitView.owner != photonView.owner && hitStats && photonView.isMine)
            {
                // Apply damage to the player
                hitStats.TakeDamage(damage);
                print("Player hit!");
                PhotonNetwork.Destroy(photonView);
            }   
        }
    }
}