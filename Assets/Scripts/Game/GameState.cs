using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StateOfGame { Protection, Royale };

public class GameState : MonoBehaviour {

    private int playerHealth = 100;
    public StateOfGame state;
    public int minutes;
    public int seconds;
    public int fontMin;
    public int fontMax;
    public Text timeText;
    public Text gameState;

    private float refreshRate = 0.1f;
    

    void Start()
    {
        state = StateOfGame.Protection;
        timeText.fontSize = fontMin;
        timeText.color = Color.white;
        InitializeGameTime();
        InvokeRepeating("GameTime", 1f, 1f);
    }

    private void Update()
    {
        CurrentGameState();
    }

    public void CurrentGameState()
    {
        switch (state)
        {
            case StateOfGame.Protection:
                gameState.text = state.ToString();
                if (playerHealth <= 0)
                {
                    Respawn();
                }
                break;
            case StateOfGame.Royale:
                gameState.text = state.ToString();
                if (playerHealth <= 0)
                {
                    Death();
                }
                break;
            default:

                break;
        }
    }

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

	void Respawn()
    {
        Destroy(gameObject);
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
