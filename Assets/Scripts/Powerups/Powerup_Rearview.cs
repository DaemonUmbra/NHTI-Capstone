﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{ public class Powerup_Rearview : PassiveAbility
    {

        //Camera Object
        private Vector3 cameraOffset = new Vector3(0, 0, 0);
        private Quaternion cameraRotation = Quaternion.Euler(0, 180, 0);

        //Camera Viewport
        private Vector2 cameraPosition; //Set automatically to fit the camerta on screen according to its size
        private Vector2 cameraSize = new Vector2(.35f, .35f);

        private GameObject cameraDummy;
        new private Camera camera;
        public override void OnAbilityAdd()
        {
            Name = "Rearview";
            //Created a camera dummy
            cameraDummy = new GameObject
            {
                name = "Rearview Camera"
            };

            //and set its properties
            cameraDummy.transform.parent = transform;
            cameraDummy.transform.localPosition = cameraOffset;
            cameraDummy.transform.localRotation = cameraRotation;

            //And add a camera to it
            camera = cameraDummy.AddComponent<Camera>();

            //Now choose where it renders onscreen
            camera.rect = new Rect(new Vector2(1-cameraSize.x,1-cameraSize.y), cameraSize);
        }

        public override void OnAbilityRemove()
        {
            Destroy(camera);
            Destroy(cameraDummy);
            base.OnAbilityRemove();
        }

        public override void OnUpdate()
        {

        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
