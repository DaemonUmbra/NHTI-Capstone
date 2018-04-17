using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Photon.MonoBehaviour {

    public Slider hpSlider;
    public Text Health;
    private GameObject Player;
    private PlayerStats pstats;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        //Health.text = pstats.CurrentHp.ToString();
        if (Player != null)
        {
            hpSlider.value = (Player.GetComponent<PlayerStats>().CurrentHp / Player.GetComponent<PlayerStats>().MaxHp);
        }
    }

    public void SetPlayer(GameObject Player)
    {
        this.Player = Player;
    }
}
