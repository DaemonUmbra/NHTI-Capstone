using System;
using UnityEngine;
using Powerups;

/// <summary>
/// DOES NOT WORK
/// </summary>
public class PowerupDebugger : Photon.MonoBehaviour
{
    [HideInInspector]
    public AbilityManager Player = null;

    [HideInInspector]
    public Type SelectedPowerup;

    [HideInInspector]
    public BaseAbility SelectedPlayerPowerup;

    public void AddAbility(BaseAbility ability)
    {
        if (Player && photonView.isMine)
        {
            if (Player.CanPickupAbility(ability))
            {
                Player.AddAbility(ability);
            }
        }
    }

    public void RemoveAbility(BaseAbility ability)
    {
        if (Player && photonView.isMine)
        {
            if (!Player.CanPickupAbility(ability))
            {
                Player.RemoveAbility(ability);
            }
        }
    }
}