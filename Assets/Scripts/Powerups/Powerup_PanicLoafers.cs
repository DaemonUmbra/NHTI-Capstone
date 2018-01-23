using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_PanicLoafers : PassiveAbility {

    [HideInInspector]
    public PlayerStats PS;
    public float WalkSpeed;

    public override void OnAbilityAdd()
    {
        Name = "Panic Loafers";
        Debug.Log(Name + " added");
        PS = GetComponent<PlayerStats>();
        WalkSpeed = PS.WalkSpeed; // Get current walkspeed to save for removal of ability
        PS.WalkSpeed = PS.WalkSpeed * 3; // Triple Walkspeed
        StartCoroutine(PanicTime()); // Start timer
    }

    public override void OnAbilityRemove()
    {
        PS.WalkSpeed = WalkSpeed; // Set walk speed back to the original walk speed
        base.OnAbilityRemove(); // Remove the ability
    }

    public override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator PanicTime()
    {
        yield return new WaitForSecondsRealtime(5); // Ability duration
        OnAbilityRemove(); // Remove ability (Event over)
    }
}
