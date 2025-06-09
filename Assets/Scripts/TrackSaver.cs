using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class TrackSaver : MonoBehaviour
{
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

    public void SaveTrackLayout()
    {
        List<TrackPieceData> trackData = new List<TrackPieceData>();

        foreach (Transform child in tracksPlaced)
        {
            TrackPieceData data = new TrackPieceData
            {
                prefabName = child.name.Replace("(Clone)", "").Trim(),
                position = child.position,
                rotation = child.rotation
            };

            trackData.Add(data);
        }

        string json = JsonUtility.ToJson(new TrackDataWrapper { list = trackData }, true);
        string path = Application.persistentDataPath + "/track.json";

        File.WriteAllText(path, json);
        Debug.Log($"Track layout saved to: {path}");
    }
}
