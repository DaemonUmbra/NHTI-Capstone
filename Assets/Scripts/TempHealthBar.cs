using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempHealthBar : MonoBehaviour {

    public Slider HealthBar;
    public Text Health;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Health.text = GetComponent<PlayerStats>().CurrentHp.ToString();
        HealthBar.value = ((float)GetComponent<PlayerStats>().CurrentHp / (float)GetComponent<PlayerStats>().MaxHp);
    }
}
