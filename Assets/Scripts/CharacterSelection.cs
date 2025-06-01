using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public string characterName;
    public string characterDrivingPrefabName;

    public AnimatorController startingAnimatorController; 

    public void OnCharacterSelect()
    {
        CharacterSelectionManager.startingCharacterAnimationPrefabname = characterDrivingPrefabName;

        CharacterSelectionManager.selectedCharacterName = characterName;

        CharacterSelectionManager.startingAnimatorController = startingAnimatorController;

        // Then play the Game
        SceneManager.LoadScene("SampleScene");
    }
}
