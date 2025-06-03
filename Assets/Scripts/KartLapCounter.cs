using UnityEngine;
using UnityEngine.UI;

public class KartLapCounter : MonoBehaviour
{
    public int waypointCounter = 0;
    public int numberOfWaypoints = 0;

    public int currentLap = 0;
    public int numberOfLaps = 3;

    public Text lapCounter;
    public GameObject canvasObject;
    public AudioSource lapCompletionSound;

    private bool crossedStartLine = false;
    private bool raceStarted = false;
    private bool shouldCountWaypoints = false; // ✅ New flag
    private float raceStartDelayTimer = 0f;

    void Start()
    {
        DisplayLapCounter();
    }

    public void ReachWaypoint(int index)
    {
        if (!shouldCountWaypoints)
        {
            Debug.Log("Waypoint ignored before race start.");
            return;
        }

        if (index == waypointCounter)
        {
            waypointCounter++;
            Debug.Log($"Waypoint {waypointCounter}/{numberOfWaypoints} reached.");
        }
        else
        {
            Debug.Log($"Unexpected waypoint index {index}, expected {waypointCounter}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            if (!crossedStartLine)
            {
                crossedStartLine = true;
                raceStarted = true;
                raceStartDelayTimer = 0.1f; // wait 1 physics frame (~0.02–0.05s)
                waypointCounter = 0;
                Debug.Log("First time crossing finish line. Starting race delay timer...");
                return;
            }

            if (shouldCountWaypoints && waypointCounter >= numberOfWaypoints)
            {
                currentLap++;
                waypointCounter = 0;
                lapCompletionSound.Play();
                Debug.Log("Lap Completed! Current Lap: " + currentLap);

                if (currentLap >= numberOfLaps)
                {
                    Debug.Log("Race Finished");
                    canvasObject.GetComponent<TimeTrialTimer>().StopTimer();
                }
            }
            else
            {
                Debug.Log("Crossed finish line without completing waypoints.");
            }
        }
    }

    void Update()
    {
        if (raceStarted && !shouldCountWaypoints)
        {
            raceStartDelayTimer -= Time.deltaTime;
            if (raceStartDelayTimer <= 0f)
            {
                shouldCountWaypoints = true;
                Debug.Log("✅ Race is now officially counting waypoints.");
            }
        }

        DisplayLapCounter();
    }

    public void DisplayLapCounter()
    {
        lapCounter.text = $"Lap Counter: {Mathf.Clamp(currentLap + 1, 1, numberOfLaps)}/{numberOfLaps}";
    }
}
