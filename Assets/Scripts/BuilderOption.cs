using UnityEngine;

public class BuilderOption : MonoBehaviour
{
    
    public void RotateCurrentPiece()
    {

        var activeMenu = DragMenu.activeDragMenu;

        Debug.Log($"BuilderOption: You have clicked Rotation, active menu is {activeMenu}");

        // Allows for fixed rotation and snapping.
        if (activeMenu != null)
        {
            GameObject piece = activeMenu.getCurrentPiece();

            if (piece != null)
            {
                float newRotation = (piece.transform.eulerAngles.y + 90f) % 360f;
                piece.transform.rotation = Quaternion.Euler(0f, newRotation, 0f);
                Debug.Log("BuilderOption: Rotated to " + newRotation);
            }
        }
    }

    public void DeleteCurrentPiece()
    {

        var activeMenu = DragMenu.activeDragMenu;

        GameObject piece = activeMenu.getCurrentPiece();

        Destroy(piece);
        Debug.Log("BuilderOption: Piece Deleted");

    }
}