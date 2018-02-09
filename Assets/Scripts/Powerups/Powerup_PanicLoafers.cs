using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Programmer Assigned: Steven Zachary
 * Power-up: Panic Loafers
 * Description: After being damaged movement speed is increased by X% for Y seconds, 
 *              each additional stack of this powerup increases the X and Y values.
 */

public class Powerup_PanicLoafers : PassiveAbility {

    [HideInInspector]
    public PlayerStats PS;
    public float WalkSpeed;
    public float Health;

    public override void OnAbilityAdd()
    {
        Name = "Panic Loafers"; // Setting Name of power up
        Debug.Log(Name + " Added");
        PS = GetComponent<PlayerStats>();
        WalkSpeed = PS.WalkSpeed; // Get current walkspeed to save for later use
        Health = PS.Health; // Get current health to keep track of damage
    }

    public override void OnAbilityRemove()
    {
        PS.WalkSpeed = WalkSpeed; // In the event the powerup is removed part way through Panic Time
        base.OnAbilityRemove(); // Remove the ability
    }

    public override void OnUpdate()
    {
        if(PS.Health < Health) // If player is damaged
        {
            Health = PS.Health; // Health variable is set to player's current health
            PS.WalkSpeed = PS.WalkSpeed * 3; // Walkspeed is tripled
            StartCoroutine(PanicTime()); // Begin coroutine for timer
        }

        else if(PS.Health > Health) // If player gains health 
        {
            Health = PS.Health; // Health variable is set to player's current health
        }
    }

    IEnumerator PanicTime() // Timer for running
    {
        yield return new WaitForSecondsRealtime(5); // Wait for 5 seconds
        PS.WalkSpeed = WalkSpeed; // Set player walkspeed back to normal
    }
}
