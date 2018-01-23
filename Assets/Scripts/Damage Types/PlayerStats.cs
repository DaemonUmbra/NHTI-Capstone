using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    #region Private Variables
    /// <summary>
    /// List of effects the player is under
    /// </summary>
    private List<Effect> _effects;
    private List<Effect> _expiredEffects;

    [SerializeField]
    private float _walkSpeed;
    [SerializeField]
    private float _maxHp;
    private float _currentHp;
    [SerializeField]
    private float _baseDmg;
    
    /// <summary>
    /// Effects applied to other player when attacking them
    /// </summary>
    private List<Effect> _onHitEffects;
    #endregion

    #region Access Variables
    public float WalkSpeed { get { return _walkSpeed; } }
    public float MaxHp { get { return _maxHp; } }
    public float CurrentHp { get { return _currentHp; } }
    public float BaseDamage { get { return _baseDmg; } }
    #endregion

    #region Unity Callbacks
    // Use this for initialization
    void Start () {
        _effects = new List<Effect>();
        _expiredEffects = new List<Effect>();
        _onHitEffects = new List<Effect>();
	}

    // Update is called once per frame
    void Update() {

        // Triggers each effect
        foreach (Effect e in _effects)
        {
            e.OnFrame();
        }
        // Clean up the expired effects
        foreach (Effect e in _expiredEffects)
        {
            _effects.Remove(e);
        }
        _expiredEffects.Clear();


        // TEST
        if (Input.GetKeyDown("0"))
        {
            TakeDamage(null, 10f, null);
        }
	}
    #endregion


    #region Public Methods
    // Add an effect to the player
    public void AddEffect(Effect effect)
    {
        if(effect.isUnique == true)
        {
            bool found = false;
            foreach(Effect e in _effects)
            {
                if (e.GetType() == effect.GetType())
                {
                    Debug.LogError("Cannot add effect. Unique effect already exists on player");
                    found = true;
                }
            }
            if(!found)
            {
                _effects.Add(effect);
            }
        }
        else
        {
            _effects.Add(effect);
            effect.Owner = gameObject;
            effect.Activate();
        }
    }
    // Called by effects when they expire
    public void RemoveEffect(Effect effect)
    {
        _expiredEffects.Add(effect);
    }

    // Add an OnHit effect to the player
    public void AddOnHit(Effect effect)
    {
        if(effect.isUnique == true)
        {
            bool found = false;
            foreach(Effect e in _onHitEffects)
            {
                if(e.GetType() == effect.GetType())
                {
                    Debug.LogError("Cannot add effect. Unique effect already exists on player");
                    found = true;
                }
            }
            if(!found)
            {
                _onHitEffects.Add(effect);
            }
        }
        else
        {
            _onHitEffects.Add(effect);
            // Do NOT activate the effect or set an owner
        }
    }
    // Remove an OnHit effect from the player
    public void RemoveOnHit(Effect effect)
    {
        _onHitEffects.Remove(effect);
    }

    /// <summary>
    /// Cause the player to take damage
    /// </summary>
    /// <param name="source">Source damaging the player</param>
    /// <param name="amount">Amount of damage player will recieve</param>
    /// <param name="effects">Effects applied to the player</param>
    public void TakeDamage(GameObject source, float amount, List<Effect> effects)
    {
        if (effects != null)
        {
            // Add effects to player
            foreach (Effect e in effects)
            {
                AddEffect(e);
            }
        }

        // Reduce hp by amount
        _currentHp -= amount;
        if(_currentHp <= 0)
        {
            Debug.Log(gameObject.name + " hp <= 0");
            Die(source);
        }
    }
    #endregion


    #region Private Methods
    private void Die(GameObject killer)
    {
        if(killer != null)
        {
            Debug.Log(gameObject.name + " was killed by " + killer.name);
        }
        else
        {
            Debug.Log(gameObject.name + " has died of mysterious causes.");
        }
        
        // No death logic yet
    }
    #endregion

}
