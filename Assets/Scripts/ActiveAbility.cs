using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : BaseAbility {
    
    /// <summary>
    /// BaseAbility cooldown in seconds
    /// </summary>
    [SerializeField]
    protected float Cooldown;

    /// <summary>
    /// Activate the ability
    /// </summary>
    public abstract void Activate(); // Check cooldown before activating


}
