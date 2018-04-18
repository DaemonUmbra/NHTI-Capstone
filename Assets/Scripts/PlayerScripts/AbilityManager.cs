using System;
using System.Collections.Generic;
using UnityEngine;
using Powerups;

public class AbilityManager : Photon.MonoBehaviour
{
    #region Variables
    // Keyed list of abilities by name

    [SerializeField]
    private List<string> _abilityStrings; //HACK: For debugging

    private Dictionary<string, BaseAbility> _abilities;
    public Dictionary<string, BaseAbility> AbilityList { get { return _abilities; } }

    [SerializeField]
    private int MaxActives = 4;
    [SerializeField]
    private int MaxPassives = 6;
    private List<ActiveAbility> _activeAbilities;
    private List<PassiveAbility> _passiveAbilities;

    public List<ActiveAbility> ActiveAbilities { get { return _activeAbilities; } }
    public List<PassiveAbility> PassiveAbilities { get { return _passiveAbilities; } }
    #endregion


    #region Unity Callbacks
    // Use this for initialization
    private void Awake()
    {
        _abilities = new Dictionary<string, BaseAbility>();
        _activeAbilities = new List<ActiveAbility>();
        _passiveAbilities = new List<PassiveAbility>();
    }

    // Update is called once per frame
    private void Update()
    {
        _abilityStrings.Clear();
        foreach(string str in _abilities.Keys)
        {
            _abilityStrings.Add(str);
        }
        // Call BaseAbility Updates for owned abilities
        foreach (BaseAbility a in _abilities.Values)
        {
            if (a.IsActive)
            {
                a.OnUpdate();
            }
        }
    }
    #endregion


    #region Public Methods
    // Check if ability is owned
    public bool HasAbility(string abilityType)
    {
        // Check if the ability is already owned and unique
        return GetComponent(abilityType) != null;
    }
    public bool HasAbility<T>() where T : BaseAbility
    {
        return HasAbility(typeof(T).GetType().ToString());
    } 
    public bool HasAbility(BaseAbility ability)
    {
        return HasAbility(ability.GetType().ToString());
    }
    public bool HasAbility(Type type)
    {
        if (type != typeof(BaseAbility))
        {
            Debug.LogWarning("Not an ability.");
            return false;
        }

        return HasAbility(type.ToString());
    }

    // Check if an ability can be picked up
    public bool CanPickupAbility(string abilityType)
    {
        // Check if the ability is already owned and unique
        BaseAbility ownedAbility = (BaseAbility)GetComponent(abilityType);
        if (ownedAbility)
        {
            if (ownedAbility.IsUnique)
            {
                Debug.LogWarning(abilityType + " is already owned by player.");
                return false;
            }
        }
        // Check active capacity
        if (abilityType == typeof(ActiveAbility).ToString())
        {
            if (_activeAbilities.Count >= MaxActives)
            {
                Debug.LogWarning("Too many actives. Drop logic will be added soon.");
                return false;
            }
        }
        // Check passive capacity
        else if(abilityType == typeof(PassiveAbility).ToString())
        {
            if(_passiveAbilities.Capacity >= MaxPassives)
            {
                Debug.LogWarning("Too many passives. Drop logic will be added soon.");
                return false;
            }
        }
        // Passed the test!
        return true;
    }
    public bool CanPickupAbility<T>() where T : BaseAbility
    {
        return CanPickupAbility(typeof(T).ToString());
    }
    public bool CanPickupAbility(BaseAbility ability)
    {
        return CanPickupAbility(ability.GetType().ToString());
    }
    public bool CanPickupAbility(Type type)
    {
        if(type != typeof(BaseAbility))
        {
            Debug.LogWarning("Not an ability.");
            return false;
        }
        return CanPickupAbility(type.ToString());
    }

    /// <summary>
    /// Used by the Powerup Debugger to get an ability list with C# class names as keys
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, BaseAbility> GetAbilityClasses()
    {
        Dictionary<string, BaseAbility> output = new Dictionary<string, BaseAbility>();
        foreach(KeyValuePair<string,BaseAbility> kvp in _abilities)
        {
            output.Add(kvp.Value.GetType().Name, kvp.Value);
        }
        return output;
    }

    // Add ability
    public void AddAbility<T>() where T : BaseAbility
    {
        if(CanPickupAbility<T>())
        {
            // Add ability on server
            photonView.RPC("RPC_AddAbility", PhotonTargets.All, typeof(T).ToString());
        }
    }
    public void AddAbility(BaseAbility ability)
    {
        if(CanPickupAbility(ability))
        {
            // Add ability on server
            photonView.RPC("RPC_AddAbility", PhotonTargets.All, ability.GetType().ToString());   
        }
             
    }

