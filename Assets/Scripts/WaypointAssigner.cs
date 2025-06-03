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
        List<WaypointTrigger> triggers = new List<WaypointTrigger>(GameObject.FindObjectsOfType<WaypointTrigger>());

        Debug.Log($"Found {triggers.Count} WaypointTriggers in scene.");

        // Filter only those under tracksPlaced and remove duplicates by track
        List<WaypointTrigger> valid = new List<WaypointTrigger>();

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
                valid.Add(trigger);
            }
        }

        // ✅ Reverse the list to assign index 0 to the first placed track (furthest forward)
        valid.Reverse();

        for (int i = 0; i < valid.Count; i++)
        {
            valid[i].waypointIndex = i;
            waypoints.Add(valid[i]);
            Debug.Log($"✅ Waypoint #{i} assigned to track '{valid[i].transform.parent.name}' at {valid[i].transform.position}");
        }

        Debug.Log($"✅ Waypoint Finalization Complete. Total unique waypoints assigned: {waypoints.Count}");
    }

    public int GetWaypointCount() => waypoints.Count;
}




