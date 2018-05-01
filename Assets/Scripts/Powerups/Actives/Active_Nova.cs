
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups {
    public class Active_Nova : ActiveAbility
    {
        private AbilityManager AbilityManager;
        private GameObject Explosion;
        private AudioSource audioSource;
        private bool fired = false;


        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            Name = "Nova";
            Icon = Resources.Load<Sprite>("Images/Nova");
            Tier = PowerupTier.Rare;
        }

        public override void OnAbilityAdd()
        {
            base.OnAbilityAdd();
            AbilityManager = GetComponent<AbilityManager>();
        }
        protected override void Activate()
        {
            base.Activate();
            if (fired == false)
            {
                fired = true; //HACK: Should fix Cody's issue
                Explosion = PhotonNetwork.Instantiate("NovaExplosion", transform.position, transform.rotation, 0);
                Explosion.GetComponent<NovaDummy>().SetOwner(gameObject);
            }
        }

        public override void OnAbilityRemove()
        {
            base.OnAbilityRemove();
        }
    }
}