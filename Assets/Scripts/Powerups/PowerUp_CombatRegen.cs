using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_CombatRegen : PassiveAbility {

    [HideInInspector]
    public PlayerStats PS;
    public bool Active = false;

    public override void OnAbilityAdd()
    {
        Active = true;
        Name = "Combat Regeneration";
        Debug.Log(Name + " Added");
        PS = GetComponent<PlayerStats>();
        StartCoroutine(Regen());
    }

    public override void OnAbilityRemove()
    {
        Active = false;
        base.OnAbilityRemove();
    }

    public override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator Regen()
    {
        while (Active)
        {
            PS.GainHp(10.0f);
            yield return new WaitForSecondsRealtime(20);
        }
    }
}
