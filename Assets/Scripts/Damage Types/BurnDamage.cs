using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDamage : Effect
{
    private float _burnDamage;
    public float Damage {get { return _burnDamage; } }
    //private BurnDamage burnDamage;

    public BurnDamage(BurnDamage burnDamage)
    {
        _burnDamage = burnDamage.Damage;
        _tickTime = burnDamage.TickTime;
    }

    public BurnDamage(float burnDamage, float lifetime)
    {
        _burnDamage = burnDamage;
        _lifetime = lifetime;
    }

    public override void OnTick()
    {
        PlayerStats ps = Owner.GetComponent<PlayerStats>();
        ps.TakeDamage(null, _burnDamage, null);

        base.OnTick();
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
    }

    public override void ApplyEffect(GameObject target)
    {
        // Ref the player stat class
        PlayerStats ps = target.GetComponent<PlayerStats>();
        if (!ps)
        {
            Debug.LogError("No Player Stats class in target: " + target);
            return;
        }

        // Copy this effect to the target
        BurnDamage burn = new BurnDamage(this);
        ps.AddEffect(burn);
    }
}
