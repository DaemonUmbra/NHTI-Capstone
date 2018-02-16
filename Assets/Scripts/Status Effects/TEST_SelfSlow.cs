using UnityEngine;

public class TEST_SelfSlow : MonoBehaviour
{
    private SlowMovement slow;

    // Use this for initialization
    private void Start()
    {
        slow = new SlowMovement(0.25f, 3f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Slowing down...");
            slow.ApplyEffect(gameObject);
        }
    }
}