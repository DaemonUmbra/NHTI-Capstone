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
    [PunRPC]
    public abstract void OnAbilityAdd();

    /// <summary>
    /// Called by the ability manager on each update step. 
    /// Use this instead of Unity's Update()
    /// </summary>
    [PunRPC]
    public abstract void OnUpdate();

    /// <summary>
    /// Called when an ability is removed from the player
    /// </summary>
    [PunRPC]
    public virtual void OnAbilityRemove()
    {
        active = false;
    }

    #endregion

    // Access name
    
    // Access active status
    public bool IsActive { get { return active; } }


}
