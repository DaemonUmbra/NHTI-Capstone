using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour {

    // Keyed list of abilities by name
    Dictionary<string, Ability> Abilities;

	// Use this for initialization
	void Start () {
        Abilities = new Dictionary<string, Ability>();
        
	}
	
	// Update is called once per frame
	void Update () {

        // Call Ability Updates
		foreach (Ability a in Abilities.Values)
        {
            if (a.IsActive)
            {
                a.OnUpdate();
            }
        }
	}

    public bool HasAbility<T>() where T : Ability
    {
        return GetComponent<T>() != null;
    }
    public void AddAbility<T>() where T : Ability
    {
        // Make sure the ability isn't already there
        if (gameObject.GetComponent<T>() == null)
        {
            // Add ability to player and register it
            Ability ability = gameObject.AddComponent<T>();
            RegisterAbility(ability);
        }
    }
    public void RemoveAbility<T>() where T : Ability
    {
        // Get ability
        Ability ability = gameObject.GetComponent<T>();
        if (ability)
        {
            // Unregister ability and destroy it
            UnregisterAbility(ability);
            Destroy(ability);
        }
    }
    public void RegisterAbility(Ability ability)
    {
        // Run function for when the ability is added
        ability.OnAbilityAdd();
        // Add ability to dictionary
        Abilities.Add(ability.GetName, ability);
        Debug.Log(ability.GetName);
    }
    public void UnregisterAbility(Ability ability)
    {
        string aName = ability.GetName;
        // Run function for when the ability is added
        ability.OnAbilityRemove();
        // Remove ability from dictionary
        Abilities.Remove(aName);
    }
}
