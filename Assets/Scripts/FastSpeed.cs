using System.Collections;
using UnityEngine;

public class FastSpeed: MonoBehaviour {
    public float boostForce = 1000f;
    public float boostDuration = 2f;

    public float soundDuration = 2f;

    public AudioSource zoomSound;

    void Awake()
    {
        if (zoomSound == null)
        {
            zoomSound = GetComponent<AudioSource>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Do a Forward Force
                StartCoroutine(playZoomSound());
                Vector3 boostDirection = transform.forward;
                rb.AddForce(boostDirection * boostForce, ForceMode.Impulse);
                Debug.Log("BoostRamp: Did Impulse Force");
            
                
            }
        }
    }

    private IEnumerator playZoomSound() {
        zoomSound.Play();
        yield return new WaitForSeconds(soundDuration);

        zoomSound.Stop();
        
    }
}