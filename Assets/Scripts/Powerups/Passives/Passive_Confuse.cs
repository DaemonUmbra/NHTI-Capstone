using UnityEngine;

namespace Powerups
{
    public class Passive_Confuse : PassiveAbility
    {

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            Name = "Confuse";
            //TODO: Confuse Icon
            Tier = PowerupTier.Rare;
        }

        public override void OnAbilityAdd()
        {

        }
        // Update is called once per frame
        private void Update()
        {
        }
    }
}