using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHealthUI : Photon.MonoBehaviour {

    
    public GameObject thisPlayer;

    public Text healthText;
    
    
	// Use this for initialization
	void Start ()
    {
     
        

    }
	
	// Update is called once per frame
	void Update ()
    {
        PlayerStats pStats = thisPlayer.GetComponent<PlayerStats>();
        healthText.text = pStats.Health.ToString();
    }
}
