using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all abilities/powerups. 
/// Abilities should be derived from either ActiveAbility or PassiveAbility
/// </summary>
public abstract class BaseAbility : MonoBehaviour {

    [SerializeField]
    protected string Name = "New Ability";
    public string GetName { get { return Name; } }
    [SerializeField]
    protected bool active = true;


    #region Abstract Methods

    /// <summary>
    /// Called when abilities are added to a player.
    /// Make sure to set the ability name string!
    /// </summary>
    public abstract void OnAbilityAdd();
    /// <summary>
    /// Called by the ability manager on each update step. 
    /// Use this instead of Unity's Update()
    /// </summary>
    public abstract void OnUpdate();
    /// <summary>
    /// Called when an ability is removed from the player
    /// </summary>
    public virtual void OnAbilityRemove()
    {
        active = true;
    }

    #endregion

    // Access name
    
    // Access active status
    public bool IsActive { get { return active; } }


}
