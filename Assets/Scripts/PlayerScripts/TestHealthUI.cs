using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerStats))]
public class TestHealthUI : Photon.MonoBehaviour {

    //set in editor
    public Text healthText;

    GameObject Player;
    private PlayerStats pstats;
    private AbilityManager abilities;
    public Slider HealthBar;
    public Text Health;
    public Text powerups;
    public AbilitySlots[] slotsActive;
    public AbilitySlots[] slotsPassive;

    // Use this for initialization
    void Start ()
    {
        if (photonView.isMine) {
            Player = photonView.gameObject;
            pstats = Player.GetComponent<PlayerStats>();
            abilities = Player.GetComponent<AbilityManager>();
            HealthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
            GameObject.Find("HealthBar").GetComponent<TempHealthBar>().SetPlayer(gameObject);
            Health = GameObject.Find("HealthBar").transform.Find("Health").GetComponent<Text>();
            powerups = GameObject.Find("Powerups").GetComponent<Text>();
            for (int i = 0; i < 6; i++)
            {
                string slotName = "Active_" + i;
                slotsActive[i].Icon = GameObject.Find(slotName).GetComponent<Image>();
                slotsActive[i].taken = false;
            }
            for (int i = 0; i < 6; i++)
            {
                string slotName = "Passive_" + i;
                slotsPassive[i].Icon = GameObject.Find(slotName).GetComponent<Image>();
                slotsPassive[i].taken = false;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (photonView.isMine)
        {
            HealthBar.value = ((float)pstats.GetComponent<PlayerStats>().CurrentHp / (float)pstats.GetComponent<PlayerStats>().MaxHp);
            healthText.text = pstats.CurrentHp.ToString();
            Health.text = pstats.CurrentHp.ToString();
            powerups.text = "";
            List<string> abilityNames = new List<string>(abilities.AbilityList.Keys);
            foreach (var power in abilityNames)
            {
                powerups.text += power + "\n";
            }

        }
    }
}

[System.Serializable]
public class AbilitySlots
{
    public Image Icon;
    public bool taken;
}
