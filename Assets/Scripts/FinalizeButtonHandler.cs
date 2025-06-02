using UnityEngine;
using UnityEngine.UI;

public class FinalizeButtonHandler : MonoBehaviour
{
    public GameObject tracksPlaced;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnFinalizeButtonClick);
    }

    void OnFinalizeButtonClick()
{
    if (tracksPlaced != null)
    {
        WaypointAssigner assigner = tracksPlaced.GetComponent<WaypointAssigner>() ?? tracksPlaced.AddComponent<WaypointAssigner>();
        assigner.tracksPlaced = tracksPlaced; // <- THIS LINE FIXES IT

        assigner.FinalizeWaypoints();

        Debug.Log("Waypoint count after finalization: " + assigner.GetWaypointCount());

        KartLapCounter kartCounter = Object.FindFirstObjectByType<KartLapCounter>();
        if (kartCounter != null)
        {
            kartCounter.numberOfWaypoints = assigner.GetWaypointCount();
            Debug.Log($"KartLapCounter updated with {kartCounter.numberOfWaypoints} waypoints.");
        }
    }
    else
    {
        Debug.LogWarning("tracksPlaced GameObject not assigned.");
    }
}

}
