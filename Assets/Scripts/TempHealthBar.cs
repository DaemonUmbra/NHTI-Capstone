using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempHealthBar : MonoBehaviour {

    public Slider HealthBar;
    public Text Health;
    public int currentHealth;
    public int maxHealth;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Health.text = currentHealth.ToString();
        HealthBar.value = ((float)currentHealth / (float)maxHealth);
    }
}
