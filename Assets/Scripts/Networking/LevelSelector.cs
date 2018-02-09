using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField]
    List<GameObject> maps = new List<GameObject>();
    [SerializeField]
    Text desciptionText;

	// Use this for initialization
	void Start ()
    {
        if (!maps.Contains(null))
        {
            Debug.Log("Maps is not empty");
        }
        else
        {
            Debug.Log("Maps is empty, you silly");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ButtonHover(Button button)
    {
        if(button.name == "Button1")
        {
            maps[0].transform.SetAsLastSibling();
            desciptionText.text = "I AM RED";
        }
        if (button.name == "Button2")
        {
            maps[1].transform.SetAsLastSibling();
            desciptionText.text = "NOW I AM AN ORANGE";
        }
        if (button.name == "Button3")
        {
            maps[2].transform.SetAsLastSibling();
            desciptionText.text = "Purple is MY favorite color";
        }
    }

    public void ButtonClick(Button button)
    {
        if (button.name == "Button1")
        {
            //SceneManager.LoadScene("YourLevel here");
            Debug.Log("Button 1 Clicked");
        }
        if (button.name == "Button2")
        {
            //SceneManager.LoadScene("Your Level 2 Here");
            Debug.Log("Button 2 Clicked");
        }
        if (button.name == "Button3")
        {
            //SceneManager.LoadScene("Your Level 3 here");
            Debug.Log("Button 3 Clicked");
        }
    }
}
