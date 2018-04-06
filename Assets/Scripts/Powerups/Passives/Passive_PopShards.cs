using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class Passive_PopShards : PassiveAbility
    {

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            Name = "Pop Shards";
            //TODO: Pop Shards Icon
            Tier = PowerupTier.Common;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
