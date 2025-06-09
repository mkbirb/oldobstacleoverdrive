using UnityEngine;

public class CameraModeManager : MonoBehaviour
{
    public GameObject xrOrigin;
    public GameObject editorCamera;

    void Start()
    {
#if UNITY_EDITOR
        xrOrigin.SetActive(false);
        editorCamera.SetActive(true);
#else
        xrOrigin.SetActive(true);
        editorCamera.SetActive(false);
#endif
    }
}
