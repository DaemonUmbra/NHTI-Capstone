using UnityEngine;
using UnityEngine.UI;

namespace Powerups
{

    /// <summary>
    /// Base class for all abilities/powerups.
    /// Abilities should be derived from either ActiveAbility or PassiveAbility
    /// </summary>
    public abstract class BaseAbility : Photon.MonoBehaviour
    {
        [SerializeField]
        protected string Name = "New Ability";

        public string GetName { get { return Name; } }

        private PowerupTier tier = PowerupTier.Common;

        public PowerupTier Tier
        {
            get
            {
                return tier;
            }

            protected set
            {
                tier = value;
            }
        }

        [SerializeField]
        protected bool active = false;
        [SerializeField]
        protected bool unique = true;

        public bool IsActive { get { return active; } }
        public bool IsUnique { get { return unique; } }

        public Sprite Icon;
        protected string IconPath;

        private void Awake()
        {
            // Load the icon if another doesn't already exist
            if(Icon == null && IconPath != null)
            {
                Icon = Resources.Load<Sprite>(IconPath);
            }
        }

        #region Virtual Methods
        /// <summary>
        /// Called when abilities are added to a player.
        /// Make sure to set the ability name string!
        /// </summary>
        public virtual void OnAbilityAdd()
        {
            active = true;
        }

        /// <summary>
        /// Called by the ability manager on each update step.
        /// Use this instead of Unity's Update()
        /// </summary>
        public virtual void OnUpdate()
        {
            // Nothing in base class update yet. Still call it in your overrides.
        }

        /// <summary>
        /// Called when an ability is removed from the player
        /// </summary>
        public virtual void OnAbilityRemove()
        {
            active = false;
        }
        #endregion Abstract Methods


        #region Photon RPCs

        [PunRPC]
        public void RPC_OnAbilityAdd()
        {
            OnAbilityAdd();
        }

        [PunRPC]
        public void RPC_OnAbilityRemove()
        {
            OnAbilityRemove();
        }

        #endregion Photon RPCs
    }

    public enum PowerupTier
    {
        Common,
        Uncommon,
        Rare,
        Legendary,
        OP
    }
}