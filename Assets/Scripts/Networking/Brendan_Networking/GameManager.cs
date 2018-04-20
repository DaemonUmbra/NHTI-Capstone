using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using UnityEngine.UI;

public enum GameState { Preparation, Brawl, Royale, SuddenDeath, GameOver};
public enum GameMode { Royale, Brawl };
public class GameManager : Photon.PunBehaviour {

    #region Private Variables
    [SerializeField]
    GameStateUI UI;
    #endregion

    #region Room Options
    // Access variables for Room Properties.
    // Stored on the server
    public GameMode gameMode {
        get { return (GameMode)PhotonNetwork.room.CustomProperties["GameMode"]; }
        private set { PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "GameMode", value } }); }
    }
    public GameState gameState {
        get { return (GameState)PhotonNetwork.room.CustomProperties["GameState"]; }
        private set { PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "GameState", value } }); }
    }
    public float gameTime
    {
        get { return (float)PhotonNetwork.room.CustomProperties["GameTime"]; }
        private set { PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "GameTime", value } }); }
    }
    public int Map
    {
        get { return (int)PhotonNetwork.room.CustomProperties["Map"]; }
        private set { PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "Map", value } }); }
    }
    public float startTime
    {
        get { return (float)PhotonNetwork.room.CustomProperties["StartTime"]; }
        private set { PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "StartTime", value } }); }
    }
    public float stateTime
    {
        get { return gameTime - stateStartTime; }
    }
    public float stateTimeLeft
    {
        get
        {
            float remTime = 0f;
            if (gameState == GameState.Preparation)
            {
                remTime = prepTime - stateTime;
            }
            else if (gameState == GameState.Brawl)
            {
                remTime = brawlTime - stateTime;
            }
            else if (gameState == GameState.Royale)
            {
                remTime = royaleTime - stateTime;
            }

            return remTime;
        }
    }
    public float stateStartTime
    {
        get { return (float)PhotonNetwork.room.CustomProperties["StateStartTime"]; }
        private set { PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "StateStartTime", value } }); }
    }
    public float prepTime
    {
        get { return (float)PhotonNetwork.room.CustomProperties["PrepTime"]; }
        private set { PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "PrepTime", value } }); }
    }
    public float brawlTime
    {
        get { return (float)PhotonNetwork.room.CustomProperties["BrawlTime"]; }
        private set { PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "BrawlTime", value } }); }
    }
    public float royaleTime
    {
        get { return (float)PhotonNetwork.room.CustomProperties["RoyaleTime"]; }
        private set { PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "RoyaleTime", value } }); }
    }
    public int playersLeft
    {
        get { return (int)PhotonNetwork.room.CustomProperties["PlayersLeft"]; }
        private set { PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "PlayersLeft", value } }); }
    }
    #endregion

    private void Awake()
    {
        UI = GameObject.Find("PlayerCanvas").GetComponent<GameStateUI>();
    }
    private void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {
            // Set the start time
            startTime = Time.time;
            ChangeGameState(GameState.Preparation);
        }

        playersLeft = PhotonNetwork.playerList.Length;
    }


    // Update is called once per frame
    void Update() {

        if (PhotonNetwork.isMasterClient)
        {
            // State specific update logic
            GameState currentState = gameState;
            if (currentState == GameState.Preparation)
            {
                // Check if prep phase ended
                if (stateTime > prepTime)
                {
                    ChangeGameState(GameState.Brawl);
                }
            }
            else if (currentState == GameState.Brawl)
            {
                // Check if brawl phase ended
                if (stateTime > brawlTime)
                {
                    ChangeGameState(GameState.Royale);
                }
            }
            else if (currentState == GameState.Royale)
            {
                // Check if royale phase ended
                if (stateTime > royaleTime)
                {
                    ChangeGameState(GameState.SuddenDeath);
                }
                else if (playersLeft <= 1)
                {
                    ChangeGameState(GameState.GameOver);
                }
            }
            else if (currentState == GameState.SuddenDeath)
            {
                // Check if there is only 1 player left
                if (playersLeft == 1)
                {
                    ChangeGameState(GameState.GameOver);
                }
            }
        }

        UpdateGameTime();
    }

    public void UpdateGameTime()
    {
        // Update game time
        if (PhotonNetwork.isMasterClient)
        {
            gameTime = Time.time - startTime;
        }
    }

    private void ToggleInvulnerability(bool inv)
    {
        PlayerStats[] players = FindObjectsOfType<PlayerStats>();

        foreach (PlayerStats ps in players)
        {
            ps.Invulnerable = inv;
        }
    }

    private void ToggleRespawn(bool canRespawn)
    {
        PlayerStats[] players = FindObjectsOfType<PlayerStats>();

        foreach (PlayerStats ps in players)
        {
            ps.CanRespawn = canRespawn;
        }
    }

    public void RegisterDeath(GameObject deadPlayer, GameObject killer)
    {
        playersLeft--;

        PlayerStats ps = killer.GetComponent<PlayerStats>();

        if (ps)
        {
            ps.RegisterKill();
        }
    }
    public void RegisterDeath(GameObject deadPlayer)
    {
        playersLeft--;
    }

   
    
    // Change the current game state
    public void ChangeGameState(GameState newState)
    {
        gameState = newState;
        stateStartTime = gameTime;

        if(gameState == GameState.Preparation)
        {
            ToggleInvulnerability(true);
            ToggleRespawn(true);
        }
        else if(gameState == GameState.Brawl)
        {
            ToggleInvulnerability(false);
            ToggleRespawn(true);
        }
        else if(gameState == GameState.Royale)
        {
            ToggleInvulnerability(false);
            ToggleRespawn(false);
        }
        else if(gameState == GameState.SuddenDeath)
        {
            ToggleInvulnerability(false);
            ToggleRespawn(false);
        }
        else if(gameState == GameState.GameOver)
        {
            ToggleInvulnerability(true);
            ToggleRespawn(false);

            PlayerStats[] players = FindObjectsOfType<PlayerStats>();
            PlayerStats winner = null;
            if(gameMode == GameMode.Brawl)
            {
                // The winner is whoever has the highest killcount
                int topKills = 0;
                foreach(PlayerStats p in players)
                {
                    if(!p.Dead && p.Kills >= topKills)
                    {
                        winner = p;
                    }
                }
            }
            else if(gameMode == GameMode.Royale)
            {
                // Winner is last player alive
                List<PlayerStats> alivePlayers = new List<PlayerStats>();
                foreach(PlayerStats p in players)
                {
                    if(!p.Dead)
                    {
                        alivePlayers.Add(p);
                    }
                }
                // If more than one are alive the winner is whoever has the most kills
                int topKills = 0;
                foreach (PlayerStats ap in alivePlayers)
                {
                    if(ap.Kills >= topKills)
                    {
                        winner = ap;
                    }
                }
            }

            foreach(PlayerStats p in players)
            {
                if(p == winner)
                {
                    p.RegisterWin();
                }
                else
                {
                    p.RegisterLoss();
                }
            }
        }

        photonView.RPC("RPC_ChangeGameState", PhotonTargets.All, (byte)newState); 
    }

    #region Photon RPCs
    [PunRPC]
    private void RPC_ChangeGameState(byte newState)
    {
        // Update game state UI
        UI.UpdateStateText();


    }
    [PunRPC]
    private void RPC_GameOver(int winnerID)
    {

    }
    [PunRPC]
    private void RPC_RegisterDeath(int photonId, int playersLeft)
    {
        //playersLeft = 
    }
    #endregion
}

