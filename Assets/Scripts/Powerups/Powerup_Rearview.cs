using System;
using UnityEngine;

namespace Powerups
{
    [Serializable]
    public class Powerup_Rearview : PassiveAbility
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
            /*
            pv = PhotonView.Get(this);

            pv.RPC("Rearview_AddAbility", PhotonTargets.All);
            */

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
            if (photonView.isMine)
            {
                //And add a camera to it
                camera = cameraDummy.AddComponent<Camera>();

                //Now choose where it renders onscreen
                camera.rect = new Rect(new Vector2(1 - cameraSize.x, 1 - cameraSize.y), cameraSize);
            }
            base.OnAbilityAdd();
        }

        public override void OnAbilityRemove()
        {
            //pv.RPC("Rearview_RemoveAbility", PhotonTargets.All);
            if (photonView.isMine)
            {
                Destroy(camera);
                Destroy(cameraDummy);
            }
            base.OnAbilityRemove();
        }

        /*
        [PunRPC]
        void Rearview_AddAbility()
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
            camera.rect = new Rect(new Vector2(1 - cameraSize.x, 1 - cameraSize.y), cameraSize);
        }

        [PunRPC]
        void Rearview_RemoveAbility()
        {
            Destroy(camera);
            Destroy(cameraDummy);
        }
        */
    }
}