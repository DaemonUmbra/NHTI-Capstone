using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Ability : MonoBehaviour {

    protected string Name = "New Ability";
    protected bool active = true;

    public abstract void OnAbilityAdd();
    public abstract void OnUpdate();
    public virtual void OnAbilityRemove()
    {
        active = true;
    }
    // Access name
    public string GetName { get { return Name; } }
    public bool IsActive { get { return active; } }


}
