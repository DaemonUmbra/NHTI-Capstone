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

        // Update is called once per frame
        void Update()
        {

        }

        public override void OnAbilityAdd()     
        {
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
            throw new System.NotImplementedException();
        }
    }
}
