using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class Powerup_HighJump : PassiveAbility
    {
        public float JumpBoost = 2;

        public override void OnAbilityAdd()
        {
            Name = "High Jump";
            if (photonView.isMine)
            {
                GetComponent<PlayerMotor>().JumpMultiplier += JumpBoost;
            }
           
            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            base.OnAbilityRemove();
            if (photonView.isMine)
            {
                GetComponent<PlayerStats>().JumpPower -= JumpBoost;
            }
        }
    }
}
