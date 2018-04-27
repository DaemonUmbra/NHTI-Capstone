using UnityEngine;

/*
 * Programmer Assigned: Steven Zachary, Brodey Lajoie
 * Power-up: Blink
 * Description: On button press the player teleports forward
 */

namespace Powerups
{
    public class Active_Blink : ActiveAbility
    {
        [HideInInspector]
        public PlayerController playercontrol;
        public float BlinkDistance = 12; // For debugging purposes. Once this has been determined, will be set to HideInInspector

        private AudioManager audioManager;
        private AudioSource audioSource;
        private AudioClip blink;

        public float blinkVolume = 1f;

        // Use this for initialization
        private void Awake()
        {
            // Set name
            Name = "Blink";
            Icon = Resources.Load<Sprite>("Images/Blink");
            Tier = PowerupTier.Rare;
            Cooldown = 1f;
        }

        public override void OnAbilityAdd()
        {
            playercontrol = GetComponent<PlayerController>();
            audioManager = gameObject.GetComponent<AudioManager>();
            audioSource = audioManager.GetNewAudioSource(Name);
            audioSource.playOnAwake = false;

            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            audioManager.DeleteAudioSource(Name);

            // Call base function
            base.OnAbilityRemove();
        }
        protected override void Activate()
        {
            base.Activate();
            // Checks if player is blocked by a wall or player, only allows activate if the raycast returns false
                Vector3 destination = transform.position + transform.forward * BlinkDistance;
                RaycastHit hit;
                if (Physics.Linecast(transform.position, destination, out hit))
                {
                    destination = transform.position + transform.forward * (hit.distance - 1.5f);
                }

                destination.y = 1000;
                if (Physics.Raycast(destination, -Vector3.up, out hit))
                {
                    destination = hit.point;
                    destination.y += 2;
                    transform.position = destination;
                }
                //    if (Physics.Raycast(transform.position, transform.TransformDirection(transform.forward), BlinkDistance) == false)
                //    {
                //        if (photonView.isMine)
                //        {
                //            transform.position = transform.forward * BlinkDistance;
                //        }

                //        **Cooldown * *

                //}
                photonView.RPC("RPC_Activate_Blink", PhotonTargets.All);
            
        }


        [PunRPC]
        protected void RPC_Activate_Blink()
        {
            {
                
                gameObject.GetComponent<AudioManager>().PlayOneShot(Name, "Blink", blinkVolume);
            }
        }

       
    }
}