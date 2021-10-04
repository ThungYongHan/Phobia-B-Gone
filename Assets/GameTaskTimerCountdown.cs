using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTaskTimerCountdown : MonoBehaviour
{
    public float timeRemaining = 90;
    public TextMeshPro timerText;

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            Debug.Log("The time is up");
        }
        DisplayTimeLeft(timeRemaining);
    }

    void DisplayTimeLeft(float timeLeft)
    {
        if (timeLeft < 0)
        {
            timeLeft = 0;
        }
        float minutes = Mathf.FloorToInt(timeLeft / 60);
        float seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
