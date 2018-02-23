using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HUD : MonoBehaviour {

    public int minutes;
    public int seconds;
    public int fontMin;
    public int fontMax;
    public Text timeText;

    // Use this for initialization
    void Start () {
        timeText.fontSize = fontMin;
        InitializeGameTime();
        InvokeRepeating("GameTime", 1f, 1f);
    }

    void GameTime()
    {
        seconds--;
        if (seconds == 0 && minutes == 0)
        {
            timeText.text = "START!";
            timeText.fontSize = 25;
            CancelInvoke();
        } else if(seconds > 0 || minutes > 0) {
            if (seconds < 0)
            {
                minutes--;
                seconds = 59;
            }

            if (seconds < 11 && minutes == 0)
            {
                timeText.text = "0" + minutes + ":0" + seconds;
                InvokeRepeating("royaleStartWarning", 0f, .5f);
                InvokeRepeating("royaleStartWarning", 1f, .5f);
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

    void InitializeGameTime() {
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
        if(timeText.fontSize == fontMin)
        {
            timeText.fontSize = fontMax;
        } else {
            timeText.fontSize = fontMin;
        }
    }
}

