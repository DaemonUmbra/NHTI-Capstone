using UnityEngine;

public class BurnDamage : Effect
{
    public float Damage;

    public BurnDamage()
    {
    }
    public BurnDamage(BurnDamage burnDamage)
    {
        Damage = burnDamage.Damage;
        Lifetime = burnDamage.Lifetime;
    }

    public BurnDamage(float burnDamage, float lifetime)
    {
        Damage = burnDamage;
        Lifetime = lifetime;
    }

    public override void Activate()
    {
        TickTime = 1f;
        base.Activate();
    }

    public override void OnTick()
    {
        PlayerStats ps = Owner.GetComponent<PlayerStats>();
        ps.TakeDamage(Damage);

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