using UnityEngine;

public class MinimapIcon : MonoBehaviour
{
    public Transform player;

    public RectTransform minimapUI;

    public RectTransform playerIcon;

    public GameObject tracksPlaced;

    private Vector2 minimapUISize;
    private Vector2 mapWorldSize;

    private Bounds worldBounds;

    void Start()
    {
        getMapWorldSize();
        minimapUISize = minimapUI.rect.size;
    }

    void Update()
    {

        PlayerPositionInMinimap();
        // Debug.Log("Minimap UI Size: " + minimapUISize);
    }

    private void PlayerPositionInMinimap()
    {
        Vector3 playerPosition = player.position;

        // Normalize the Position
        float normalizedX = (playerPosition.x - worldBounds.min.x) / mapWorldSize.x;
        float normalizedZ = (playerPosition.z - worldBounds.min.z) / mapWorldSize.y;

        // Convert to UI Space
        float uiX = normalizedX * minimapUISize.x;
        float uiY = normalizedZ * minimapUISize.y;

        playerIcon.anchoredPosition = new Vector2(
            uiX - (minimapUISize.x / 2f),
            uiY - (minimapUISize.y / 2f));

        // Debug.Log($"Player Pos: {player.position}, Min: {worldBounds.min}, Size: {mapWorldSize}");
        // Debug.Log($"UI Pos: {playerIcon.anchoredPosition}");
    }

    // Checks the Width and Length of the Track
    public void getMapWorldSize()
    {
        // Start at center of Tracks placed
        Bounds combinedBounds = new Bounds(tracksPlaced.transform.position, Vector3.zero);

        foreach (Renderer renderer in tracksPlaced.GetComponentsInChildren<Renderer>())
        {
            // Grow the Bounds so captures all of the tracks placed
            combinedBounds.Encapsulate(renderer.bounds);
        }

        worldBounds = combinedBounds;

        mapWorldSize = new Vector2(combinedBounds.size.x, combinedBounds.size.z);
    }
}
