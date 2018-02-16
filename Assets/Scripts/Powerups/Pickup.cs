using UnityEngine;

public class Pickup : MonoBehaviour
{
    private BaseAbility _ability;

    // Use this for initialization
    private void Start()
    {
        _ability = GetComponent<BaseAbility>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AbilityManager aManager = other.GetComponent<AbilityManager>();
            aManager.AddAbility(_ability);
            Destroy(gameObject);
        }
    }
}