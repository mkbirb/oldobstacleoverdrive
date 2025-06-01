using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    // The Character Categorization Gameobject
    public GameObject characterGategory;

    // The Camera that is following the Kart
    public Camera kartCamera;

    void Start()
    {
        string characterName = CharacterSelectionManager.selectedCharacterName;

        string prefabName = CharacterSelectionManager.startingCharacterAnimationPrefabname;

        if (characterName == "You")
        {
            // This provides a full VR Experience, where the Player is the one driving the Kart
            // So set the Camera to be on the Kart
            kartCamera.transform.localPosition = new Vector3(0.323f, 0.2569999f, 0.272f);
        }
        else
        {
            // Make the Character go behind the Kart, if a Character is selected, to see the Character Player has chosen.
            // Make sure that the Kart Camera is a Child Object to Kart, so this is done in Local Position:
            kartCamera.transform.localPosition = new Vector3(0.804f, 0.2569999f, 0.491f);

            // Find the desired prefab, which would be the Driving Animation in this case
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/Characters/{characterName}/{prefabName}");

            if (prefab != null)
            {
                GameObject characterSpawned = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation, characterGategory.transform);

                Animator animator = characterSpawned.GetComponent<Animator>();

                if (animator != null && CharacterSelectionManager.startingAnimatorController != null)
                {
                    animator.runtimeAnimatorController = CharacterSelectionManager.startingAnimatorController;
                }
                else
                {
                    Debug.LogWarning("CharacterSpawner: Animator or AnimatorController is missing!");
                }

                // Adjust the Character so it aligns and sits on the Kart
                characterSpawned.transform.rotation = Quaternion.Euler(-1.368f, -288.313f, 0.139f);
                characterSpawned.transform.localScale = new Vector3(0.4182899f, 0.2901165f, 0.2140791f);
                characterSpawned.transform.localPosition = new Vector3(-0.008731713f, -0.2470086f, 0.01811911f);

            }
            else
            {
                Debug.LogError($"CharacterSpawner: Prefab '{prefabName}' not found in Resources!");
            }
        }
    }

}