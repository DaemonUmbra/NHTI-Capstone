using UnityEngine;

namespace Powerups
{
    public class Powerup_Confuse : PassiveAbility
    {

        public override void OnAbilityAdd()
        {
            Name = "Confuse";
        }
        // Update is called once per frame
        private void Update()
        {
        }
    }
}