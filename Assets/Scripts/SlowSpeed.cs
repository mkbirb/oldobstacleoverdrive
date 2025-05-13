// Slows the Kart down a certain speed

using UnityEngine;

public class SlowSpeed: MonoBehaviour {
    public float slowMultiplier = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        KartController kart = other.GetComponent<KartController>();

        if (kart != null) {
            kart.SetSpeedMultiplier(slowMultiplier);
            Debug.Log("SlowSpeed: Speed slowed down");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset back to Normal Speed.
        KartController kart = other.GetComponent<KartController>();

        if (kart != null) {
            kart.SetSpeedMultiplier(1.0f);
            Debug.Log("SlowSpeed: Speed back to Normal");
        }
    }
}