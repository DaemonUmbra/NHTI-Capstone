using UnityEngine;
using System;
using System.Text;
using Photon;
using ExitGames.Client.Photon;

public abstract class Effect
{
    #region Private Variables

    public string Name = "NEW_EFFECT";

    //protected bool _stackable = false;
    public bool Unique = false;

    /// <summary>
    /// Time in seconds before debuff wears off.
    /// Lasts forever if zero or less
    /// </summary>
    [SerializeField]
    public float Lifetime = 0f; // 0 or negative to last forever

    private float _timeAdded;

    /// <summary>
    /// Increment of time before tick function is called.
    /// Ticks every frame if zero or less
    /// </summary>
    public float TickTime = 0f; //0 or negative to disable

    private float _lastTick;

    /// <summary>
    /// Is the effect attached to a game object.
    /// </summary>
    private bool isAttached = false;

    #endregion Private Variables
    

    #region Public Variables

    // The owner of the effect
    public GameObject Owner;

    #endregion Public Variables

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
        _lastTick = Time.time - TickTime;
        _timeAdded = Time.time;
    }

    /// <summary>
    /// Called every TickTime seconds. For effects like poison that do things after a certain amount of time.
    /// </summary>
    public virtual void OnTick()
    {
        //Debug.Log("Tick!");
        // Check if it has a lifetime
        if (Lifetime > 0 && isAttached)
        {
            // If the lifetime is up, remove debuff
            if (Time.time > _timeAdded + Lifetime)
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
        if (TickTime < 0)
        {
            OnTick();
            return;
        }

        // Calls OnTick when able
        if (Time.time > _lastTick + TickTime)
        {
            _lastTick = Time.time;
            OnTick();
        }
    }

    #endregion Overrides
}

/// <summary>
/// Used to package effects and send them over the network
/// </summary>
public class EffectPackage
{
    public Effect PackedEffect;
    public enum EFFECTS
    {
        BURN,
        SLOW,
        CONFUSE
    }
    public byte effectType;

    public byte[] effectInfo;
    public int sizeofEffect = 0;
    public EffectPackage(Effect effect)
    {
        PackedEffect = effect;
        Type type =  effect.GetType();

        if(type == typeof(BurnDamage))
        {
            effectType = (byte)EFFECTS.BURN;
            SerializeAsBurn((BurnDamage)effect);
        }
        else if(type == typeof(SlowMovement))
        {
            effectType = (byte)EFFECTS.SLOW;
            SerializeAsSlow((SlowMovement)effect);
        }
        else if(type == typeof(Confuse))
        {
            effectType = (byte)EFFECTS.CONFUSE;
            SerializeAsConfuse((Confuse)effect);
        }
    }
    public EffectPackage()
    {

    }

    public void Deserialize()
    {
        if(effectType == (byte)EFFECTS.BURN)
        {
            DeserializeBurn();
        }
        else if(effectType == (byte)EFFECTS.SLOW)
        {
            DeserializeSlow();
        }
        else if (effectType == (byte)EFFECTS.CONFUSE)
        {
            DeserializeConfuse();
        }
    }

