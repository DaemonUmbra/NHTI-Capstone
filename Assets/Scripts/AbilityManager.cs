using System;
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

        // Call Ability Updates for owned abilities
		foreach (Ability a in Abilities.Values)
        {
            if (a.IsActive)
            {
                a.OnUpdate();
            }
        }
	}

    // Check if the player has an ability
    public bool HasAbility<T>() where T : Ability
    {
        return GetComponent<T>() != null;
    }
    public bool HasAbility(Ability ability)
    {
        return GetComponent(ability.GetType()) != null;
    }
    public bool HasAbility(string name)
    {
        // Get ability from dictionary
        Ability target = Abilities[name];

        if(target)
        {
            // Double check with player components
            if(GetComponent(target.GetType()) != null)
                return true;
        }
        // False by default
        return false;
    }

    // Add ability
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
    public void AddAbility(Ability ability)
    {
        // Make sure the ability isn't already there
        if (gameObject.GetComponent(ability.GetType()) == null)
        {
            // Add ability to player and register it
            Ability newAbility = (Ability)gameObject.AddComponent(ability.GetType());
            Debug.Log("Ability added: " + newAbility.GetName);
            RegisterAbility(newAbility);
        }
        else
        {
            Debug.LogError(ability.GetName + " already owned by player.");
        }
    }

    // Remove ability
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
    public void RemoveAbility(Ability ability)
    {
        // Get ability
        Ability newAbility = (Ability)gameObject.GetComponent(ability.GetType());
        if (newAbility)
        {
            // Unregister ability and destroy it
            UnregisterAbility(ability);
            Debug.Log("Ability removed: " + newAbility.name);
            Destroy(ability);
        }
        else
        {
            Debug.LogError("Ability not owned. Unable to remove " + ability.GetName);
        }
    }

    // Register an ability
    public void RegisterAbility(Ability ability)
    {
        // Run function for when the ability is added
        ability.OnAbilityAdd();
        // Add ability to dictionary
        Abilities.Add(ability.GetName, ability);
        Debug.Log(ability.GetName);
    }

    // Unregister an ability
    public void UnregisterAbility(Ability ability)
    {
        string aName = ability.GetName;
        // Run function for when the ability is added
        ability.OnAbilityRemove();
        // Remove ability from dictionary
        Abilities.Remove(aName);
    }
    
    
}
