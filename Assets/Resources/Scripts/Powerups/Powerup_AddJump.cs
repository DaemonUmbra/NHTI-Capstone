using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups      //Ian MacKenzie
{
    
    public class Powerup_AddJump : PassiveAbility
    {
        PlayerController playerController;

        // Use this for initialization
        void Start()
        {
            playerController = GetComponent<PlayerController>();
            
        }

        public override void OnAbilityAdd()     
        {
            Name = "Add Jump";
            //!!!!!Commented out because I am waiting on the passive and active abilites, and to work with
            // someone on the player controller!!!!!.

            //Add to the max jumps variable on the player controller
            playerController.maxJumpCount++;
        }

        public override void OnAbilityRemove()
        {
            playerController.maxJumpCount--;
        }

        public override void OnUpdate()
        {
            // Nothing yet
        }
    }
}
