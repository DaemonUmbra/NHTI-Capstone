using UnityEngine;

public class BurnDamage : Effect
{
    private float _burnDamage;
    public float Damage { get { return _burnDamage; } }

    public BurnDamage(BurnDamage burnDamage)
    {
        _burnDamage = burnDamage.Damage;
        _lifetime = burnDamage.Lifetime;
    }

    public BurnDamage(float burnDamage, float lifetime)
    {
        _burnDamage = burnDamage;
        _lifetime = lifetime;
    }

    public override void Activate()
    {
        _tickTime = 1f;
        base.Activate();
    }

    public override void OnTick()
    {
        PlayerStats ps = Owner.GetComponent<PlayerStats>();
        ps.TakeDamage(_burnDamage);

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