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

    public void Activate()
    {
        if (lastActivated + Cooldown <= Time.time)
        {
            photonView.RPC("RPC_Activate", PhotonTargets.All);
        }
    }

    /// <summary>
    /// Activate the ability
    /// </summary>
    [PunRPC]
    protected virtual void RPC_Activate() // Check cooldown before activating
    {
        lastActivated = Time.time;
    }
}