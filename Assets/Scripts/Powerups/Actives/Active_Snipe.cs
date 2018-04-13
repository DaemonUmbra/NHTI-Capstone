using System.Collections;
using UnityEngine;

///summary
 /*
Developers and Contributors: Ian Cahoon

Information
    Name: Snipe
    Type: Active
    Effect: Basic fire turns into hitscan lightning bolts with increase damage but decreased fire rate
    Tier: Rare
  */
///summary

namespace Powerups
{
    public class Active_Snipe : ActiveAbility
    {
        //private float CDstart;
        private bool CurrentlyActive = false;
        private PlayerShoot pShoot;
        ModelManager manager;

        private void Awake()
        {
            // Set name
            Cooldown = 5f;
            Name = "Snipe";
            //Set Sprite
            Icon = Resources.Load<Sprite>("Images/Sniper");
            Tier = PowerupTier.Rare;
        }
        public override void OnAbilityAdd()
        {
            //Powerup added
            Debug.Log(Name + " Added");
            manager = GetComponent<ModelManager>();
            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            manager.SetModel("Default");
            base.OnAbilityRemove();
        }
        protected override void Activate()
        {
            if (!photonView.isMine)
            {
                return;
            }

            StartCoroutine(SnipeRay());
            base.Activate();
        }
        IEnumerator SnipeRay()
        {
            manager.SetModel("Beam");
            yield return new WaitForSecondsRealtime(.5f);
            manager.SetModel("Default");
        }
    }
}