    #region Internal Serialization Methods
    // Serialize
    private void SerializeAsBurn(BurnDamage effect)
    {
        int index = 0;
        // Allocate space for burn params
        int sizeofDamage = 4;   // One float for damage amount
        sizeofEffect = sizeofDamage;

        SerializeBaseEffect(effect, ref index);

        // Serialization
        Protocol.Serialize(effect.Damage, effectInfo, ref index);

    }
    private void SerializeAsSlow(SlowMovement effect)
    {
        int index = 0;
        // Allocate space for slow params
        int sizeofSlow = 4;     // One float for slow amount
        sizeofEffect = sizeofSlow;
        SerializeBaseEffect(effect, ref index);

        // Serialization
        Protocol.Serialize(effect.SlowAmount, effectInfo, ref index);
    }
    private void SerializeAsConfuse(Confuse effect)
    {
        int index = 0;
        sizeofEffect = 0;       // No additional info 
        SerializeBaseEffect(effect, ref index);

        // No extra serialization

    }
    private void SerializeBaseEffect(Effect effect, ref int index)
    {
        // Default Parameter sizes
        int sizeofUnique = 1;
        int sizeofName = Encoding.UTF8.GetByteCount(effect.Name);
        byte[] nameBytes = Encoding.UTF8.GetBytes(effect.Name); 
        int sizeofTicktime = 4;
        // Add default effect size
        sizeofEffect += sizeofUnique + sizeofName + sizeofTicktime + 4;
        // Initialize info array
        effectInfo = new byte[sizeofEffect];
        // Serialize base effect info
        // Name [string]
        Protocol.Serialize(sizeofName, effectInfo, ref index); // Serialize length info
        for(int i = 0; i < sizeofName; i++)
        {
            effectInfo[index] = nameBytes[i];
            index++;
        }
        // Unique [bool]
        effectInfo[index] = Convert.ToByte(effect.Unique);
        index++;
        // Ticktime [float]
        Protocol.Serialize(effect.TickTime, effectInfo, ref index);
    }
    // Deserialize
    private void DeserializeBurn()
    {
        PackedEffect = new BurnDamage();
        int index = 0;
        DeserializeBaseEffect(ref index);

        // Damage [float]
        int dmg;
        Protocol.Deserialize(out dmg, effectInfo, ref index);
        ((BurnDamage)PackedEffect).Damage = dmg;
    }
    private void DeserializeSlow()
    {
        PackedEffect = new SlowMovement();
        int index = 0;
        DeserializeBaseEffect(ref index);

        // SlowAmount [float]
        int slow;
        Protocol.Deserialize(out slow, effectInfo, ref index);
        ((SlowMovement)PackedEffect).SlowAmount = slow;
    }
    private void DeserializeConfuse()
    {
        PackedEffect = new Confuse();
        int index = 0;
        DeserializeBaseEffect(ref index);
    }
    private void DeserializeBaseEffect(ref int index)
    {
        int nameSize;
        // Name [string]
        Protocol.Deserialize(out nameSize, effectInfo, ref index);
        Encoding.UTF8.GetString(effectInfo, index, nameSize);
        index += nameSize;
        // Unique [bool]
        PackedEffect.Unique = Convert.ToBoolean(effectInfo[index]);
        index++;
        // Ticktime [float]
        Protocol.Deserialize(out PackedEffect.TickTime, effectInfo, ref index);

    }
    #endregion

    #region Photon Serialization
    private static short SerializeEffectPackage(StreamBuffer outStream, object customObject)
    {
        EffectPackage package = (EffectPackage)customObject;
        short sizeofPackage = (short)(1 + 4 + package.sizeofEffect);
        byte[] bytes = new byte[sizeofPackage];
        int index = 0;

        // Effect enum [int]
        bytes[index] = package.effectType;
        // Info size [int]
        Protocol.Serialize(package.sizeofEffect, bytes, ref index);
        // Effect info [byte array]
        for (int i = 0; i < package.sizeofEffect; i++)
        {
            bytes[index] = package.effectInfo[i];
        }
        outStream.Write(bytes, 0, sizeofPackage);

        return sizeofPackage;
    }

    private static object DeserializeEffectPackage(StreamBuffer inStream, short length)
    {
        EffectPackage package = new EffectPackage();
        byte[] buffer = new byte[length];

        inStream.Read(buffer, 0, length);
        int index = 0;
        // Effect type
        package.effectType = buffer[index];
        index++;
        // Effect byte array size
        Protocol.Deserialize(out package.sizeofEffect, buffer, ref index);
        // Effect info byte array
        package.effectInfo = new byte[package.sizeofEffect];
        Array.Copy(buffer, index, package.effectInfo, 0, package.sizeofEffect);
        package.Deserialize();

        return package;
    }
    #endregion
}