using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerStats))]
public class TempHealthBar : Photon.MonoBehaviour {

    public Slider HealthBar;
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
            HealthBar.value = (Player.GetComponent<PlayerStats>().CurrentHp / Player.GetComponent<PlayerStats>().MaxHp);
        }
    }

    public void SetPlayer(GameObject Player)
    {
        this.Player = Player;
    }
}
