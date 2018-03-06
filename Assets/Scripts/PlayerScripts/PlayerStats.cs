using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Photon.MonoBehaviour
{

    #region Class Variables
    // Effects the player is currently under
    private List<Effect> _effects;

    private List<Effect> _expiredEffects;

    // Effects applied to other players when attacking them
    private List<Effect> _onHitEffects;

    // Public Stats
    [SerializeField]
    public float WalkSpeed = 10f;

    [SerializeField]
    public float JumpPower = 10f;

    // Private stats (access variables below)
    [SerializeField]
    private float _maxHp = 100f;
    private float _defaultMaxHp;

    private float _currentHp;

    [SerializeField]
    private float _baseDmg = 10f;

    // Damage modifiers
    public float dmgMult = 1f;

    public float dmgAdd = 0f;

   

    private PlayerSpawning pSpawn;
    #endregion Class Variables


    #region Access Variables
    public float MaxHp { get { return _maxHp; } }
    public float CurrentHp { get { return _currentHp; } }
    public float BaseDamage { get { return _baseDmg; } }
    public float EffectiveDamage { get { return _baseDmg * dmgMult + dmgAdd; } } // Calculate effective damage with dmg mods
    public List<Effect> OnHitEffects { get { return _onHitEffects; } }
    #endregion Access Variables


    #region Unity Callbacks
    // Use this for initialization
    private void Start()
    {
        _effects = new List<Effect>();
        _expiredEffects = new List<Effect>();
        _onHitEffects = new List<Effect>();

        _defaultMaxHp = _maxHp;
        _currentHp = _maxHp;
    }

    // Update is called once per frame
    private void Update()
    {
        // Clean up the expired effects
        for (int i = 0; i < _expiredEffects.Count; ++i)
        {
            _effects.Remove(_expiredEffects[i]);
        }
        _expiredEffects.Clear();

        // Triggers each effect
        foreach (Effect e in _effects)
        {
            e.OnFrame();
        }

        // TEST
        if (Input.GetKeyDown("q"))
        {
            TakeDamage(10f);
        }
    }
    #endregion Unity Callbacks


    #region Public Methods
    // Add an effect to the player
    public void AddEffect(Effect effect)
    {
        if (effect.Unique == true)
        {
            bool found = false;
            foreach (Effect e in _effects)
            {
                if (e.Name == effect.Name && effect.Name != "")
                {
                    Debug.LogError("Cannot add effect. Unique effect already exists on player");
                    found = true;
                }
            }
            if (!found)
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
        if (effect.Unique == true)
        {
            bool found = false;
            foreach (Effect e in _onHitEffects)
            {
                if (e.GetType() == effect.GetType())
                {
                    Debug.LogError("Cannot add effect. Unique effect already exists on player");
                    found = true;
                }
            }
            if (!found)
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
        print("In take damage function. Damage to be taken: " + amount);
        photonView.RPC("RPC_TakeDamage", PhotonTargets.All, amount);
    }

    /// <summary>
    /// Cause the player to take damage from a source
    /// </summary>
    /// <param name="source">Source damaging the player, can be null</param>
    /// <param name="amount">Amount of damage player will recieve</param>
    public void TakeDamage(float amount, GameObject source)
    {
        print("In take damage function. Damage to be taken: " + amount);
        photonView.RPC("RPC_TakeDamage", PhotonTargets.All, amount, source.GetPhotonView().viewID);
    }

    /// <summary>
    /// Cause the player to take damage with effects
    /// </summary>
    /// <param name="amount">Amount of damage player will recieve</param>
    /// <param name="effects">Effects applied to the player, can be null</param>
    public void TakeDamage(float amount, List<Effect> effects)
    {
        print("In take damage function. Damage to be taken: " + amount);
        photonView.RPC("RPC_TakeDamage", PhotonTargets.All, amount);
    }

    /// <summary>
    /// Cause the player to take damage from a source with status effects
    /// </summary>
    /// <param name="source">Source damaging the player, can be null</param>
    /// <param name="amount">Amount of damage player will recieve</param>
    /// <param name="effects">Effects applied to the player, can be null</param>
    public void TakeDamage(float amount, GameObject source, List<Effect> effects)
    {
        print("In take damage function. Damage to be taken: " + amount);
        photonView.RPC("RPC_TakeDamage", PhotonTargets.All, amount, source.GetPhotonView().viewID);
    }

    // Increase the player's current hp by amount
    public void GainHp(float amount)
    {
        photonView.RPC("RPC_GainHp", PhotonTargets.All, amount);
    }
    /// <summary>
    /// Change the player's max hp
    /// </summary>
    /// <param name="amount">Amount to add, negative to reduce hp</param>
    public void ChangeMaxHp(float amount)
    {
        photonView.RPC("RPC_ChangeMaxHp", PhotonTargets.All, amount);
    }
    // Can be used later for checking accuracy etc
    public void ReportHit(GameObject hit)
    {
        Debug.Log(hit.name + " was hit by " + gameObject.name);
    }
    #endregion Public Methods


    #region Photon RPCs
    [PunRPC]
    private void RPC_TakeDamage(float amount)
    {
        // Validate the damage amount
        if (amount < 0)
        {
            Debug.LogWarning("Cannot take negative damage.");
            return;
        }
        // Reduce hp by amount
        _currentHp -= amount;
        // Kill if hp is zero or less
        if (_currentHp <= 0)
        {
            Debug.Log(gameObject.name + " hp <= 0");
            Die();
        }
        Debug.Log("Player hp: " + CurrentHp);
    }

    [PunRPC]
    private void RPC_TakeDamage(float amount, int srcViewId)
    {
        // Find the source gameobject from it's view id
        PhotonView srcView = PhotonView.Find(srcViewId);
        GameObject srcObj = null;
        if (srcView != null)
            srcObj = srcView.gameObject;

        // Validate the damage amount
        if (amount < 0)
        {
            Debug.LogWarning("Cannot take negative damage.");
            return;
        }
        // Reduce hp by amount
        _currentHp -= amount;
        // Kill if hp is zero or less
        if (_currentHp <= 0)
        {
            Debug.Log(gameObject.name + " hp <= 0");
            if (srcObj)
                Die(srcObj);
            else
                Die();
        }
        Debug.Log("Player hp: " + CurrentHp);
    }

    /* Needs rework because of PUN serialization
    [PunRPC]
    private void RPC_TakeDamage(float amount, GameObject source, List<Effect> effects)
    {
        PlayerStats pSource = source.GetComponent<PlayerStats>();
        pSource.ReportHit(gameObject);

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
            Die(source);
        }
    }
    */

    [PunRPC]
    private void RPC_GainHp(float amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("Cannot gain negative hp. Use TakeDamage instead.");
            return;
        }
        if (_currentHp + amount > _maxHp)
        {
            Debug.LogWarning("Cannot overheal. Hp is max.");
            _currentHp = _maxHp;
        }
        else
        {
            _currentHp += amount;
        }
    }
    [PunRPC]
    private void RPC_ModifyMaxHp(float amount)
    {
        _maxHp += amount;
        // Check that the max hp isn't less than the current hp
        if (CurrentHp > _maxHp)
        {
            _currentHp = _maxHp;
        }
    }
    [PunRPC]
    private void RPC_Die()
    {
        Debug.Log(gameObject.name + " has died.");

        // No death logic yet
        GameObject gameMng = FindObjectOfType<PlayerSpawning>().gameObject;
        var Mng = gameMng.GetComponent<PlayerSpawning>();
        Transform respawn = Mng.GetRandomSpawnPoint();

        gameObject.transform.position = respawn.position;
        _currentHp = _maxHp; // Resets hp
        Debug.LogWarning("Death logic not implemented yet. Player healed to full.");
    }

    [PunRPC]
    private void RPC_Die(int srcId)
    {
        GameObject killer = PhotonView.Find(srcId).gameObject;

        if (killer != null)
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
    #endregion Photon RPCs


    #region Private Methods
    private void Die()
    {
        photonView.RPC("RPC_Die", PhotonTargets.All);
    }

    private void Die(GameObject killer)
    {
        photonView.RPC("RPC_Die", PhotonTargets.All, killer.GetPhotonView().viewID);
    }
    #endregion Private Methods
}