using UnityEngine;
using UnityEngine.UI;

public static class TimeTrialTimerManager
{
    // Stores the Best Times
    public static int bestMinutes = int.MaxValue;
    public static int bestSeconds = int.MaxValue;
    public static int bestMiliseconds = int.MaxValue;

    // Stores the Current Time achieved
    public static int currentMinutes = int.MaxValue;
    public static int currentSeconds = int.MaxValue;
    public static int currentMiliseconds = int.MaxValue;
}