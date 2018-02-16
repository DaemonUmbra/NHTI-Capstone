using UnityEngine;

public class TEST_SelfConfusion : MonoBehaviour
{
    private Confuse confusion;

    private void Start()
    {
        confusion = new Confuse(5.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            confusion.ApplyEffect(gameObject);
            confusion.Activate();
        }

        if (Input.GetKeyDown("o"))
        {
            confusion.RemoveEffect();
        }
    }
}