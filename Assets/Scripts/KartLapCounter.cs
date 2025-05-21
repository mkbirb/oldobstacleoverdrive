using System.Collections;
using UnityEngine;

public class KartLapCounter: MonoBehaviour {
    public int waypointCounter = 1;
    
    // Identifies the Number of Waypoints on the Racetrack
    public int numberOfWaypoints = 0;
    
    public int currentLap = 0;
    public int numberOfLaps = 3;

    public void ReachWaypoint(int index) {
        // Check if the Player has reached the Waypoint in order

        if (index == waypointCounter + 1) {
            waypointCounter++;
            Debug.Log("KartLapCounter: Waypoint Number Reached " + waypointCounter);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine")) {
            if (waypointCounter >= numberOfWaypoints) {
                // Increment the Race Count
                currentLap++;

                // Reset the Waypoint Counter
                waypointCounter = 0;

                if (currentLap >= numberOfLaps) {
                    Debug.Log("KartLapCounter: Race Finished");
                }
                Debug.Log("KartLapCounter: Current Lap is " + currentLap);
            }
            else {
                Debug.Log("KartLapCounter: Crossed through Finish Line");
            }
        }
    }
}