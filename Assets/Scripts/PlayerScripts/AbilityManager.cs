using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : Photon.MonoBehaviour {

    // Keyed list of abilities by name
    Dictionary<string, BaseAbility> _abilities;

	// Use this for initialization
	void Awake () {
        _abilities = new Dictionary<string, BaseAbility>();
	}
	
	// Update is called once per frame
	void Update () {
        // Call BaseAbility Updates for owned abilities
		foreach (BaseAbility a in _abilities.Values)
        {
            if (a.IsActive)
            {
                a.OnUpdate();
            }
        }
	}

    #region Public Methods
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
        BaseAbility target = _abilities[name];

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
    public void AddAbility<T>() where T : BaseAbility
    {
        
        if(HasAbility<T>())
        {
            Debug.LogWarning("Ability already owned by player.");
            return;
        }

        // Add ability locally then call on server
        BaseAbility ability = gameObject.AddComponent<T>();
        RegisterAbility(ability);
        Debug.Log("Ability added: " + ability.GetName);
        //photonView.RPC("RPC_AddAbility", PhotonTargets.Others, ability);
    }
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
        // Add ability on server
        //photonView.RPC("RPC_AddAbility", PhotonTargets.All, ability);
    }

    // Remove ability
    public void RemoveAbility<T>() where T : BaseAbility
    {
        // Get ability
        BaseAbility ability = gameObject.GetComponent<T>();
        if (ability)
        {
            // Unregister ability and destroy it
            UnregisterAbility(ability);
            Debug.Log("Ability removed: " + ability.name);
            Destroy(ability);
        }
        else
        {
            Debug.LogError("Ability not owned. Unable to remove " + ability.GetName);
        }
    }
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
    #endregion

    #region Photon RPCs
    /**** Removed for simplicity, networking in progress ****
    [PunRPC]
    private void RPC_AddAbility(BaseAbility ability)
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
    [PunRPC]
    private void RPC_RemoveAbility(BaseAbility ability)
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
    */
    #endregion

    #region Private Methods
    // Register ability
    private void RegisterAbility(BaseAbility ability)
    {
        // Run function for when the ability is added
        ability.OnAbilityAdd();
        // Add ability to dictionary
        _abilities.Add(ability.GetName, ability);
        Debug.Log(ability.GetName);
    }

    // Unregister ability
    private void UnregisterAbility(BaseAbility ability)
    {
        string aName = ability.GetName;
        // Run function for when the ability is added
        ability.OnAbilityRemove();
        // Remove ability from dictionary
        _abilities.Remove(aName);
    }
    #endregion


    public Dictionary<string,BaseAbility> ListAbilities()
    {
        return _abilities;
    }
}
