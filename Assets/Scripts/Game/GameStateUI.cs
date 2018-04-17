using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameStateUI : MonoBehaviour {

    #region Private Fields
    [SerializeField]
    GameManager manager;
    
    // Text fields
    [SerializeField]
    Text txtGameState;
    [SerializeField]
    Text txtGameMode;
    [SerializeField]
    Text txtGameTime;
    [SerializeField]
    Text txtStateTime;
    [SerializeField]
    Text txtPlayersLeft;
    [SerializeField]
    Text txtKills;
    #endregion


    public int fontMin;
    public int fontMax;
    
    //private float refreshRate = 0.1f;
    

    private void Start()
    {
        manager = GameObject.Find(manager.name).GetComponent<GameManager>();
    }

    private void Update()
    {
        UpdateTimeText();
        //UpdatePlayerText();
    }

    #region Public Methods
    public string TimeToString(float time)
    {
        int min = (int)time / 60;
        int sec = (int)time % 60;
        string strTime = "";

        // Game time to string
        if (min == 0)
            strTime += "00";
        else if (min < 10)
            strTime += "0" + min.ToString();
        else
            strTime += min.ToString();
        strTime += ":";
        if (sec == 0)
            strTime += "00";
        else if (sec < 10)
            strTime += "0" + sec.ToString();
        else
            strTime += sec.ToString();

        return strTime;
    }
    public void UpdateStateText()
    {
        GameState currentState = manager.gameState;
        string strState = "Unknown Mode";

        if (currentState == GameState.Preparation)
        {
            strState = "Prepare For Battle!";
        }
        else if (currentState == GameState.Brawl)
        {
            strState = "Fight For Supplies!";
        }
        else if (currentState == GameState.Royale)
        {
            strState = "Fight To Survive!";
        }
        else if (currentState == GameState.SuddenDeath)
        {
            strState = "SUDDEN DEATH!";
        }
        else if (currentState == GameState.GameOver)
        {
            strState = "Game Over";
        }

        // Change Text
        txtGameState.text = strState;
    }
    public void UpdateModeText()
    {
        GameMode currentMode = manager.gameMode;
        string strMode = "Unknown Game Mode";

        if(currentMode == GameMode.Brawl)
        {
            strMode = "BRAWL";
        }
        else if(currentMode == GameMode.Royale)
        {
            strMode = "BATTLE ROYALE";
        }

        // Change Text
        txtGameMode.text = strMode;
    }
    public void UpdateTimeText()
    {
        txtGameTime.text = TimeToString(manager.gameTime);
        txtStateTime.text = TimeToString(manager.stateTimeLeft);

        // Set time text color
        if(manager.stateTimeLeft < 10)
        {
            txtStateTime.color = Color.red;

            // Countdown effect
            //Debug.Log(manager.gameTime % 1);
        }
        else
        {
            txtStateTime.color = Color.black;
        }
    }
    public void UpdatePlayerText()
    {
        txtPlayersLeft.text = manager.playersLeft.ToString();
    }
    public void UpdateKills(int kills)
    {
        txtKills.text = kills.ToString();
    }
    #endregion



    /*
    void GameTime()
    {
        seconds--;
        if (seconds == 0 && minutes == 0)
        {
            timeText.text = "START!";
            timeText.fontSize = 25;
            state = StateOfGame.Royale;
            CancelInvoke();
        }
        else if (seconds > 0 || minutes > 0)
        {
            if (seconds < 0)
            {
                CancelInvoke("royaleStartWarning");
                minutes--;
                seconds = 59;
            }
            if (seconds == 10 && minutes == 0)
            {
                timeText.text = "0" + minutes + ":" + seconds;
                timeText.color = Color.red;
                InvokeRepeating("royaleStartWarning", 0f, refreshRate);
            }
            else if (seconds < 10 && minutes == 0)
            {
                timeText.text = "0" + minutes + ":0" + seconds;
                timeText.color = Color.red;
            }
            else if (minutes < 10 && seconds < 10)
            {
                timeText.text = "Time To Royale: 0" + minutes + ":0" + seconds;
            }
            else if (minutes < 10)
            {
                timeText.text = "Time To Royale: 0" + minutes + ":" + seconds;
            }
            else if (seconds < 10)
            {
                timeText.text = "Time To Royale: " + minutes + ":0" + seconds;
            }
            else
            {
                timeText.text = "Time To Royale: " + minutes + ":" + seconds;
            }
        }


    }
   

    void InitializeGameTime()
    {
        if (minutes < 10 && seconds < 10)
        {
            timeText.text = "Time To Royale: 0" + minutes + ":0" + seconds;
        }
        else if (minutes < 10)
        {
            timeText.text = "Time To Royale: 0" + minutes + ":" + seconds;
        }
        else if (seconds < 10)
        {
            timeText.text = "Time To Royale: " + minutes + ":0" + seconds;
        }
        else
        {
            timeText.text = "Time To Royale: " + minutes + ":" + seconds;
        }
    }

    void royaleStartWarning()
    {
        if (timeText.fontSize < fontMax)
        {
            timeText.fontSize++;
        }
        else
        {
            timeText.fontSize = fontMin;
        }
    }

     */
}
