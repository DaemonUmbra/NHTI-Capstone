using UnityEngine;

namespace Powerups
{
    public class Active_ThunderWave : ActiveAbility
    {
        public float upforce = 20;

        public float forwardforce = 500;
        public Vector3 explosionPos;
        public float radius = 20;
        public float up = .1f;
        public float hitDistance = 20;

        public Vector3 origin;
        private Vector3 direction;

        public float sphereRadius = 10;
        public float maxDistance = 10;
        public LayerMask layerMask = 1;

        private PlayerShoot pShoot;

        public float shockwaveVolume = 3f;
        public AudioManager audioManager;
        private AudioSource audioSource;
        private AudioClip thunderWave;
        
        private void Awake()
        {
            Cooldown = 5f;
            Name = "Thunder Wave";
            Icon = Resources.Load<Sprite>("Images/Thunder Wave");
            Tier = PowerupTier.Rare;
        }

        #region Abstract Methods

        /// <summary>
        /// Called when abilities are added to a player.
        /// Make sure to set the ability name string!
        /// </summary>
        public override void OnAbilityAdd()
        {
            
            Debug.Log(Name + " Added");

            audioManager = gameObject.GetComponent<AudioManager>();
            audioSource = audioManager.GetNewAudioSource(Name);
            audioSource.playOnAwake = false;

            base.OnAbilityAdd();
        }

        protected override void Activate() // Will need to be activated by something once we decide how players will trigger abilities.
        {
            photonView.RPC("RPC_Activate_ThunderWave", PhotonTargets.All);
            origin = transform.position;
            direction = Vector3.forward;

            explosionPos = transform.position;

            RaycastHit[] hits = Physics.SphereCastAll(origin, sphereRadius, direction, maxDistance, layerMask);
            foreach (RaycastHit hit in hits)
            {


                if (hit.rigidbody != null && hit.transform.gameObject.tag == "Player")
                {
                    if (hit.transform.gameObject != this.gameObject)
                    {
                        Vector3 hitDir = hit.point;
                        Vector3 forceDir = Quaternion.AngleAxis(0, hitDir) * Vector3.up;
                        hit.rigidbody.AddForce(forceDir * upforce, ForceMode.Impulse);


                        //hit.rigidbody.AddForce(transform.up * upforce);
                        //hit.rigidbody.AddForce(transform.forward * forwardforce);
                        PlayerStats stats;
                        stats = hit.transform.GetComponent<PlayerStats>();
                        stats.TakeDamage(3, gameObject);
                    }
                }

            }

           

            base.Activate();
        }

        [PunRPC]
        protected void RPC_Activate_ThunderWave()
        {
            {
                Debug.Log(photonView.owner.NickName + ": ThunderWave!");
                gameObject.GetComponent<AudioManager>().PlayOneShot(Name, "Shockwave", shockwaveVolume);
            }
        }

        public override void OnUpdate()
        {
            // Call base function
            base.OnUpdate();
        }

        /// <summary>
        /// Called when an ability is removed from the player
        /// </summary>
        public override void OnAbilityRemove()
        {
            audioManager.DeleteAudioSource(Name);
            base.OnAbilityRemove();
        }

        #endregion Abstract Methods
    }
}