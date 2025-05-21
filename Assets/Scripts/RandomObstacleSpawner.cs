// Randomly spawns an object around the Tracks placed

using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    // Category Gameobject
    public GameObject tracksPlaced;
    public int obstacleCount = 5;
    
    // Category Gameobject
    public GameObject obstacleSpawned;

    public void SpawnObstacles()
    {
        int totalTracks = tracksPlaced.transform.childCount;

        if (totalTracks == 0)
        {
            Debug.LogWarning("ObstacleSpawner: No tracks to put obstacle in");
            return;
        }

        for (int i = 0; i < obstacleCount; i++)
        {
            // Select a Random Track to put the obstacle on
            int randomIndex = Random.Range(0, totalTracks);
            int randomObstacleIndex = Random.Range(0, obstaclePrefabs.Length);

            Transform track = tracksPlaced.transform.GetChild(randomIndex);
            // Get the Renderers of the Track, so that the obstacle is able to spawn anywhere within a track piece
            Renderer trackRenderer = track.GetComponentInChildren<Renderer>();


            if (trackRenderer == null)
            {
                Debug.LogWarning("ObstacleSpawner: Track has no Renderer to determine bounds.");
                continue;
            }

            Bounds bounds = trackRenderer.bounds;

            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomZ = Random.Range(bounds.min.z, bounds.max.z);
            float yPosition = bounds.max.y + 0.3543816f;

            GameObject newObstacle = obstaclePrefabs[randomObstacleIndex];

            Vector3 spawnPosition = new Vector3(randomX, yPosition, randomZ);

            GameObject createdObstacle = Instantiate(newObstacle, spawnPosition, Quaternion.identity, obstacleSpawned.transform);

            Vector3 yPos = createdObstacle.transform.position;
            if (newObstacle.name.Contains("FallObstacle"))
            {
                createdObstacle.transform.localScale = new Vector3(5.317097f, 4.731523f, 0.7523794f);
                yPos.y = 4.97f;
            }
            else if (newObstacle.name.Contains("ExplodeObstacle"))
            {
                createdObstacle.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                yPos.y = -0.04f;
            }
            else if (newObstacle.name.Contains("MudAssetObject"))
            {
                createdObstacle.transform.localScale = new Vector3(1.714059f, 1.0f, 1.960392f);
                yPos.y = 0.125f;
            }
            else if (newObstacle.name.Contains("BridgeRamp"))
            {
                createdObstacle.transform.localScale = new Vector3(14.78838f, 0.002253056f, 5.422424f);
                yPos.y = 0.05f;
            }

            // Apply Y Position back
            createdObstacle.transform.position = yPos;
        }
        Debug.Log("ObstacleSpawner: Obstacle Spawned");
    }

}