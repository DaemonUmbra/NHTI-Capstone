using UnityEngine;

namespace Powerups
{
    /**
     * Var collections marked with //TWEAKABLE may be changed locally, do not push changes to these values
     * without permission, ask in the discord first
     **/

    public class Powerup_Aliens : BaseAbility
    {

        PlayerStats playerStats;

        //TWEAKABLE
        [SerializeField]
        private float xChange = 0.5f;

        [SerializeField]
        private float yChange = -0.5f;

        [SerializeField]
        private float zChange = 0.5f;
        
        public override void OnAbilityAdd()
        {
            playerStats = GetComponent<PlayerStats>();
            /*** Handled by base class ***
            pv = PhotonView.Get(this);
            pv.RPC("Miss_Unrealistic_AddAbility", PhotonTargets.All);
            */

            Name = "Aliens";

            playerStats.AddScaleFactor(Name, new Vector3(1 + xChange, 1 + yChange, 1 + zChange));

            base.OnAbilityAdd();
        }

        public void ApplyScaleChange()
        {

        }

        /*** Handled by base class
        [PunRPC]
        void Miss_Unrealistic_AddAbility()
        {
            Name = "Miss Unrealistic";
            //If we don't have a template set in the editor
            if (!crownOrSashTemplate)
            {
                //Try searching for a crown prefab
                Resources.Load("Prefabs/Crown");
            }

            //If that doesn't exist
            if (!crownOrSashTemplate)
            {
                //Try a sash
                Resources.Load("Prefabs/Sash");
            }

            //If that doesn't work either
            if (!crownOrSashTemplate)
            {
                //Log a warning
                Debug.LogWarning("No Crown or Sash prefab set, could not apply to model");
            }

            ActualChanges = new Vector3(gameObject.transform.localScale.x * xChange, gameObject.transform.localScale.y * yChange, gameObject.transform.localScale.z * zChange);

            //Set the players scale
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + ActualChanges.x, gameObject.transform.localScale.x + ActualChanges.y, gameObject.transform.localScale.x + ActualChanges.z);

            //If we have a template
            if (crownOrSashTemplate)
            {
                //Instantiate
                crownOrSashInstance = Instantiate(crownOrSashTemplate, gameObject.transform);
            }

            //If we have successfully spawned the Crown or Sash
            if (crownOrSashInstance)
            {
                //Set the crown or sash's rotation and position
                crownOrSashInstance.localRotation = crownOrSashRotation;
                crownOrSashInstance.localPosition = crownOrSashOffset;
            }
        }

        [PunRPC]
        void Miss_Unrealistic_RemoveAbility()
        {
            //Remove the crown
            //FLAW: Due to the way our powerups are handled, simply disabling the crown or sash would be a possible memory leak
            Destroy(crownOrSashInstance);

            //And undo our changes to the player's scale
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - ActualChanges.x, gameObject.transform.localScale.y - ActualChanges.y, gameObject.transform.localScale.z - ActualChanges.z);
            base.OnAbilityRemove();
        }
        */

        public override void OnAbilityRemove()
        {
            playerStats.RemoveScaleFactor(Name);

            base.OnAbilityRemove();
        }
    }
}