﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Effect {

    #region Private Variables
    /// <summary>
    /// Time in seconds before debuff wears off.
    /// </summary>
    [SerializeField]
    protected float _lifetime = 0f; // 0 or negative to last forever
    private float _timeAdded;

    /// <summary>
    /// Increment of time before tick function is called
    /// </summary>
    [SerializeField]
    protected float _tickTime = 0f; //0 or negative to disable
    private float _lastTick;

    /// <summary>
    /// Is the effect attached to a game object
    /// </summary>
    private bool isAttached = false;

    
    #endregion

    #region Access Variables
    public float Lifetime { get { return _lifetime; } }
    public float TickTime { get { return _tickTime; } }
    #endregion

    #region Public Variables
    // The owner of the effect
    public GameObject Owner;

    public bool isUnique = false;
    public bool isStackable = false;
    #endregion

    #region Overrides
    /// <summary>
    /// Called to add an effect to a player.
    /// </summary>
    /// <param name="target">Player to recieve effect</param>
    public abstract void ApplyEffect(GameObject target);

    // Activate the effect so it starts ticking down
    public virtual void Activate()
    {
        Debug.Log("Activated");
        isAttached = true;
        _lastTick = Time.time - _tickTime;
        _timeAdded = Time.time;
    }

    /// <summary>
    /// Called every TickTime seconds. For effects like poison that do things after a certain amount of time.
    /// </summary>
    public virtual void OnTick()
    {
        //Debug.Log("Tick!");
        // Check if it has a lifetime
        if(_lifetime > 0 && isAttached)
        {
            // If the lifetime is up, remove debuff
            if(Time.time > _timeAdded + _lifetime)
            {
                RemoveEffect(); 
            }
        }
    }

    public virtual void RemoveEffect()
    {
        // Remove effect from owner collection
        PlayerStats ps = Owner.GetComponent<PlayerStats>();
        if (ps == null)
        {
            Debug.LogError("Playerstats not found. Unable to remove effect.");
            return;
        }
        ps.RemoveEffect(this);
        Owner = null;
        
    }
    
    /// <summary>
    /// Called every frame by the affected GameObject. 
    /// Do most logic in the OnTick function unless it really needs to be done every frame.
    /// </summary>
    public virtual void OnFrame()
    {
        // Checks for zero or negative tick time which means there is no tick event
        if (_tickTime < 0)
        {
            OnTick();
            return;
        }

        // Calls OnTick when able
        if (Time.time > _lastTick + _tickTime)
        {
            _lastTick = Time.time;
            OnTick();
        }
    }
    #endregion
    
    // To be implemented as an interface
    public void AddStack()
    {
        // Make sure it is a stackable effect
        if(!isStackable)
        {
            Debug.LogError("Unable to add a stack. The effect is not stackable.");
            return;
        }
        Debug.LogWarning("Stacking Not Implemented Yet");
        
    }
}