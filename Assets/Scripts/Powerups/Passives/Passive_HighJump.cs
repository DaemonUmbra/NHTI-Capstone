using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class Passive_HighJump : PassiveAbility
    {
        public float JumpBoost = 2;

        private void Awake()
        {
            Name = "High Jump";
        }
        public override void OnAbilityAdd()
        {
            
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
                GetComponent<PlayerMotor>().JumpMultiplier -= JumpBoost;
            }
        }
    }
}
