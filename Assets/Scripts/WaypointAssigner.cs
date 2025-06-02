using System.Collections.Generic;
using UnityEngine;

public class WaypointAssigner : MonoBehaviour
{
    public List<WaypointTrigger> waypoints = new List<WaypointTrigger>();
    public GameObject tracksPlaced;

    public void FinalizeWaypoints()
    {
        waypoints.Clear();
        HashSet<Transform> uniqueTracks = new HashSet<Transform>();
        WaypointTrigger[] triggers = GameObject.FindObjectsOfType<WaypointTrigger>();

        Debug.Log($"Found {triggers.Length} WaypointTriggers in scene.");

        foreach (var trigger in triggers)
        {
            Transform parentTrack = trigger.transform.parent;
            if (parentTrack == null || parentTrack == tracksPlaced.transform)
            {
                Debug.LogWarning($"Skipping invalid waypoint '{trigger.name}'");
                continue;
            }

            if (!uniqueTracks.Contains(parentTrack))
            {
                uniqueTracks.Add(parentTrack);
                waypoints.Add(trigger);
                trigger.waypointIndex = waypoints.Count - 1;
                Debug.Log($"Waypoint #{trigger.waypointIndex} assigned to track '{parentTrack.name}' at {trigger.transform.position}");
            }
        }

        Debug.Log($"Waypoint Finalization Complete. Total unique waypoints assigned: {waypoints.Count}");
    }

    public int GetWaypointCount() => waypoints.Count;
}
