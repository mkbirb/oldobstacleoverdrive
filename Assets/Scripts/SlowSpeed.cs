// Slows the Kart down a certain speed

using System.Collections;
using UnityEngine;

public class SlowSpeed: MonoBehaviour {
    public float slowMultiplier = 0.5f;

    public float slowDuration = 1f;

    private void OnTriggerEnter(Collider other)
    {
        KartController kart = other.GetComponent<KartController>();

        if (kart != null) {
            kart.SetSpeedMultiplier(slowMultiplier);
            StartCoroutine(SlowForSeconds(kart, 2f));
        }
    }

    private IEnumerator SlowForSeconds(KartController kart, float duration)
    {
        // Decreases the Kart Speed
        kart.SetSpeedMultiplier(slowMultiplier);
        Debug.Log("SlowSpeed: Speed slowed down");
        yield return new WaitForSeconds(duration);
        kart.SetSpeedMultiplier(1.0f);
        Debug.Log("SlowSpeed: Speed back to Normal");
    }
}