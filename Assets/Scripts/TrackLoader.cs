using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class TrackLoader : MonoBehaviour
{
    public GameObject MudStraightObject;
    public GameObject BrownCurvedPath;
    public Transform tracksPlaced;

    [System.Serializable]
    public class TrackPieceData
    {
        public string prefabName;
        public Vector3 position;
        public Quaternion rotation;
    }

    [System.Serializable]
    public class TrackDataWrapper
    {
        public List<TrackPieceData> list;
    }

    void Start()
    {
        string path = Application.persistentDataPath + "/track.json";
        if (!File.Exists(path)) return;

        string json = File.ReadAllText(path);
        TrackDataWrapper wrapper = JsonUtility.FromJson<TrackDataWrapper>(json);

        foreach (var data in wrapper.list)
        {
            GameObject prefabToSpawn = null;

            if (data.prefabName.Contains("Straight"))
                prefabToSpawn = MudStraightObject;
            else if (data.prefabName.Contains("Curved"))
                prefabToSpawn = BrownCurvedPath;

            if (prefabToSpawn != null)
            {
                GameObject obj = Instantiate(prefabToSpawn, data.position, data.rotation);
                obj.transform.SetParent(tracksPlaced);
            }
        }

        Debug.Log("Track loaded from saved file.");
    }
}