    // Remove ability
    public void RemoveAbility<T>() where T : BaseAbility
    {
        // Remove ability on server
        photonView.RPC("RPC_RemoveAbility", PhotonTargets.All, typeof(T).ToString());
    }
    public void RemoveAbility(BaseAbility ability)
    {
        // Remove ability on server
        photonView.RPC("RPC_RemoveAbility", PhotonTargets.All, ability.GetType().ToString());
    }
    
    // Remove all abilities
    public void ResetAbilities()
    {
        foreach(BaseAbility a in _abilities.Values)
        {
            RemoveAbility(a);
        }
    }

    // Trigger ability at an index in the list
    public void TriggerAbility(int index)
    {
        if(index < _activeAbilities.Count)
        {
            photonView.RPC("RPC_TriggerAbility", PhotonTargets.All, index);
        }
    }
    #endregion Public Methods


    #region Photon RPCs
    [PunRPC] private void RPC_AddAbility(string abilityType) // This could and should be optimized once it is working
    {
        // Get ability type from string
        Type t = Type.GetType(abilityType);

        // Make sure the ability isn't already there
        if (CanPickupAbility(abilityType))
        {
            // Add ability to player and register it
            BaseAbility newAbility = (BaseAbility)gameObject.AddComponent(t);
            RegisterAbility(newAbility);
            Debug.Log("Ability added: " + newAbility.GetName);
        }
        else
        {
            Debug.LogWarning(t.Name + " already owned by player.");
        }
    }
    [PunRPC] private void RPC_RemoveAbility(string abilityType)
    {
        // Get ability type from string and retrieve the attached ability
        Type t = ReflectionUtil.GetAbilityTypeFromName(abilityType);
        BaseAbility ability = (BaseAbility)GetComponent(t);

        if (ability)
        {
            // Unregister ability and destroy it
            UnregisterAbility(ability);
            Debug.Log("Ability removed: " + ability.GetName);
            Destroy(ability);
        }
        else
        {
            Debug.LogError("Ability not owned. Unable to remove.");
        }
    }
    [PunRPC] private void RPC_TriggerAbility(int index)
    {
        if(index < _activeAbilities.Count)
        {
            _activeAbilities[index].TryActivate();
        }
    }
    #endregion Photon RPCs


    #region Private Methods
    // Register any ability
    private void RegisterAbility(BaseAbility ability)
    {
        Debug.Log("Registering generic ability: " + ability.GetType().ToString());
        // Register as active or passive
        
        if(ability.GetType().BaseType == typeof(ActiveAbility))
        {
            RegisterAbility((ActiveAbility)ability);
        }
        else if(ability.GetType().BaseType == typeof(PassiveAbility))
        {
            RegisterAbility((PassiveAbility)ability);
        }
    }

    // Register active
    private void RegisterAbility(ActiveAbility ability)
    {
        Debug.Log("registering active");
        // Add to actives
        _activeAbilities.Add(ability);
        // Add to master
        _abilities.Add(ability.GetType().ToString(), ability);
        // Initialize ability
        ability.OnAbilityAdd();
    }

    // Register passive
    private void RegisterAbility(PassiveAbility ability)
    {
        Debug.Log("registering passive");
        // Add to passives
        _passiveAbilities.Add(ability);
        // Add to master
        _abilities.Add(ability.GetType().ToString(), ability);
        // Initialize ability
        ability.OnAbilityAdd();
    }

    // Unregister any ability
    private void UnregisterAbility(BaseAbility ability)
    {
        // Unregister as active or passive
        if (ability.GetType().BaseType == typeof(ActiveAbility))
        {
            UnregisterAbility((ActiveAbility)ability);
        }
        else if (ability.GetType().BaseType == typeof(PassiveAbility))
        {
            UnregisterAbility((PassiveAbility)ability);
        }
    }
    // Unregister active
    private void UnregisterAbility(ActiveAbility ability)
    {
        // Remove from actives
        _activeAbilities.Remove(ability);
        // Add to master
        _abilities.Remove(ability.GetType().ToString());
        // Initialize ability
        ability.OnAbilityRemove();
    }
    // Unregister passive
    private void UnregisterAbility(PassiveAbility ability)
    {
        // Remove from actives
        _passiveAbilities.Remove(ability);
        // Remove from master
        _abilities.Remove(ability.GetType().ToString());
        // Deconstructor
        ability.OnAbilityRemove();
    }
    #endregion Private Methods
}