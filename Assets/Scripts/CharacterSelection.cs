using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public string characterName;
    public string characterDrivingPrefabName;

    public AnimatorController startingAnimatorController;

    public AudioSource sound;

    public float soundDuration = 1.0f;


    public void OnCharacterSelect()
    {
        CharacterSelectionManager.startingCharacterAnimationPrefabname = characterDrivingPrefabName;

        CharacterSelectionManager.selectedCharacterName = characterName;

        CharacterSelectionManager.startingAnimatorController = startingAnimatorController;

        // Play Character Select Sound
        StartCoroutine(playSound());

    }


    public IEnumerator playSound()
    {
        sound.Play();
        Debug.Log("PlaySound: Sound is played ", sound);
        yield return new WaitForSeconds(soundDuration);
        Debug.Log("PlaySound: Sound is stopped ", sound);
        sound.Stop();

        // Then play the Game
        SceneManager.LoadScene("SampleScene");
    }
}
