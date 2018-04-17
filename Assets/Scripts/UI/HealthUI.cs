using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Powerups;
<<<<<<< HEAD:Assets/Scripts/UI/HealthUI.cs
public class HealthUI : Photon.MonoBehaviour {

    //set in editor
    public Text healthText;

    GameObject Player;
=======
public class PlayerUI : Photon.MonoBehaviour {

    GameObject player;
>>>>>>> 021b0cc552aa4304870a28e3df6d6e80a9d9819a:Assets/Scripts/UI/PlayerUI.cs
    private PlayerStats pstats;
    private AbilityManager abilityManager;
    public Slider HealthBar;
    public Text Health;
    public Text powerups;
    public AbilitySlots[] slotsActive;
    public AbilitySlots[] slotsPassive;
    public Sprite defaultActive;
    public Sprite defaultPassive;

    // Use this for initialization
    void Start()
    {
        if (photonView.isMine) {
            Player = photonView.gameObject;
            pstats = Player.GetComponent<PlayerStats>();
            abilityManager = Player.GetComponent<AbilityManager>();
            HealthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
            GameObject.Find("HealthBar").GetComponent<HealthBar>().SetPlayer(gameObject);
            Health = GameObject.Find("Health").GetComponent<Text>();
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
            List<string> abilityNames = new List<string>(abilityManager.AbilityList.Keys);
            foreach (var power in abilityNames)
            {
                powerups.text += power + "\n";
            }

        }
        UpdatePowerups();
    }

    public void UpdatePowerups()
    {

        List<ActiveAbility> actives = abilityManager.ActiveAbilities;
        for(int i = 0; i < actives.Count; ++i)
        {
            slotsActive[i].Icon.sprite = actives[i].Icon;
        }

        List<PassiveAbility> passives = abilityManager.PassiveAbilities;
        for (int i = 0; i < passives.Count; ++i)
        {
            slotsPassive[i].Icon.sprite = passives[i].Icon;
        }
    }

    public void ResetPowerups()
    {
        for (int i = 0; i < 4; i++)
        {
            slotsActive[i].Icon.sprite = defaultActive;
        }
        for (int i = 0; i < 6; i++)
        {
            slotsPassive[i].Icon.sprite = defaultPassive;
        }
    }
}

[System.Serializable]
public class AbilitySlots
{
    public Image Icon;
    public bool taken;
}
