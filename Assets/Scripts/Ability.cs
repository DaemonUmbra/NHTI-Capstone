using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Ability : MonoBehaviour {

    protected string Name = "New Ability";
    protected bool active = true;

    /// <summary>
    /// Called when abilities are added to a player
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

    // Access name
    public string GetName { get { return Name; } }
    // Access active status
    public bool IsActive { get { return active; } }


}
