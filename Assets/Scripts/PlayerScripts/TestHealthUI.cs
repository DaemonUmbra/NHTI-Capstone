using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerStats))]
public class TestHealthUI : Photon.MonoBehaviour {

    public Text healthText;
    private PlayerStats pstats;
    public Slider HealthBar;
    public Text Health;

    // Use this for initialization
    void Start ()
    {
        pstats = GetComponent<PlayerStats>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        HealthBar.value = ((float)pstats.GetComponent<PlayerStats>().CurrentHp / (float)pstats.GetComponent<PlayerStats>().MaxHp);
        healthText.text = pstats.CurrentHp.ToString();
        Health.text = healthText.text;
    }
}
