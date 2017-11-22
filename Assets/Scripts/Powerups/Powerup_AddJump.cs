using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups      //Ian MacKenzie
{

    public class Powerup_AddJump : Ability
    {

        // Use this for initialization
        void Start()
        {

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
            //player.maxJumps++;
        }

        public override void OnAbilityRemove()
        {
            
            //player.maxJumps--;
        }
    }
}
