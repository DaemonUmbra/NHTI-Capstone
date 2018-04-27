using UnityEngine;
using System.Collections;

///summary
 /*
Developers and Contributors: Ian Cahoon, Brodey Lajoie

Information
    Name: Beam
    Type: Active
    Effect: Basic fire turns into a sustained beam
    Tier: Rare
  */
///summary

namespace Powerups
{
    public class Active_Beam : ActiveAbility
    {
        private bool onCooldown = false, CurrentlyActive = false;
        private PlayerShoot pShoot;
        private GameObject rayOrigin;
        ModelManager modelManager;
        private GameObject beam;

        private AudioManager audioManager;
        private AudioSource audioSource;
        private AudioClip beamClip;

        public float beamVolume = 1f;

        private void Awake()
        {
            // Set name
            Cooldown = 5f;
            Name = "Beam";
            Icon = Resources.Load<Sprite>("Images/Beam");
            Tier = PowerupTier.Rare;
        }
        public override void OnAbilityAdd()
        {
            Debug.Log(Name + " Added");
            audioManager = gameObject.GetComponent<AudioManager>();
            audioSource = audioManager.GetNewAudioSource(Name);
            audioSource.playOnAwake = false;
            //modelManager = GetComponent<ModelManager>();
            //modelManager.SetModel("Beam");
            // Call base function
            base.OnAbilityAdd();
        }

        protected override void Activate()
        {
            StartCoroutine(Beam());
            
            base.Activate();
        }

        IEnumerator Beam()
        {
            photonView.RPC("RPC_Activate_Beam", PhotonTargets.All);
            modelManager = GetComponent<ModelManager>();
            modelManager.AddSubModel("Beam");
            if (photonView.isMine)
            {
                beam = GameObject.Find("Beam");
            }
           
            yield return new WaitForSecondsRealtime(4);
           photonView.GetComponent<ModelManager>().RemoveSubModel("Beam");
            
        }

        public override void OnAbilityRemove()
        {
            // Call base function
            photonView.GetComponent<ModelManager>().RemoveSubModel("Beam");
            audioManager.DeleteAudioSource(Name);
            base.OnAbilityRemove();
        }



        [PunRPC]
        protected void RPC_Activate_Beam()
        {
            {
                Debug.Log(photonView.owner.NickName + ": Beam!");
                gameObject.GetComponent<AudioManager>().PlayOneShot(Name, "Beam", beamVolume);
            }
        }



    }
}