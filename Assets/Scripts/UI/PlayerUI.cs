using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Powerups;
public class PlayerUI : Photon.MonoBehaviour {
    GameObject player;
    private PlayerStats pstats;
    private AbilityManager abilityManager;
    public Slider HealthBar;
    public Text txtHealth;
    public Text powerups;
    public AbilitySlots[] slotsActive;
    public AbilitySlots[] slotsPassive;
    public Sprite defaultActive;
    public Sprite defaultPassive;

    // Use this for initialization
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Find local player
        foreach(GameObject p in players)
        {
            if(p.GetPhotonView().isMine)
            {
                player = p;
            }
        }

        pstats = player.GetComponent<PlayerStats>();
        abilityManager = player.GetComponent<AbilityManager>();



        if (photonView.isMine) {
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
            HealthBar.value = (pstats.GetComponent<PlayerStats>().CurrentHp / pstats.GetComponent<PlayerStats>().MaxHp);
            txtHealth.text = pstats.CurrentHp.ToString();
            powerups.text = "";
            
            foreach (BaseAbility ab in abilityManager.AbilityList.Values)
            {
                powerups.text += ab.AbilityName + "\n";
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
            slotsActive[i].taken = true;
        }

        List<PassiveAbility> passives = abilityManager.PassiveAbilities;
        for (int i = 0; i < passives.Count; ++i)
        {
            slotsPassive[i].Icon.sprite = passives[i].Icon;
            slotsPassive[i].taken = true;
        }
    }

    public void ResetPowerups()
    {
        for (int i = 0; i < 4; i++)
        {
            slotsActive[i].Icon.sprite = defaultActive;
            slotsActive[i].taken = false;
        }
        for (int i = 0; i < 6; i++)
        {
            slotsPassive[i].Icon.sprite = defaultPassive;
            slotsPassive[i].taken = false;
        }
    }
}

[System.Serializable]
public class AbilitySlots
{
    public Image Icon;
    public bool taken;
}
