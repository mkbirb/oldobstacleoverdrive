using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeTrialTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedTime = 0f;

    private bool isRunning = false;

    private float timeSinceLastUpdate = 0f;

    // Done to prevent flashing of the Timer Text
    private float updateDisplayTimeCooldown = 0.1f;

    // For the Displaying of the time
    private int minutes;
    private int seconds;
    private int miliseconds;

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            timeSinceLastUpdate += Time.deltaTime;

            if (timeSinceLastUpdate >= updateDisplayTimeCooldown)
            {
                UpdateTimerDisplay();
                timeSinceLastUpdate = 0;   
            }
        }
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;

        // Also store the Time if it is the best
        int currentTotalMs = (minutes * 60 * 1000) + (seconds * 1000) + miliseconds;
        int bestTotalMs = (TimeTrialTimerManager.bestMinutes * 60 * 1000) + (TimeTrialTimerManager.bestSeconds * 1000) + TimeTrialTimerManager.bestMiliseconds;

        if (currentTotalMs < bestTotalMs)
        {
            TimeTrialTimerManager.bestMinutes = minutes;
            TimeTrialTimerManager.bestSeconds = seconds;
            TimeTrialTimerManager.bestMiliseconds = miliseconds;
        }

        // Also give the Time that the Player got to the Manager
        TimeTrialTimerManager.currentMinutes = minutes;
        TimeTrialTimerManager.currentSeconds = seconds;
        TimeTrialTimerManager.currentMiliseconds = miliseconds;

    }

    public TextMeshProUGUI getTimerText()
    {
        return timerText;
    }

    private void UpdateTimerDisplay()
    {
        minutes = Mathf.FloorToInt(elapsedTime / 60f);
        seconds = Mathf.FloorToInt(elapsedTime % 60);
        miliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);

        timerText.text = string.Format("Timer: {0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds);
    }
}