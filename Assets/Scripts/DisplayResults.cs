using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayResults : MonoBehaviour
{
    public TextMeshProUGUI bestTime;
    public TextMeshProUGUI currentTime;

    void Update()
    {
        DisplayBestTime();
        DisplayCurrentTimer();
    }

    private void DisplayBestTime()
    {
        bestTime.text = string.Format("Best Time: {0:00}:{1:00}:{2:000}", TimeTrialTimerManager.bestMinutes, TimeTrialTimerManager.bestSeconds, TimeTrialTimerManager.bestMiliseconds);
    }

    private void DisplayCurrentTimer()
    {
        currentTime.text = string.Format("Time Achieved: {0:00}:{1:00}:{2:000}", TimeTrialTimerManager.currentMinutes, TimeTrialTimerManager.currentSeconds, TimeTrialTimerManager.currentMiliseconds);
    }
}