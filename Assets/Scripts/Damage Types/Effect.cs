using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Effect : MonoBehaviour {

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
    protected float _tickTime = 1f; //-1 to disable
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


    #region Overrides
    // Called when debuffs are added to a player
    public abstract void ApplyEffect(GameObject target);

    // Activate the effect so it starts ticking down
    public virtual void Activate()
    {
        isAttached = true;
        _lastTick = Time.time - _tickTime;
        _timeAdded = Time.time;
    }

    // Called every TickTime seconds
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

    public abstract void RemoveEffect();
    #endregion


    #region Unity Callbacks
    public void Update()
    {
        // Checks for negative tick time which means there is no tick event
        if (_tickTime < 0)
            return;

        // Calls OnTick when able
        if (Time.time > _lastTick + _tickTime)
        {
            _lastTick = Time.time;
            OnTick();
        }
    }
    #endregion
}
