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
    private float startTime = 0f;

    public void Start()
    {
        if(photonView.isMine)
        {
            GetShooter();
            photonView.RPC("Shoot", PhotonTargets.All);
        }
        
        
    }

    public void Update()
    {
        if (Time.time >= startTime + lifetime)
        {
            if(photonView.isMine)
                PhotonNetwork.Destroy(photonView);
        }
    }

    private void GetShooter()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            if(player.GetPhotonView().isMine)
            {
                Debug.Log("Player found!");
                IgnorePlayer(player);
                break;
            }
        }
    }

    // Ignore collision with player
    public void IgnorePlayer(GameObject player)
    {
        int viewId = player.GetPhotonView().viewID;
        photonView.RPC("RPC_IgnorePlayer", PhotonTargets.All, viewId);
    }

    [PunRPC]
    private void Shoot()
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

        // Set the start time of the bullet
        startTime = Time.time;

        // Get rigidbody reference
        rb = gameObject.GetComponent<Rigidbody>();

        // Apply velocity
        rb.velocity = transform.forward * speed;
    }

    
    [PunRPC]
    private void RPC_IgnorePlayer(int playerId)
    {
        Debug.Log("Ignoring player.....");
        // Find gameobject
        PhotonView pv = PhotonView.Find(playerId);
        if (pv == null)
        {
            Debug.LogError("Photon view not found!");
            return;
        }
        // Set shooter to gameobject
        shooter = pv.gameObject;

        shooterStats = shooter.GetComponent<PlayerStats>();
        if (shooterStats == null)
            Debug.LogError("No player stats found on shooter.");

        Physics.IgnoreCollision(shooter.GetComponent<Collider>(), GetComponent<Collider>());
    }
    

    private void OnTriggerEnter(Collider other)
    {
        GameObject hit = other.gameObject;


        PlayerStats hitStats = hit.GetComponent<PlayerStats>();
        if (hitStats)
        {
            hitStats.TakeDamage(damage);

            if (photonView.isMine)
            {
                PhotonNetwork.Destroy(photonView);
            }

        }
    }
}