// For the Game Menu actions

using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public void QuitGame() {
        Application.Quit();
        Debug.Log("GameMenu: Game Quit");
    }
}