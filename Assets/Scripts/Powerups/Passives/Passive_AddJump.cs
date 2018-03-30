namespace Powerups  //Ian MacKenzie
{
    public class Passive_AddJump : PassiveAbility
    {
        private PlayerController playerController;

        // Use this for initialization
        private void Awake()
        {
            Name = "Add Jump";
        }

        public override void OnAbilityAdd()
        {
            playerController = GetComponent<PlayerController>();
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