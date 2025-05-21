using UnityEngine;
using System.IO;

public class ito : MonoBehaviour
{
    public GameObject obstaclePrefab; // Reference to the prefab (the obstacle)
    public string imageFileName = "obstacle.jpeg";  // Use .jpeg here

    void Start()
    {
        // Path to the image file in the StreamingAssets folder
        string path = Path.Combine(Application.streamingAssetsPath, imageFileName);

        // Log the path to check if it’s correct
        Debug.Log("Path to image: " + path);
        
        // Check if the file exists at the given path
        if (File.Exists(path))
        {
            // Read the image data into a byte array
            byte[] imageData = File.ReadAllBytes(path);
            
            // Create a new texture
            Texture2D texture = new Texture2D(1, 1);  // You can initialize it to a very small size or load the image directly.
            
            // Load the image data into the texture
            if (texture.LoadImage(imageData))
            {
                // If the image is loaded successfully, spawn the obstacle with the texture
                SpawnObstacle(texture);
            }
            else
            {
                // Log an error if the image could not be loaded
                Debug.LogError("Failed to load image into texture.");
            }
        }
        else
        {
            // Log an error if the image file does not exist
            Debug.LogError("Image not found at: " + path);
        }
    }

    void SpawnObstacle(Texture2D texture)
    {
        // Create an instance of the obstacle prefab at the specified position
        GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(0, 0.5f, 0), Quaternion.identity);

        // Get the MeshRenderer component of the instantiated object
        MeshRenderer renderer = obstacle.GetComponent<MeshRenderer>();

        if (renderer != null)
        {
            // Create a new material using the instance of the material (not shared material)
            Material mat = new Material(renderer.material); // Use renderer.material to avoid shared material changes
            
            // Assign the loaded texture to the material's main texture
            mat.mainTexture = texture;

            // Apply the material to the obstacle's renderer
            renderer.material = mat;
        }
        else
        {
            Debug.LogError("MeshRenderer not found on the obstacle prefab.");
        }
    }
}
