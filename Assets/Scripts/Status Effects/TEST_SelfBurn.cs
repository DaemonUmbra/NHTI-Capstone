using UnityEngine;

public class TEST_SelfBurn : MonoBehaviour
{
    private BurnDamage burn;

    // Use this for initialization
    private void Start()
    {
        burn = new BurnDamage(10f, 3f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            burn.ApplyEffect(gameObject);
        }
    }
}