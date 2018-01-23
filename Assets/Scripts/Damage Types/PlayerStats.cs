using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    #region Class Variables
    /// <summary>
    /// Effects the player is currently under
    /// </summary>
    private List<Effect> _effects;
    private List<Effect> _expiredEffects;
    /// <summary>
    /// Effects applied to other players when attacking them
    /// </summary>
    private List<Effect> _onHitEffects;

    // Public Stats
    [SerializeField]
    public float WalkSpeed = 10f;
    [SerializeField]
    public float JumpPower = 10f;

    // Private stats (access variables below)
    [SerializeField]
    private float _maxHp = 100f;
    private float _currentHp;
    [SerializeField]
    private float _baseDmg = 10f;
    #endregion


    #region Access Variables
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

        _currentHp = _maxHp;
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
    /// <param name="amount">Amount of damage player will recieve</param>
    public void TakeDamage(float amount)
    {
        if(amount < 0)
        {
            Debug.LogWarning("Cannot take negative damage.");
            return;
        }
        // Reduce hp by amount
        _currentHp -= amount;
        if (_currentHp <= 0)
        {
            Debug.Log(gameObject.name + " hp <= 0");
            Die();
        }
    }
    /// <summary>
    /// Cause the player to take damage from a source
    /// </summary>
    /// <param name="source">Source damaging the player, can be null</param>
    /// <param name="amount">Amount of damage player will recieve</param>
    public void TakeDamage(GameObject source, float amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("Cannot take negative damage.");
            return;
        }

        // Reduce hp by amount
        _currentHp -= amount;
        if (_currentHp <= 0)
        {
            Debug.Log(gameObject.name + " hp <= 0");
            Die(source);
        }
    }
    /// <summary>
    /// Cause the player to take damage with effects
    /// </summary>
    /// <param name="amount">Amount of damage player will recieve</param>
    /// <param name="effects">Effects applied to the player, can be null</param>
    public void TakeDamage(float amount, List<Effect> effects)
    {
        if (effects != null)
        {
            // Add effects to player
            foreach (Effect e in effects)
            {
                e.ApplyEffect(gameObject);
            }
        }

        if (amount < 0)
        {
            Debug.LogWarning("Cannot take negative damage.");
            return;
        }

        // Reduce hp by amount
        _currentHp -= amount;
        if (_currentHp <= 0)
        {
            Debug.Log(gameObject.name + " hp <= 0");
            Die();
        }
    }
    /// <summary>
    /// Cause the player to take damage from a source with status effects
    /// </summary>
    /// <param name="source">Source damaging the player, can be null</param>
    /// <param name="amount">Amount of damage player will recieve</param>
    /// <param name="effects">Effects applied to the player, can be null</param>
    public void TakeDamage(GameObject source, float amount, List<Effect> effects)
    {
        if (effects != null)
        {
            // Add effects to player
            foreach (Effect e in effects)
            {
                e.ApplyEffect(gameObject);
            }
        }
        if(amount < 0)
        {
            Debug.LogWarning("Cannot take negative damage.");
            return;
        }
        // Reduce hp by amount
        _currentHp -= amount;
        if(_currentHp <= 0)
        {
            Debug.Log(gameObject.name + " hp <= 0");
            Die(source);
        }
    }
    
    // Increase the player's current hp by amount
    public void GainHp(float amount)
    {
        if(amount < 0)
        {
            Debug.LogWarning("Cannot gain negative hp. Use TakeDamage instead.");
            return;
        }
        if(_currentHp + amount > _maxHp)
        {
            Debug.LogWarning("Cannot overheal. Hp is max.");
            _currentHp = _maxHp;
        }
        else
        {
            _currentHp += amount;
        }
    }

    /// <summary>
    /// Inflict your base damage on another player.
    /// </summary>
    /// <param name="target">Player to take damage</param>
    public void DamagePlayer(GameObject target)
    {
        PlayerStats tStats = target.GetComponent<PlayerStats>();
        if(!tStats)
        {
            Debug.LogError("Cannot damage " + target.name + ". No playerstats script found.");
            return;
        }

        tStats.TakeDamage(gameObject, _baseDmg, _onHitEffects);
    }
    #endregion


    #region Private Methods
    private void Die()
    {
        Debug.Log(gameObject.name + " has died.");

        // No death logic yet
        _currentHp = _maxHp; // Resets hp
        Debug.LogWarning("Death logic not implemented yet. Player healed to full.");
    }
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
        _currentHp = _maxHp; // Resets hp
        Debug.LogWarning("Death logic not implemented yet. Player healed to full.");
    }
    #endregion

}
