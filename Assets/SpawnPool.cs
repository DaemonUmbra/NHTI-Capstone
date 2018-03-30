using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Powerups;

public class SpawnPool : MonoBehaviour {


    List<BaseAbility> _possibleAbilities;

    private void Awake()
    {
        _possibleAbilities =  new List<BaseAbility>(GetComponents<BaseAbility>());
    }
}
