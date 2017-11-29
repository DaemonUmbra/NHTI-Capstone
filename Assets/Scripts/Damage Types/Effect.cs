using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Effect : MonoBehaviour {

    #region Private Variables

    /// <summary>
    /// Time in seconds before debuff wears off.
    /// </summary>
    [SerializeField]
    protected float Lifetime = 0f; // 0 or negative to last forever
    private float TimeAdded;

    /// <summary>
    /// Increment of 
    /// </summary>
    [SerializeField]
    protected float TickTime = 1f; //-1 to disable
    private float LastTick;

    #endregion


    #region Overrides

    // Called when debuffs are added to a player
    public virtual void ApplyEffect(GameObject target)
    {
        LastTick = Time.time - TickTime;
        TimeAdded = Time.time;
    }

    // Called every TickTime seconds
    public virtual void OnTick()
    {
        // Check if it has a lifetime
        if(Lifetime > 0)
        {
            // If the lifetime is up, remove debuff
            if(Time.time > TimeAdded + Lifetime)
            {
                RemoveDebuff();
            }
        }
    }

    public abstract void RemoveDebuff();

    #endregion


    #region Unity Callbacks

    public void Update()
    {
        // Checks for negative tick time which means there is no tick event
        if (TickTime < 0)
            return;

        // Calls OnTick when able
        if (Time.time > LastTick + TickTime)
        {
            LastTick = Time.time;
            OnTick();
        }
    }

    #endregion
}
