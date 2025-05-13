using System.Collections;
using UnityEngine;

public class WaypointTrigger: MonoBehaviour {
    // Identifies which Waypoint the attached Gameobject is part off
    public int waypointIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("IJK");
        KartLapCounter counter = other.GetComponentInParent<KartLapCounter>();

        if (counter != null) {
            counter.ReachWaypoint(waypointIndex);
        }
    }
}