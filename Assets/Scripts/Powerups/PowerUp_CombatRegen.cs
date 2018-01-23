using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_CombatRegen : PassiveAbility {

    [HideInInspector]
    public PlayerStats PS;

    public override void OnAbilityAdd()
    {
        Name = "Combat Regeneration";
        Debug.Log(Name + " is Added");
        PS = GetComponent<PlayerStats>();
    }

    public override void OnAbilityRemove()
    {
        base.OnAbilityRemove();
    }

    public override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    /*IEnumerator Regen()
    {
        Waiting for Brendan to work on PlayerStats
    }*/
}
