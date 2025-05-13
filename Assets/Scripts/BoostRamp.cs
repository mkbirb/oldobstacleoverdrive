using UnityEngine;

public class BoostRamp: MonoBehaviour {
    public float boostForce = 1000f;
    public float boostDuration = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Do a Forward Force
                Vector3 boostDirection = transform.forward;
                rb.AddForce(boostDirection * boostForce, ForceMode.Impulse);
                Debug.Log("BoostRamp: Did Impulse Force");
            }
        }
    }
}