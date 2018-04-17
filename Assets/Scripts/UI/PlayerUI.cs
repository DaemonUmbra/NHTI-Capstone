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
    public AbilitySlot[] slotsActive;
    public AbilitySlot[] slotsPassive;
    public Sprite defaultActive;
    public Sprite defaultPassive;

    private int maxActives = 4;
    private int maxPassives = 6;

    // Use this for initialization
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        slotsActive = new AbilitySlot[maxActives];
        slotsPassive = new AbilitySlot[maxPassives];

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
        
        // Ability UI setup
        for (int i = 0; i < maxActives; i++)
        {
            string slotName = "Active_" + i;
            slotsActive[i] = new AbilitySlot(GameObject.Find(slotName).GetComponent<Image>(), false);
        }
        for (int i = 0; i < maxPassives; i++)
        {
            string slotName = "Passive_" + i;
            slotsPassive[i] = new AbilitySlot(GameObject.Find(slotName).GetComponent<Image>(), false);
        }
    }

    void LateUpdate()
    {
        HealthBar.value = ((float)pstats.GetComponent<PlayerStats>().CurrentHp / (float)pstats.GetComponent<PlayerStats>().MaxHp);
        //healthText.text = pstats.CurrentHp.ToString();
        txtHealth.text = pstats.CurrentHp.ToString();
        powerups.text = "";
        List<string> abilityNames = new List<string>(abilityManager.AbilityList.Keys);
        foreach (var power in abilityNames)
        {
            powerups.text += power + "\n";
        }

        UpdatePowerups();
    }
     

    public void UpdatePowerups()
    {
        // Update active ability icons
        List<ActiveAbility> actives = abilityManager.ActiveAbilities;
        for(int i = 0; i < maxActives; ++i)
        {
            if(i < actives.Count)
            {
                slotsActive[i].Icon.sprite = actives[i].Icon;
                slotsActive[i].Taken = true;
            }
            else
            {
                slotsActive[i].Icon.sprite = defaultActive;
                slotsActive[i].Taken = false;
            }
            
        }
        // Update passive ability icons
        List<PassiveAbility> passives = abilityManager.PassiveAbilities;
        for (int i = 0; i < maxPassives; ++i)
        {
            if (i < passives.Count)
            {
                slotsPassive[i].Icon.sprite = passives[i].Icon;
                slotsPassive[i].Taken = true;
            }
            else
            {
                slotsPassive[i].Icon.sprite = defaultPassive;
                slotsPassive[i].Taken = false;
            }
            
        }
    }

    public void ResetPowerups()
    {
        for (int i = 0; i < 4; i++)
        {
            slotsActive[i].Icon.sprite = defaultActive;
            slotsActive[i].Taken = false;
        }
        for (int i = 0; i < 6; i++)
        {
            slotsPassive[i].Icon.sprite = defaultPassive;
            slotsPassive[i].Taken = false;
        }
    }
}

[System.Serializable]
public class AbilitySlot
{
    public Image Icon;
    public bool Taken;

    public AbilitySlot(Image icon, bool taken)
    {
        Icon = icon;
        Taken = taken;
    }

    public AbilitySlot()
    {
    }
}
