﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour {
    //Needs Edited Health to proper int
    public static PlayerManagement Instance;
    private PhotonView PhotonView;

    private List<PlayerStats> PlayerStats = new List<PlayerStats>();

	private void Awake () {
        Instance = this;
        PhotonView = GetComponent<PhotonView>();
	}
	
    public void AddPlayerStats(PhotonPlayer photonPlayer)
    {
        int index = PlayerStats.FindIndex(x => x.PhotonPlayer == photonPlayer);
        if(index == -1)
        {
            PlayerStats.Add(new PlayerStats(photonPlayer, 30));
        }
    }

    public void ModifyHealth(PhotonPlayer photonPlayer, int value)
    {
        int index = PlayerStats.FindIndex(x => x.PhotonPlayer == photonPlayer);
        if (index != -1)
        {
            PlayerStats playerStats = PlayerStats[index];
            playerStats.Health += value;
            PlayerNetwork.Instance.NewHealth(photonPlayer, playerStats.Health);
        }
    }
}

