using UnityEngine;

namespace Powerups
{
    public class Passive_Confuse : PassiveAbility
    {
        PlayerController movement;
        private float timeLimit = 15.0f;
        private float timer;

        private void Awake()
        {
            Name = "Confuse";
            Icon = Resources.Load<Sprite>("Images/Confuse");
            Tier = PowerupTier.Rare;
        }

        public override void OnAbilityAdd()
        {
            movement = GetComponent<PlayerController>();
            movement.InvertX = true;
            movement.InvertY = true;

            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            movement = GetComponent<PlayerController>();
            movement.InvertX = false;
            movement.InvertY = false;
            
            AbilityManager manage = gameObject.GetComponent<AbilityManager>();
            manage.RemoveAbility<Passive_ThunderCloud>();
        }

        public override void OnUpdate()
        {
            timer += Time.deltaTime;
            if (timer >= timeLimit)
            {
                OnAbilityRemove();
            }

            base.OnUpdate();
        }
    }
}