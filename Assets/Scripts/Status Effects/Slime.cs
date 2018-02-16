using UnityEngine;

public class Slime : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            GameObject target = col.gameObject;
            target.GetComponent<Rigidbody>().AddForce(target.transform.position * -.00000000000001f);
        }
    }
}