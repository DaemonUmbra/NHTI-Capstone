using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Powerups;

public class SpawnPool : Photon.MonoBehaviour {


    List<BaseAbility> _abilityPool;
    

    public List<BaseAbility> AbilityPool { get { return _abilityPool; } }

    public void Init()
    {
        if(_abilityPool == null)
            _abilityPool =  new List<BaseAbility>(GetComponents<BaseAbility>());
    }

    public BaseAbility RandomPowerup()
    {
        if (_abilityPool.Count > 0)
        {
            int idx = Random.Range(0, _abilityPool.Count);
            return _abilityPool[idx];
        }
        else
        {
            Debug.LogError("Spawn pool contains no abilities.");
            return null;
        }
    }
}
