using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Photon.MonoBehaviour
{
    #region Class Variables
    // Effects the player is currently under
    private List<Effect> _effects;

    private List<Effect> _expiredEffects;
    
    private List<Effect> _onHitEffects;


    // **** Stats **** //
    // Speed
    [SerializeField]
    private float _baseWalkSpeed = 10f;
    List<KeyValuePair<string, float>> _speedMultipliers;
    List<KeyValuePair<string, float>> _speedBoosts;
    float _walkSpeed;

    // Jump Power
    [SerializeField]
    public float JumpPower = 20000f;

    // Health
    [SerializeField]
    private float _baseMaxHp = 100f;
    List<KeyValuePair<string, float>> _healthBoosts;
    private float _maxHp;
    private float _currentHp;

    // Damage
    [SerializeField]
    private float _baseDmg = 10f;
    List<KeyValuePair<string, float>> _dmgMultipliers;
    List<KeyValuePair<string, float>> _dmgBoosts;
    float _damage;

    // Scale
    Vector3 _scaleMod;
    Vector3 _baseScale;
    List<KeyValuePair<string, Vector3>> _scaleFactors;
    /// <summary>
    /// Transforms to be scaled with the player. For now it just includes the local transform which is added at runtime.
    /// </summary>
    List<Transform> _transformsToScale;

    private PlayerSpawning pSpawn;
    #endregion Class Variables


    #region Access Variables
    public float MaxHp { get { return _maxHp; } }
    public float CurrentHp { get { return _currentHp; } }
    public float BaseDamage { get { return _baseDmg; } }
    public float Damage { get { return _damage; } } // Calculate effective damage with dmg mods
    public List<Effect> OnHitEffects { get { return _onHitEffects; } }
    //public List<EffectPackage> PackagedEffects { get { return _packagedEffects; } }
    #endregion Access Variables


    #region Unity Callbacks

    // Use this for initialization
    private void Awake()
    {
        _effects = new List<Effect>();
        _expiredEffects = new List<Effect>();
        _onHitEffects = new List<Effect>();
        _scaleFactors = new List<KeyValuePair<string, Vector3>>();
        if(_transformsToScale == null) _transformsToScale = new List<Transform>();
        _dmgBoosts = new List<KeyValuePair<string, float>>();
        _dmgMultipliers = new List<KeyValuePair<string, float>>();

        _baseMaxHp = _maxHp;
        _currentHp = _maxHp;
        _baseScale = transform.localScale;
        _scaleMod = new Vector3(1f, 1f, 1f);

        _transformsToScale.Add(transform);
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
                effect.Owner = gameObject;
                effect.Activate();
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
                EffectPackage ePack = new EffectPackage(effect);
                //_packagedEffects.Add(ePack);
                //_packageLinker[effect] = ePack;
                // Do NOT activate the effect or set an owner
            }
        }
        else
        {
            _onHitEffects.Add(effect);
            EffectPackage ePack = new EffectPackage(effect);
            //_packagedEffects.Add(ePack);
            //_packageLinker[effect] = ePack;
            // Do NOT activate the effect or set an owner
        }
    }
    // Remove an OnHit effect from the player
    public void RemoveOnHit(Effect effect)
    {
        // Remove onhit effect from all lists
        _onHitEffects.Remove(effect);
        //_packagedEffects.Remove(_packageLinker[effect]);
        //_packageLinker.Remove(effect);
    }

    // Public Damage methods
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
        List<EffectPackage> packagedEffects = new List<EffectPackage>();
        foreach(Effect e in effects)
        {
            packagedEffects.Add(new EffectPackage(e));
        }
        photonView.RPC("RPC_TakeDamage", PhotonTargets.All, amount, packagedEffects);
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
        List<EffectPackage> packagedEffects = new List<EffectPackage>();
        foreach (Effect e in effects)
        {
            packagedEffects.Add(new EffectPackage(e));
        }
        photonView.RPC("RPC_TakeDamage", PhotonTargets.All, amount, source.GetPhotonView().viewID, packagedEffects);
    }

    // Increase the player's current hp
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

    // Public Damage Modifiers
    /// <summary>
    /// Add a damage multiplier to the player. Save the multiplier name as a reference for when you remove it.
    /// </summary>
    /// <param name="dmgMultiplier">Damage multiplier</param>
    /// <param name="multiplierName">Name of multiplier for reference. Used to remove the multiplier later on.</param>
    public void AddDmgMultiplier(string multiplierName, float dmgMultiplier)
    {
        photonView.RPC("RPC_AddDamageMutiplier", PhotonTargets.All, multiplierName, dmgMultiplier);
    }
    /// <summary>
    /// Add a flat boost to the player. Save the multiplier name as a reference for when you remove it.
    /// </summary>
    /// <param name="dmgBoost">Boost amount</param>
    /// <param name="boostName">Name of boost for reference. Used to remove the boost later on.</param>
    public void AddDmgBoost(string boostName, float dmgBoost)
    {
        photonView.RPC("RPC_AddDamageBoost", PhotonTargets.All, boostName, dmgBoost);
        //_dmgFlatBuffs.Add(new KeyValuePair<string, float>(sourceName, dmgBoost));
    }
    /// <summary>
    /// Remove damage multiplier from player. Use the name set when adding the multiplier.
    /// </summary>
    /// <param name="multiplierName">Name of the multiplier</param>
    public void RemoveDmgMultiplier(string multiplierName)
    {
        photonView.RPC("RPC_RemoveDmgMultiplier", PhotonTargets.All, multiplierName);
    }
    /// <summary>
    /// Remove a damage boost from the player. Use the name set when adding the boost.
    /// </summary>
    /// <param name="boostName">Name of the boost</param>
    public void RemoveDmgBoost(string boostName)
    {
        photonView.RPC("RPC_RemoveDmgBoost", PhotonTargets.All, boostName);
    }

    // Public Scale Modifiers
    public void AddScaleFactor(string factorName, Vector3 factor)
    {
        photonView.RPC("RPC_AddScaleFactor", PhotonTargets.All, factorName, factor);
    }
    public void AddScaleFactor(string factorName, float factor)
    {
        AddScaleFactor(factorName, new Vector3(factor, factor, factor));
    }
    public void RemoveScaleFactor(string factorName)
    {
        photonView.RPC("RPC_RemoveScaleFactor", PhotonTargets.All);
    }
    
    // Public Speed Modifiers
    public void AddSpeedMultipler(string multName, float multiplier)
    {
        photonView.RPC("RPC_AddSpeedMultiplier", PhotonTargets.All, multName, multiplier);
    }
    public void AddSpeedBoost(string boostName, float boost)
    {
        photonView.RPC("RPC_AddSpeedBoost", PhotonTargets.All, boostName, boost);
    }
    public void RemoveSpeedMultiplier(string multName)
    {
        photonView.RPC("RPC_RemoveSpeedMultiplier", PhotonTargets.All, multName);
    }
    public void RemoveSpeedBoost(string boostName)
    {
        photonView.RPC("RPC_RemoveSpeedBoost", PhotonTargets.All, boostName);
    }

    // Public Max Health Modifiers
    public void AddHealthBoost(string boostName, float boost)
    {
        photonView.RPC("RPC_AddHealthBoost", PhotonTargets.All, boostName, boost);
    }
    public void RemoveHealthBoost(string boostName)
    {
        photonView.RPC("RPC_RemoveHealthBoost", PhotonTargets.All, boostName);
    }

    // Can be used later for checking accuracy etc
    public void ReportHit(GameObject hit)
    {
        Debug.Log(hit.name + " was hit by " + gameObject.name);
    }

    #endregion Public Methods


    #region Photon RPCs

    // Damage RPCs
    [PunRPC] private void RPC_TakeDamage(float amount)
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
    [PunRPC] private void RPC_TakeDamage(float amount, int srcViewId)
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
    [PunRPC] private void RPC_TakeDamage(float amount, List<EffectPackage> effects)
    {
        // Apply effects
        if (effects != null)
        {
            // Unpack and add effects to player
            foreach (EffectPackage ePack in effects)
            {
                Effect eff = ePack.PackedEffect;
                eff.ApplyEffect(gameObject);
            }
        }
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
    [PunRPC] private void RPC_TakeDamage(float amount, int srcViewId, List<EffectPackage> effects)
    {
        // Find the source gameobject from it's view id
        PhotonView srcView = PhotonView.Find(srcViewId);
        GameObject srcObj = null;
        if (srcView != null)
            srcObj = srcView.gameObject;

        // Apply effects
        if (effects != null)
        {
            // Unpack and add effects to player
            foreach (EffectPackage ePack in effects)
            {
                Effect eff = ePack.PackedEffect;
                eff.ApplyEffect(gameObject);
            }
        }
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

    // Health RPCs
    [PunRPC] private void RPC_GainHp(float amount)
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
    [PunRPC] private void RPC_ModifyMaxHp(float amount)
    {
        _maxHp += amount;
        // Check that the max hp isn't less than the current hp
        if (CurrentHp > _maxHp)
        {
            _currentHp = _maxHp;
        }
    }

    // Death RPCs
    [PunRPC] private void RPC_Die()
    {
        Debug.Log(gameObject.name + " has died.");

        // Respawn Player
        GameObject gameMng = FindObjectOfType<PlayerSpawning>().gameObject;
        var Mng = gameMng.GetComponent<PlayerSpawning>();
        Transform respawn = Mng.GetRandomSpawnPoint();
        gameObject.transform.position = respawn.position;
        // Reset abilities
        AbilityManager abilityManager = GetComponent<AbilityManager>();
        abilityManager.ResetAbilities();
        _currentHp = _maxHp; // Resets hp
    }
    [PunRPC] private void RPC_Die(int srcId)
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

        // Respawn Player
        PlayerSpawning Mng = FindObjectOfType<PlayerSpawning>();
        Transform respawn = Mng.GetRandomSpawnPoint();
        gameObject.transform.position = respawn.position;
        // Reset abilities
        AbilityManager abilityManager = GetComponent<AbilityManager>();
        abilityManager.ResetAbilities();
        _currentHp = _maxHp; // Resets hp
    }

    // Damage Modifier RPCs
    [PunRPC] private void RPC_AddDamageMultiplier(string multName, float multiplier)
    {
        _dmgMultipliers.Add(new KeyValuePair<string, float>(multName, multiplier));
        CalcNewDamage();
    }
    [PunRPC] private void RPC_AddDamageBoost(string boostName, float boost)
    {
        _dmgBoosts.Add(new KeyValuePair<string, float>(boostName, boost));
        CalcNewDamage();
    }
    [PunRPC] private void RPC_RemoveDamageMultiplier(string multName)
    {
        foreach(KeyValuePair<string, float> multPair in _dmgMultipliers)
        {
            if(multPair.Key == multName)
            {
                _dmgMultipliers.Remove(multPair);
                break;
            }
        }
        CalcNewDamage();
    }
    [PunRPC] private void RPC_RemoveDamageBoost(string boostName)
    {
        foreach(KeyValuePair<string, float> boostPair in _dmgBoosts)
        {
            if(boostPair.Key == boostName)
            {
                _dmgBoosts.Remove(boostPair);
                break;
            }
        }
        CalcNewDamage();
    }

    // Scale RPCs
    [PunRPC] private void RPC_AddScaleFactor(string factorName, Vector3 factor)
    {
        // Add scale factor
        _scaleFactors.Add(new KeyValuePair<string, Vector3>(factorName, factor));
        CalcNewScale();
        // Calculate the new scale on the client only since transforms are synced
        if (photonView.isMine)
        {
            ApplyNewScale();
        }
    }
    [PunRPC] private void RPC_RemoveScaleFactor(string factorName)
    {
        foreach(KeyValuePair<string, Vector3> factorPair in _scaleFactors)
        {
            if(factorPair.Key == factorName)
            {
                _scaleFactors.Remove(factorPair);
                break;
            }
        }
        CalcNewScale();
        // Calculate the new scale on the client only since transforms are synced
        if(photonView.isMine)
        {
            ApplyNewScale();
        }
    }

    // Speed Modifier RPCs
    [PunRPC] private void RPC_AddSpeedMultiplier(string multName, float multiplier)
    {
        _speedMultipliers.Add(new KeyValuePair<string, float>(multName, multiplier));
        CalcNewSpeed();
    }
    [PunRPC] private void RPC_AddSpeedBoost(string boostName, float boost)
    {
        _speedBoosts.Add(new KeyValuePair<string, float>(boostName, boost));
        CalcNewSpeed();
    }
    [PunRPC] private void RPC_RemoveSpeedMultiplier(string multName)
    {
        foreach (KeyValuePair<string, float> multPair in _speedMultipliers)
        {
            if (multPair.Key == multName)
            {
                _speedMultipliers.Remove(multPair);
                break;
            }
        }
        CalcNewSpeed();
    }
    [PunRPC] private void RPC_RemoveSpeedBoost(string boostName)
    {
        foreach (KeyValuePair<string, float> boostPair in _speedBoosts)
        {
            if (boostPair.Key == boostName)
            {
                _speedBoosts.Remove(boostPair);
                break;
            }
        }
        CalcNewSpeed();
    }

    // Max Health RPCs
    [PunRPC] private void RPC_AddHealthBoost(string boostName, float boost)
    {
        _healthBoosts.Add(new KeyValuePair<string, float>(boostName, boost));
        CalcNewHealth();
    }
    [PunRPC] private void RPC_RemoveHealthBoost(string boostName)
    {
        foreach(KeyValuePair<string, float> boostPair in _healthBoosts)
        {
            if(boostPair.Key == boostName)
            {
                _speedBoosts.Remove(boostPair);
                break;
            }
        }
        CalcNewHealth();
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
    private void ApplyNewScale()
    {
        // Scale each transform individually
        foreach (Transform t in _transformsToScale)
        {
            t.localScale = new Vector3(_baseScale.x * _scaleMod.x, _baseScale.y * _scaleMod.y, _baseScale.z * _scaleMod.z);
        }
    }
    private void CalcNewScale()
    {
        // Recalculate scale modifier
        _scaleMod = new Vector3(1f, 1f, 1f);
        foreach (KeyValuePair<string, Vector3> f in _scaleFactors)
        {
            _scaleMod.x *= f.Value.x;
            _scaleMod.y *= f.Value.y;
            _scaleMod.z *= f.Value.z;
        }
    }
    private void CalcNewDamage()
    {
        // Set damage to base
        _damage = _baseDmg;
        
        // Multiply by all multipliers
        foreach(KeyValuePair<string, float> multPair in _dmgMultipliers)
        {
            _damage *= multPair.Value;
        }
        
        // Add all boosts
        foreach(KeyValuePair<string, float> boostPair in _dmgBoosts)
        {
            _damage += boostPair.Value;
        }
    }
    private void CalcNewSpeed()
    {
        // Reset walk speed
        _walkSpeed = _baseWalkSpeed;
        // Multiply speed by all factors
        foreach(KeyValuePair<string, float> multPair in _speedMultipliers)
        {
            _walkSpeed *= multPair.Value;
        }
        // Increase speed by all boosts
        foreach(KeyValuePair<string, float> boostPair in _speedBoosts)
        {
            _walkSpeed += boostPair.Value;
        }
    }
    private void CalcNewHealth()
    {
        float newMaxHp = _baseMaxHp;
        foreach(KeyValuePair<string, float> boostPair in _healthBoosts)
        {
            newMaxHp += boostPair.Value;
        }
        _maxHp = newMaxHp;

        // Make sure health isn't over the max
        if(_currentHp > _maxHp)
        {
            _currentHp = _maxHp;
        }
    }

    #endregion Private Methods
}