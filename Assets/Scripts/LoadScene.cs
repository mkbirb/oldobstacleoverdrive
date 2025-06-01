using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    // Ensure that the options of the Enum has exact same name as respective Scene
    public enum SearchScene
    {
        SampleScene,
        CharacterSelectScreen,
        WelcomeScreen,
    }

    public SearchScene sceneToBeLoaded;

    public void Load()
    {
        string sceneName = sceneToBeLoaded.ToString();
        SceneManager.LoadScene(sceneName);
    }
}