using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    /// <summary>
    /// <para>Author: Colby Prince</para>
    /// <para>Power-Up Name: Growth</para>
    /// <para>Power-Up Category: Passive</para>
    /// <para>Power-Up Effect: Causes player to grow to a larger size, gaining a damage advantage and footfall AoE effect. Disadvantage would be easier to hit</para>
    /// <para>Power-Up Tier: Uncommon</para>
    /// <para>Power-Up Description:  Player model is larger,  gaining damage boost but same walk speed.</para>
    /// </summary>
    /// 
    public class Powerup_Growth : PassiveAbility
    {
        private Vector3 OriginalScale;
        public float GrowthFactor = 2;
        PhotonView pv;
        public override void OnAbilityAdd()
        {
            Name = "Growth";
            OriginalScale = transform.localScale;
            transform.localScale = OriginalScale * GrowthFactor;
            base.OnAbilityAdd();
            
            /*** Handled by base class ***
            pv = PhotonView.Get(this);
            pv.RPC("Growth_AddAbility", PhotonTargets.All);
            */
        }

        /*** Handled by base class ***
        [PunRPC]
        void Growth_AddAbility()
        {
            Name = "Growth";
            OriginalScale = transform.localScale;
            transform.localScale = OriginalScale * GrowthFactor;
        }
        [PunRPC]
        void Growth_RemoveAbility()
        {
            transform.localScale = OriginalScale;
        }
        */

        public override void OnAbilityRemove()
        {
            transform.localScale = OriginalScale;
            base.OnAbilityRemove();
        }
    }
}
