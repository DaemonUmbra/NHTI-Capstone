using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace Powerups
//{
    public class IceBall : ActiveAbility
    {
        
        // Use this for initialization
        void Awake()
        {
            Name = "Ice Ball";
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void OnAbilityAdd()
        {
            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            base.OnAbilityRemove();
        }
        protected override void Activate()
        {
            base.Activate();
        }
    }
//}