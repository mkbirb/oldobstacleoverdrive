using UnityEngine;
using UnityEngine.EventSystems;

public class DragMenu : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject prefabCreate;
    private GameObject currentPiece;

    // For the categorisation of the Hierachy
    public GameObject tracksPlaced;

    public Camera birdsEyeCamera;

    public enum TrackType
    {
        Straight,
        Curved,
    }

    public TrackType selectedTrackType;

    public static DragMenu activeDragMenu; 


    public GameObject straightDragMenu;
    public GameObject curvedDragMenu;

    private static GameObject globallySelectedPiece;
    private static Color globallyOriginalColor;

    private bool isDraggingSelectedPiece = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = birdsEyeCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                if (clickedObject.CompareTag("Track")) 
                {
                    SelectPiece(clickedObject);
                    isDraggingSelectedPiece = true;
                }
                else
                {
                    isDraggingSelectedPiece = false;
                }
            }
        }

        // While holding mouse and a piece is selected & green
        if (Input.GetMouseButton(0) && isDraggingSelectedPiece && globallySelectedPiece != null)
        {
            Renderer renderer = globallySelectedPiece.GetComponentInChildren<Renderer>();
            if (renderer != null && renderer.material.color == Color.green)
            {
                Ray ray = birdsEyeCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    globallySelectedPiece.transform.position = SnapToGrid(hit.point, 10.0f);
                }
            }
        }

        // On release stop dragging
        if (Input.GetMouseButtonUp(0))
        {
            isDraggingSelectedPiece = false;
        }
    }

    public void OnSelectStraight()
    {
        curvedDragMenu.SetActive(false);
        straightDragMenu.SetActive(true);
    }

    public void OnSelectCurved()
    {
        straightDragMenu.SetActive(false);
        curvedDragMenu.SetActive(true);
    }

    // Allows the decision on which DragMenu to use for BuilderOptions
    void OnEnable()
    {
        activeDragMenu = this;
    }

    void OnDisable()
    {
        if (activeDragMenu == this)
            activeDragMenu = null;
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        // Create a new prefab
        Debug.Log($"DragMenu: On Begin Drag, current piece deselecting is {currentPiece}");

        // Deselect Piece before selecting piece
        // DeselectPiece(currentPiece);

        currentPiece = Instantiate(prefabCreate, tracksPlaced.transform);

        // Indicate the Current Piece Selected
        SelectPiece(currentPiece);

        if (selectedTrackType == TrackType.Straight)
        {
            currentPiece.transform.localScale = new Vector3(80.20f, 0.002253056f, 97.0f);
        }
        else if (selectedTrackType == TrackType.Curved)
        {
            currentPiece.transform.localScale = new Vector3(280.0586f, 0.002253056f, 304.3666f);
        }
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        Ray ray = birdsEyeCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Debug.Log("Ray hit: " + hit.collider.gameObject.name);
            // Get the Current Piece to appear where the Mouse is at
            currentPiece.transform.position = SnapToGrid(hit.point, 10.0f);
        }
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
    
    }

    public static Vector3 SnapToGrid(Vector3 position, float gridSize)
    {
        return new Vector3(
            Mathf.Round(position.x / gridSize) * gridSize,
            position.y,
            Mathf.Round(position.z / gridSize) * gridSize
        );
    }

    public void SelectPiece(GameObject piece)
    {
        // Deselect previous global piece
        if (globallySelectedPiece != null && globallySelectedPiece != piece)
        {
            Renderer oldRenderer = globallySelectedPiece.GetComponentInChildren<Renderer>();
            if (oldRenderer != null)
            {
                oldRenderer.material.color = globallyOriginalColor;
            }
        }

        Renderer renderer = piece.GetComponentInChildren<Renderer>();
        Debug.Log($"DragMenu: Selected Piece Renderer is {renderer}" );
        if (renderer != null)
        {
            Debug.Log("DragMenu: Selected Piece");
            // Store the Original Colour
            if (globallySelectedPiece != piece)
            {
                globallyOriginalColor = renderer.material.color;
            }
            renderer.material.color = Color.green;
            globallySelectedPiece = piece;
        }
    }

    public GameObject getCurrentPiece()
    {
        return globallySelectedPiece;
    }

    public Color getGloballyOriginalColor()
    {
        return globallyOriginalColor;
    }



    public void setCurrentPieceRotation(float newRotation)
    {
        currentPiece.transform.rotation = Quaternion.Euler(0, newRotation, 0);
    }
    
}
