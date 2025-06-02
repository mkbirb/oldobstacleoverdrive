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

    private bool raceStarted = false;
    private bool crossedStartLine = false;

    void Start()
    {
        DisplayLapCounter();
    }

    public void ReachWaypoint(int index)
    {
        // Block any waypoint before race starts
        if (!raceStarted)
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
                raceStarted = true; // ✅ Start race only after crossing
                waypointCounter = 0;
                Debug.Log("Finish line crossed for the first time. Race has officially started.");
                return;
            }

            if (raceStarted && waypointCounter >= numberOfWaypoints)
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
        DisplayLapCounter();
    }

    public void DisplayLapCounter()
    {
        lapCounter.text = $"Lap Counter: {Mathf.Clamp(currentLap + 1, 1, numberOfLaps)}/{numberOfLaps}";
    }
}







// using System.Collections;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;

// public class KartLapCounter : MonoBehaviour
// {
//     public int waypointCounter = 1;

//     // Identifies the Number of Waypoints on the Racetrack
//     public int numberOfWaypoints = 0;

//     public int currentLap = 0;
//     public int numberOfLaps = 3;

//     public Text lapCounter;

//     public GameObject canvasObject;

//     public AudioSource lapCompletionSound;

//     void Update()
//     {
//         DisplayLapCounter();       
//     }

//     public void ReachWaypoint(int index)
//     {
//         // Check if the Player has reached the Waypoint in order

//         if (index == waypointCounter + 1)
//         {
//             waypointCounter++;
//             Debug.Log("KartLapCounter: Waypoint Number Reached " + waypointCounter);
//         }
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("FinishLine"))
//         {
//             if (waypointCounter >= numberOfWaypoints)
//             {
//                 // Increment the Race Count
//                 currentLap++;

//                 // Reset the Waypoint Counter
//                 waypointCounter = 0;

//                 lapCompletionSound.Play();

//                 if (currentLap >= numberOfLaps)
//                 {
//                     Debug.Log("KartLapCounter: Race Finished");

//                     // Stop the Timer
//                     canvasObject.GetComponent<TimeTrialTimer>().StopTimer();

//                     // Also load the Results Screen
//                     SceneManager.LoadScene("ResultsScreen");
//                 }
//                 Debug.Log("KartLapCounter: Current Lap is " + currentLap);
//             }
//             else
//             {
//                 Debug.Log("KartLapCounter: Crossed through Finish Line");
//             }
//         }
//     }

//     public Text getLapCounter()
//     {
//         return lapCounter;
//     }
    
//     public void DisplayLapCounter()
//     {
//         lapCounter.text = string.Format($"Lap Counter: {currentLap + 1}/{numberOfLaps}");
//     }
// }