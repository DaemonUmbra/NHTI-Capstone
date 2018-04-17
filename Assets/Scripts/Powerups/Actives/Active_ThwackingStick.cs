using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{ 
    public class Active_ThwackingStick : ActiveAbility {

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            Name = "Thwacking Stick";
            Icon = Resources.Load<Sprite>("Images/Thwacking Stick");
            Tier = PowerupTier.Uncommon;
        }

        // Use this for initialization
        void Start () {
		
	    }
	
	    // Update is called once per frame
	    void Update () {
		
	    }

        public override void OnAbilityAdd()
        {
            Debug.Log(Name + " added!");
            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            base.OnAbilityRemove();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        protected override void Activate()
        {
            Debug.LogWarning("Stick");
            base.Activate();
        }

    }
}
