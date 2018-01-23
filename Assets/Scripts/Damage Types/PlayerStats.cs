using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    /// <summary>
    /// List of effects the player is under
    /// </summary>
    private List<Effect> _effects;
    private List<Effect> _expiredEffects;

    private float _currentHp;
    private float _maxHp;
    private float _baseDmg;

    /// <summary>
    /// Effects applied to other player when attacking them
    /// </summary>
    private List<Effect> _onHitEffects;

	// Use this for initialization
	void Start () {
        _effects = new List<Effect>();
        _expiredEffects = new List<Effect>();
        _onHitEffects = new List<Effect>();
	}
	
	// Update is called once per frame
	void Update () {

        // Triggers each effect
		foreach(Effect e in _effects)
        {
            e.OnFrame();
        }
        // Clean up the expired effects
        foreach (Effect e in _expiredEffects)
        {
            _effects.Remove(e);
        }
        _expiredEffects.Clear();
	}

    // Add an effect to the player
    public void AddEffect(Effect effect)
    {
        if(effect.isUnique == true)
        {
            foreach(Effect e in _effects)
            {
                if (e.GetType() == effect.GetType())
                {
                    Debug.LogError("Cannot add effect. Unique effect already exists on player");
                }
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

    /// <summary>
    /// Cause the player to take damage
    /// </summary>
    /// <param name="source">Source damaging the player</param>
    /// <param name="amount">Amount of damage player will recieve</param>
    /// <param name="effects">Effects applied to the player</param>
    public void TakeDamage(GameObject source, float amount, List<Effect> effects)
    {
        // Add effects to player
        foreach(Effect e in effects)
        {
            AddEffect(e);
        }

        // Reduce hp by amount
        _currentHp -= amount;
        if(_currentHp <= 0)
        {
            Debug.Log(gameObject.name + " hp <= 0");
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " Has Died!");
        // No death logic yet
    }
    
}
