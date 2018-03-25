using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : Photon.MonoBehaviour
{
    #region Variables
    // Keyed list of abilities by name

    [SerializeField]
    private List<string> _abilityStrings; //HACK: For debugging

    private Dictionary<string, BaseAbility> _abilities;
    public Dictionary<string, BaseAbility> AbilityList { get { return _abilities; } }
    #endregion


    #region Unity Callbacks
    // Use this for initialization
    private void Awake()
    {
        _abilities = new Dictionary<string, BaseAbility>();
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

    // Check Ability
    public bool HasAbility<T>() where T : BaseAbility
    {
        return GetComponent<T>() != null;
    }
    public bool HasAbility(BaseAbility ability)
    {
        return GetComponent(ability.GetType()) != null;
    }
    public bool HasAbility(string abilityName)
    {
        return GetComponent(Type.GetType(abilityName)) != null;
    }
    public bool HasAbility(Type type)
    {
        return GetComponent(type) != null;
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
        if (!HasAbility<T>())
        {
            // Add ability on server
            photonView.RPC("RPC_AddAbility", PhotonTargets.All, typeof(T).ToString());
        }
    }
    public void AddAbility(BaseAbility ability)
    {
        // Add ability on server
        photonView.RPC("RPC_AddAbility", PhotonTargets.All, ability.GetType().ToString());        
    }

    // Remove ability
    public void RemoveAbility<T>() where T : BaseAbility
    {
        // Remove ability on server
        photonView.RPC("RPC_RemoveAbility", PhotonTargets.All, typeof(T).ToString());
    }
    public void RemoveAbility(BaseAbility ability)
    {
        if(HasAbility(ability))
        {
            // Remove ability on server
            photonView.RPC("RPC_RemoveAbility", PhotonTargets.All, ability.GetType().ToString());
        }
    }
    
    // Remove all abilities
    public void ResetAbilities()
    {
        foreach(BaseAbility a in _abilities.Values)
        {
            RemoveAbility(a);
        }
    }
    #endregion Public Methods


    #region Photon RPCs
    [PunRPC]
    private void RPC_AddAbility(string abilityType) // This could and should be optimized once it is working
    {
        // Get ability type from string
        Type t = Type.GetType(abilityType);

        // Make sure the ability isn't already there
        if (!HasAbility(t))
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
    [PunRPC]
    private void RPC_RemoveAbility(string abilityType)
    {
        // Get ability type from string and create and ability
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
            Debug.LogError("Ability not owned. Unable to remove " + ability.GetName);
        }
    }
    #endregion Photon RPCs


    #region Private Methods
    // Register ability
    private void RegisterAbility(BaseAbility ability)
    {
        // Add ability to dictionary
        _abilities.Add(ReflectionUtil.GetTypeName(ability), ability);
        // Run function for when the ability is added
        ability.OnAbilityAdd();
    }

    // Unregister ability
    private void UnregisterAbility(BaseAbility ability)
    {
        // Run function for when the ability is added
        ability.OnAbilityRemove();
        // Remove ability from dictionary
        _abilities.Remove(ReflectionUtil.GetTypeName(ability));
    }
    #endregion Private Methods
}