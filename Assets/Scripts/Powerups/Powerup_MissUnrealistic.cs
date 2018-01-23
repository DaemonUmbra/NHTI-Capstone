﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    /**
     * Var collections marked with //TWEAKABLE may be changed locally, do not push changes to these values
     * without permission, ask in the discord first
     **/

    public class Powerup_MissUnrealistic : PassiveAbility
    {
        //TWEAKABLE
        private float xChange = -0.5f;
        private float yChange = 0.5f;
        private float zChange = -0.5f;

        //TWEAKABLE
        private Vector3 crownOrSashOffset = new Vector3(0, 0, 0);
        private Quaternion crownOrSashRotation = Quaternion.Euler(0, 0, 0);

        public Transform crownOrSashTemplate;
        private Transform crownOrSashInstance;

        public override void OnAbilityAdd()
        {
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

            //Set the players scale
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * xChange, gameObject.transform.localScale.y * yChange, gameObject.transform.localScale.z * zChange);

            //If we have a template
            if (crownOrSashTemplate)
            {
                //Instantiate
                crownOrSashInstance = Instantiate(crownOrSashTemplate, gameObject.transform);
            }

            //Set the crown or sash's rotation and position
            crownOrSashInstance.localRotation = crownOrSashRotation;
            crownOrSashInstance.localPosition = crownOrSashOffset;
        }

        public override void OnAbilityRemove()
        {
            //Remove the crown
            //FLAW: Due to the way our powerups are handled, simply disabling the crown or sash would be a possible memory leak
            Destroy(crownOrSashInstance);

            //And undo our changes to the player's scale
            //IN-EDITOR FLAW: DO NOT change the scale modifiers whi
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x / xChange, gameObject.transform.localScale.y / yChange, gameObject.transform.localScale.z / zChange);
            base.OnAbilityRemove();
        }

        //This powerup does not need to tick
        public override void OnUpdate()
        {

        }
    }
}
