using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour {

    // Keyed list of abilities by name
    Dictionary<string, BaseAbility> Abilities;
    PhotonView _pv;

	// Use this for initialization
	void Awake () {
        _pv = GetComponent<PhotonView>();
        Abilities = new Dictionary<string, BaseAbility>();
	}
	
	// Update is called once per frame
	void Update () {

        _pv.RPC("OnUpdate", PhotonTargets.All);

        /*** SINGLE PLAYER ***
        // Call BaseAbility Updates for owned abilities
		foreach (BaseAbility a in Abilities.Values)
        {
            if (a.IsActive)
            {
                a.OnUpdate();
            }
        }*/
	}

    // Check if the player has an ability
    public bool HasAbility<T>() where T : BaseAbility
    {
        return GetComponent<T>() != null;
    }
    public bool HasAbility(BaseAbility ability)
    {
        return GetComponent(ability.GetType()) != null;
    }
    public bool HasAbility(string name)
    {
        // Get ability from dictionary
        BaseAbility target = Abilities[name];

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
    [PunRPC]
    public void AddAbility<T>() where T : BaseAbility
    {
        // Make sure the ability isn't already there
        if (!HasAbility<T>())
        {
            // Add ability to player and register it
            BaseAbility ability = gameObject.AddComponent<T>();
            RegisterAbility(ability);
        }
    }
    [PunRPC]
    public void AddAbility(BaseAbility ability)
    {
        // Make sure the ability isn't already there
        if (!HasAbility(ability))
        {
            // Add ability to player and register it
            BaseAbility newAbility = (BaseAbility)gameObject.AddComponent(ability.GetType());
            Debug.Log("Ability added: " + newAbility.GetName);
            RegisterAbility(newAbility);
        }
        else
        {
            Debug.LogError(ability.GetName + " already owned by player.");
        }
    }

    // Remove ability
    [PunRPC]
    public void RemoveAbility<T>() where T : BaseAbility
    {
        // Get ability
        BaseAbility ability = gameObject.GetComponent<T>();
        if (ability)
        {
            // Unregister ability and destroy it
            UnregisterAbility(ability);
            Destroy(ability);
        }
    }
    [PunRPC]
    public void RemoveAbility(BaseAbility ability)
    {
        // Get ability
        BaseAbility newAbility = (BaseAbility)gameObject.GetComponent(ability.GetType());
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

    // Register ability
    private void RegisterAbility(BaseAbility ability)
    {
        // Run function for when the ability is added
        ability.OnAbilityAdd();
        // Add ability to dictionary
        Abilities.Add(ability.GetName, ability);
        Debug.Log(ability.GetName);
    }

    // Unregister ability
    private void UnregisterAbility(BaseAbility ability)
    {
        string aName = ability.GetName;
        // Run function for when the ability is added
        ability.OnAbilityRemove();
        // Remove ability from dictionary
        Abilities.Remove(aName);
    }

    public Dictionary<string,BaseAbility> ListAbilities()
    {
        return Abilities;
    }
}
