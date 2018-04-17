using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Powerups;

namespace Powerups
{

    public class Passive_ThunderCloud : PassiveAbility
    {
        PlayerStats ps;
        private float timeLimit = 15.0f;
        private float timer;

        private void Awake()
        {
            Name = "Magic Overload";
            //TODO: Thundercloud Icon
            Tier = PowerupTier.Uncommon;
        }
        public override void OnAbilityAdd()
        {
            ps = GetComponentInParent<PlayerStats>();

            ps.AddSpeedBoost(Name, 0.1f);
            ps.JumpPower += 1.0f;
            ps.AddDmgBoost(Name, 2.0f);
            ps.ChangeMaxHp(10.0f);

            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            ps = GetComponentInParent<PlayerStats>();

            ps.RemoveSpeedBoost(Name);
            ps.JumpPower -= 1.0f;
            ps.RemoveDmgBoost(Name);
            ps.ChangeMaxHp(-10.0f);

            base.OnAbilityRemove();
        }

        public override void OnUpdate()
        {
            timer += Time.deltaTime;
            if (timer >= timeLimit)
            {
                //Take damage if player doesn't get rid of powerup in time
                PlayerStats ps = gameObject.GetComponent<PlayerStats>();
                ps.TakeDamage(20.0f);

                OnAbilityRemove();
            }

            base.OnUpdate();
        }

        private void OnCollisionEnter(Collision collision)
        {
            AbilityManager manager = collision.gameObject.GetComponent<AbilityManager>();
            manager.AddAbility<Passive_ThunderCloud>();

            OnAbilityRemove();
        }
    }
}
