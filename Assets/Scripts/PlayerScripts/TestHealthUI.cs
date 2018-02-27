using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerStats))]
public class TestHealthUI : Photon.MonoBehaviour {

    public Text healthText;
    private PlayerStats pstats;
    
	// Use this for initialization
	void Start ()
    {
        pstats = GetComponent<PlayerStats>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        healthText.text = pstats.CurrentHp.ToString();
    }
}
