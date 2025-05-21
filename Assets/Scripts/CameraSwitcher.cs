using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera kartCamera;
    public Camera birdsEyeCamera;

    public void ToggleBirdsEye()
    {
        birdsEyeCamera.gameObject.SetActive(true);
        kartCamera.gameObject.SetActive(false);
    }

    public void ToggleKartCamera()
    {
        birdsEyeCamera.gameObject.SetActive(false);
        kartCamera.gameObject.SetActive(true);
    }
}