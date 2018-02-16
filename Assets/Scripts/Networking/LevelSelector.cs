using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> maps = new List<GameObject>();

    [SerializeField]
    private Text desciptionText;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void ButtonHover(Button button)
    {
        if (button.name == "Map1")
        {
            maps[0].transform.SetAsLastSibling();
        }
        if (button.name == "Map2")
        {
            maps[1].transform.SetAsLastSibling();
        }
        if (button.name == "Map3")
        {
            maps[2].transform.SetAsLastSibling();
        }
    }
}