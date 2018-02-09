using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all abilities/powerups. 
/// Abilities should be derived from either ActiveAbility or PassiveAbility
/// </summary>
public abstract class BaseAbility : Photon.MonoBehaviour {

    [SerializeField]
    protected string Name = "New Ability";
    public string GetName { get { return Name; } }
    [SerializeField]
    protected bool active = false;
    public bool IsActive { get { return active; } }


    #region Abstract Methods
    /// <summary>
    /// Called when abilities are added to a player.
    /// Make sure to set the ability name string!
    /// </summary>
    public virtual void OnAbilityAdd()
    {
        active = true;
    }

    /// <summary>
    /// Called by the ability manager on each update step. 
    /// Use this instead of Unity's Update()
    /// </summary>
    public virtual void OnUpdate()
    {
        // Nothing in base class update yet. Still call it in your overrides.
    }

    /// <summary>
    /// Called when an ability is removed from the player
    /// </summary>
    public virtual void OnAbilityRemove()
    {
        active = false;
    }
    #endregion
}
