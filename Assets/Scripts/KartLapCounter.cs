using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KartLapCounter : MonoBehaviour
{
    public int waypointCounter = 1;

    // Identifies the Number of Waypoints on the Racetrack
    public int numberOfWaypoints = 0;

    public int currentLap = 0;
    public int numberOfLaps = 3;

    public Text lapCounter;

    public GameObject canvasObject;

    public AudioSource lapCompletionSound;

    void Update()
    {
        DisplayLapCounter();       
    }

    public void ReachWaypoint(int index)
    {
        // Check if the Player has reached the Waypoint in order

        if (index == waypointCounter + 1)
        {
            waypointCounter++;
            Debug.Log("KartLapCounter: Waypoint Number Reached " + waypointCounter);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            if (waypointCounter >= numberOfWaypoints)
            {
                // Increment the Race Count
                currentLap++;

                // Reset the Waypoint Counter
                waypointCounter = 0;

                lapCompletionSound.Play();

                if (currentLap >= numberOfLaps)
                {
                    Debug.Log("KartLapCounter: Race Finished");

                    // Stop the Timer
                    canvasObject.GetComponent<TimeTrialTimer>().StopTimer();
                }
                Debug.Log("KartLapCounter: Current Lap is " + currentLap);
            }
            else
            {
                Debug.Log("KartLapCounter: Crossed through Finish Line");
            }
        }
    }

    public Text getLapCounter()
    {
        return lapCounter;
    }
    
    public void DisplayLapCounter()
    {
        lapCounter.text = string.Format($"Lap Counter: {currentLap + 1}/{numberOfLaps}");
    }
}