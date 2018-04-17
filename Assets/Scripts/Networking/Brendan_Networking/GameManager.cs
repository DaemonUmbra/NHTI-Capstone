using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using UnityEngine.UI;

public enum GameState { Preparation, Brawl, Royale, SuddenDeath, GameOver};
public enum GameMode { Royale, Brawl };
public class GameManager : Photon.PunBehaviour {

    #region Private Variables
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
        get { return Time.time - stateStartTime; }
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
        get { return (float)PhotonNetwork.room.CustomProperties["stateStartTime"]; }
        private set { PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "stateStartTime", value } }); }
    }
    public float prepTime
    {
        get { return (float)PhotonNetwork.room.CustomProperties["prepTime"]; }
        private set { PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "prepTime", value } }); }
    }
    public float brawlTime
    {
        get { return (float)PhotonNetwork.room.CustomProperties["brawlTime"]; }
        private set { PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "brawlTime", value } }); }
    }
    public float royaleTime
    {
        get { return (float)PhotonNetwork.room.CustomProperties["royaleTime"]; }
        private set { PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "royaleTime", value } }); }
    }
    public int playersLeft
    {
        get { return (int)PhotonNetwork.room.CustomProperties["PlayersLeft"]; }
        private set { PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "playersLeft", value } }); }
    }
    #endregion

    private void Awake()
    {
        UI = GetComponent<GameStateUI>();

        if (PhotonNetwork.isMasterClient)
        {
            // Set the start time
            startTime = Time.time;
            ChangeGameState(GameState.Preparation);
        }
    }

    // Update is called once per frame
    void Update () {

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
	}

    public void UpdateGameTime()
    {
        // Update game time
        gameTime += Time.deltaTime;
    }

    
    
    // Change the current game state
    public void ChangeGameState(GameState newState)
    {
        photonView.RPC("RPC_ChangeGameState", PhotonTargets.All, (byte)newState); 
    }

    #region Photon RPCs
    [PunRPC]
    private void RPC_ChangeGameState(byte newState)
    {
        // Master updates server properties
        if(PhotonNetwork.isMasterClient)
        {
            gameState = (GameState)newState;
            stateStartTime = Time.time;
        }
        // Update game state UI
        UI.UpdateStateText();
    }
    #endregion
}

