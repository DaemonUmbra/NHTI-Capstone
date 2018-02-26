using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_HighJump : PassiveAbility {
    public float JumpBoost = .2f;

    public override void OnAbilityAdd()
    {
        Name = "High Jump";
        base.OnAbilityAdd();
        GetComponent<PlayerStats>().JumpPower += JumpBoost;
    }

    public override void OnAbilityRemove()
    {
        base.OnAbilityRemove();
        GetComponent<PlayerStats>().JumpPower -= JumpBoost;
    }
}
