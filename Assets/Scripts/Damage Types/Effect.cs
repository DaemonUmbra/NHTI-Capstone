using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Effect {

    #region Private Variables
    protected string _name = "";
    //protected bool _stackable = false;
    protected bool _unique = false;

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
    /// Is the effect attached to a game object.
    /// </summary>
    private bool isAttached = false;
    #endregion

    #region Access Variables
    public float Lifetime { get { return _lifetime; } }
    public float TickTime { get { return _tickTime; } }
    public string Name { get { return _name; } }
    public bool Unique { get { return _unique; } }
    //public bool Stackable { get { return _stackable; } }
    #endregion

    #region Public Variables
    // The owner of the effect
    public GameObject Owner;

    
    #endregion

    #region Overrides
    /// <summary>
    /// Called to add an effect to a player. See "Slow Movement" for an example.
    /// </summary>
    /// <param name="target">Player to recieve effect</param>
    public abstract void ApplyEffect(GameObject target);

    /// <summary>
    /// Activate the ability. Put any "start" code in this override as well.
    /// </summary>
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
    /// <summary>
    /// Remove effect from player completely
    /// </summary>
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
    /// Do "update" logic in the OnTick function override
    /// </summary>
    public void OnFrame()
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
    
    
}
