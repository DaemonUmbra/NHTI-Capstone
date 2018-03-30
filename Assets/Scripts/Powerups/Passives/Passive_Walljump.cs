using UnityEngine;

namespace Powerups
{
    /// <summary>
    /// <para>Author: Ian Cahoon</para>
    /// <para>Power-Up Name: walljump(something more creative soon)</para>
    /// <para>Power-Up Category: Passive</para>
    /// <para>Power-Up Effect: Lets the player's jumps reset off of walls.</para>
    /// <para>Power-Up Tier: Rare/Legendary</para>
    /// <para>Power-Up Description: Allows the player to reset their jumpcount off of walls.</para>
    /// </summary>

    public class Passive_Walljump : PassiveAbility
    {
        public override void OnAbilityAdd()
        {
            PlayerController pc = gameObject.GetComponent<PlayerController>();
            if (photonView.isMine)
            {
                pc.canWallJump = true;
            }
            base.OnAbilityAdd();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnAbilityRemove()
        {
            PlayerController pc = gameObject.GetComponent<PlayerController>();
            if (photonView.isMine)
            {
                pc.canWallJump = false;
            }
        }
    }
}