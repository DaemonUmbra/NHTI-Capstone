using System;
using UnityEngine;

/// <summary>
/// DOES NOT WORK
/// </summary>
public class PowerupDebugger : MonoBehaviour
{
    [HideInInspector]
    public AbilityManager Player = null;

    [HideInInspector]
    public Type SelectedPowerup;

    [HideInInspector]
    public BaseAbility SelectedPlayerPowerup;

    private PhotonView pv;

    // Use this for initialization
    private void Start()
    {
        pv = PhotonView.Get(this);
    }

    [PunRPC]
    private void Debugger_Remote_AddAbility(BaseAbility ability)
    {
        if (Player)
        {
            if (!Player.HasAbility(ability))
            {
                Player.AddAbility(ability);
            }
        }
    }

    [PunRPC]
    private void Debugger_Remote_RemoveAbility(BaseAbility ability)
    {
        if (Player)
        {
            if (Player.HasAbility(ability))
            {
                Player.RemoveAbility(ability);
            }
        }
    }

    public void AddAbility(BaseAbility ability)
    {
        pv.RPC("Debugger_Remote_AddAbility", PhotonTargets.All, ability);
    }

    public void RemoveAbility(BaseAbility ability)
    {
        pv.RPC("Debugger_Remote_RemoveAbility", PhotonTargets.All, ability);
    }
}