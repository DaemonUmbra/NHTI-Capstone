using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_ThunderCloud : PassiveAbility
{
    PlayerStats ps;
    private float timeLimit = 15.0f;
    private float timer;
    public override void OnAbilityAdd()
    {
        ps = GetComponentInParent<PlayerStats>();

        ps.WalkSpeed += 1.0f;
        ps.JumpPower += 1.0f;
        ps.dmgAdd += 2.0f;

        base.OnAbilityAdd();
    }

    public override void OnAbilityRemove() 
    {
        ps = GetComponentInParent<PlayerStats>();

        ps.WalkSpeed -= 1.0f;
        ps.JumpPower -= 1.0f;
        ps.dmgAdd -= 2.0f;

        base.OnAbilityRemove();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeLimit)
        {
            OnAbilityRemove();
        }
    }
}
