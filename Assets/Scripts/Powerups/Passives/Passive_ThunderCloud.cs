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
            Icon = Resources.Load<Sprite>("Magic Overload");
            Tier = PowerupTier.Uncommon;
        }
        public override void OnAbilityAdd()
        {
            ps = GetComponentInParent<PlayerStats>();

            ps.AddSpeedBoost(Name, 0.25f);
            ps.JumpPower += 2.0f;
            ps.AddDmgBoost(Name, 2.5f);
            ps.ChangeMaxHp(20.0f);
            ps.GainHp(20.0f);

            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            ps = GetComponentInParent<PlayerStats>();

            ps.RemoveSpeedBoost(Name);
            ps.JumpPower -= 2.0f;
            ps.RemoveDmgBoost(Name);
            ps.ChangeMaxHp(-20.0f);

            base.OnAbilityRemove();
        }

        private void Update()
        {
            if (active)
            {
                timer += Time.deltaTime;
                if (timer >= timeLimit)
                {
                    //Take damage if player doesn't get rid of powerup in time
                    PlayerStats ps = gameObject.GetComponent<PlayerStats>();
                    ps.TakeDamage(40.0f);

                    OnAbilityRemove();
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            AbilityManager manager = collision.gameObject.GetComponent<AbilityManager>();
            manager.AddAbility<Passive_ThunderCloud>();

            OnAbilityRemove();
        }
    }
}
