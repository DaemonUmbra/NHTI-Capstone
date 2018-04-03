using UnityEngine;

namespace Powerups
{
    public class Passive_FeatherFall : PassiveAbility
    {
        public float maxSpeed = 5f;
        private Rigidbody rb;
        
        private void Awake()
        {
            Name = "Feather Fall";
            Icon = Resources.Load<Sprite>("Images/Featherfall");
        }
        public override void OnAbilityAdd()
        {
            rb = gameObject.GetComponent<Rigidbody>();
            // Call base function
            base.OnAbilityAdd();
        }

        // Update is called once per frame
        public override void OnUpdate()
        {
            if (rb.velocity.y < -maxSpeed)  // Check that the y vel is less than neg maxSpeed
            {
                Vector3 ogVelocity = rb.velocity;

                Vector3 clamp = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
                Debug.Log("Da bug Check");
                rb.velocity = new Vector3(ogVelocity.x, clamp.y, ogVelocity.z);
            }
            // Call base function
            base.OnUpdate();
        }
    }
}