using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Programmer Assigned: Steven Zachary
 * Power-up: Combat Regeneration
 * Description: Constantly restores a small amount of health to the player after a set amount of time.
 */

public class PowerUp_CombatRegen : PassiveAbility {

    [HideInInspector]
    public PlayerStats PS;
    public bool Active = false;

    public override void OnAbilityAdd()
    {
        Active = true; // Sets power up as Active
        Name = "Combat Regeneration";
        Debug.Log(Name + " Added");
        PS = GetComponent<PlayerStats>();
        StartCoroutine(Regen()); // Begins the regen process
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
        while (Active) // While the player holds the power up, player gains 10 HP every 20 seconds
        {
            PS.GainHp(10.0f);
            yield return new WaitForSecondsRealtime(20);
        }
    }
}
