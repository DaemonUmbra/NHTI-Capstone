namespace Powerups      //Ian MacKenzie
{
    public class Powerup_AddJump : PassiveAbility
    {
        private PlayerController playerController;

        // Use this for initialization
        private void Start()
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

            // Call base function
            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            playerController.maxJumpCount--;

            // Call base function
            base.OnAbilityRemove();
        }
    }
}