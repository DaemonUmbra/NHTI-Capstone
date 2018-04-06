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
            //TODO: Thwacking Stick Icon
            Tier = PowerupTier.Uncommon;
        }

        // Use this for initialization
        void Start () {
		
	    }
	
	    // Update is called once per frame
	    void Update () {
		
	    }
    }
}
