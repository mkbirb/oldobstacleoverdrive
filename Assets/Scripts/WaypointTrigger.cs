using UnityEngine;

public class WaypointTrigger : MonoBehaviour
{
    public int waypointIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"WaypointTrigger [{waypointIndex}] hit by {other.name}");

        KartLapCounter counter = other.GetComponentInParent<KartLapCounter>();
        if (counter != null)
        {
            Debug.Log($"WaypointTrigger: Reached waypoint {waypointIndex} by {other.name}");
            counter.ReachWaypoint(waypointIndex);
        }
    }
}
