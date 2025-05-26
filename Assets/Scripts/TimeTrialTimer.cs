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
    }

    public TextMeshProUGUI getTimerText()
    {
        return timerText;
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int miliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);

        timerText.text = string.Format("Timer: {0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds);
    }
}