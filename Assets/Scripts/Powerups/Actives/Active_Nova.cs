
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups {
    public class Active_Nova : ActiveAbility
    {
        private AbilityManager AbilityManager;
        private GameObject Explosion;


        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            Name = "Nova";
            //TODO: Nova Icon
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
            if (photonView.isMine)
            {
                photonView.RPC("RPC_Nova_Explosion", PhotonTargets.All);
            }
        }

        [PunRPC]
        public void RPC_Nova_Explosion()
        {
            Explosion = PhotonNetwork.Instantiate("NovaExplosion",transform.position,transform.rotation,0);
            AbilityManager.RemoveAbility(this);
        }

        public override void OnAbilityRemove()
        {
            base.OnAbilityRemove();
        }
    }
}