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
    public class Powerup_Growth : BaseAbility
    {
        protected new string Name = "Growth";
        private Vector3 OriginalScale;
        public float GrowthFactor = 2;
        public override void OnAbilityAdd()
        {
            OriginalScale = transform.localScale;
            transform.localScale = OriginalScale * GrowthFactor;
        }

        public override void OnUpdate()
        {

        }

        public override void OnAbilityRemove()
        {

            transform.localScale = OriginalScale;
        }
    }
}
