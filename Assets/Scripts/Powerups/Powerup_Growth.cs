using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class Powerup_Growth : Ability
    {
        private Vector3 OriginalScale;
        public float GrowthFactor = 2;
        public override void OnAbilityAdd()
        {
            OriginalScale = transform.localScale;
            transform.localScale = OriginalScale * GrowthFactor;
        }

        public override void OnUpdate()
        {

        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void OnAbilityRemove()
        {

            transform.localScale = OriginalScale;
        }
    }
}
