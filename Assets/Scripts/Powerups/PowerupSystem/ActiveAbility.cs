using UnityEngine;

public abstract class ActiveAbility : BaseAbility
{
    /// <summary>
    /// BaseAbility cooldown in seconds
    /// </summary>
    [SerializeField]
    protected float Cooldown = 0f;

    private float lastActivated = 0f;

    public override void OnAbilityAdd()
    {
        lastActivated = Time.time - Cooldown;
        base.OnAbilityAdd();
    }

    public void TryActivate()
    {
        if (lastActivated + Cooldown <= Time.time)
        {
            Activate();
        }
    }

    /// <summary>
    /// Activate the ability, called by RPC_Activate on server
    /// </summary>
    protected virtual void Activate() // Check cooldown before activating
    {
        lastActivated = Time.time;
    }
}