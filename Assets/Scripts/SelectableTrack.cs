using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableTrack : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("SelectableTrack: Clicked");
        if (DragMenu.activeDragMenu != null)
        {
            DragMenu.activeDragMenu.SelectPiece(gameObject);
        }

    }
}