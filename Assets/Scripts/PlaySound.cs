using System.Collections;
using UnityEngine;

public class PlaySound: MonoBehaviour {

    public AudioSource sound;

    public float duration = 1.0f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            StartCoroutine(playSound());
        }
    }

    public IEnumerator playSound() {
        sound.Play();
        Debug.Log("PlaySound: Sound is played ", sound);
        yield return new WaitForSeconds(duration);
        sound.Stop();
    }


}