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
        Vector3 worldPoint;

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
            base.OnAbilityRemove();
        }
        protected override void Activate()
        {
            if (!photonView.isMine)
            {
                return;
            }
            Debug.Log("Snipe Activated");
            CameraController cController = GetComponent<CameraController>();
            Camera c = cController.cam;
            Vector2 mPos = Input.mousePosition;
            worldPoint = c.ScreenToWorldPoint(mPos);
            RaycastHit hit;
            Physics.Raycast(transform.position, worldPoint, out hit);
            Debug.Log(hit.transform.gameObject.name);
            Debug.DrawLine(transform.position, worldPoint, Color.blue, 5);
            //StartCoroutine(SnipeRay());
            base.Activate();
        }
        IEnumerator SnipeRay()
        {
            manager.AddSubModel("Beam");
            yield return new WaitForSecondsRealtime(.5f);
            manager.RemoveSubModel("Beam");
        }
        private void OnDrawGizmos()
        {
            Debug.Log("hello");
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, worldPoint);
        }
    }
}