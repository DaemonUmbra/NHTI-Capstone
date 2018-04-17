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
            _name = "Magic Overload";
            //TODO: Thundercloud Icon
            Tier = PowerupTier.Uncommon;
        }
        public override void OnAbilityAdd()
        {
            ps = GetComponentInParent<PlayerStats>();

            ps.AddSpeedBoost(_name, 0.1f);
            ps.JumpPower += 1.0f;
            ps.AddDmgBoost(_name, 2.0f);
            ps.ChangeMaxHp(10.0f);

            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            ps = GetComponentInParent<PlayerStats>();

            ps.RemoveSpeedBoost(_name);
            ps.JumpPower -= 1.0f;
            ps.RemoveDmgBoost(_name);
            ps.ChangeMaxHp(-10.0f);
            
            AbilityManager manage = gameObject.GetComponent<AbilityManager>();
            manage.RemoveAbility<Passive_ThunderCloud>();
        }

        public override void OnUpdate()
        {
            timer += Time.deltaTime;
            if (timer >= timeLimit)
            {
                //Take damage if player doesn't get rid of powerup in time
                PlayerStats ps = gameObject.GetComponent<PlayerStats>();
                ps.TakeDamage(20.0f);

                AbilityManager manage = gameObject.GetComponent<AbilityManager>();
                manage.RemoveAbility<Passive_ThunderCloud>();
            }

            base.OnUpdate();
        }

        private void OnCollisionEnter(Collision collision)
        {
            AbilityManager manager = collision.gameObject.GetComponent<AbilityManager>();
            manager.AddAbility<Passive_ThunderCloud>();

            AbilityManager manage = gameObject.GetComponent<AbilityManager>();
            manage.RemoveAbility<Passive_ThunderCloud>();
        }
    }
}
