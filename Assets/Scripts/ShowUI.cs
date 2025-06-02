using UnityEngine;

public class ShowUI : MonoBehaviour
{
    public GameObject userInterface;

    // The Show Button that displays the User Interface needs to hide
    public GameObject showButton;

    public void DisplayUI()
    {
        Debug.Log("ShowUI: Show the Userface ", userInterface);
        userInterface.SetActive(true);

        if (showButton)
        {
            showButton.SetActive(false);   
        }
    }

    public void HideUI()
    {
        Debug.Log("ShowUI: Hide the Userface ", userInterface);
        userInterface.SetActive(false);

        if (showButton)
        {
            showButton.SetActive(true);   
        }

        // Update the Map World Size for the Minimap, when the Track building has been done
        GetComponent<MinimapIcon>().getMapWorldSize();
    }
}
