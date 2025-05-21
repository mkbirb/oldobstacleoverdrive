// Falls onto the Track after a certain amount of time
using System.Collections;
using UnityEngine;

public class ObstacleFall: MonoBehaviour {
    public float slamSpeed = 20f;
    public float returnSpeed = 2f;
    public float downTime = 1.5f;

    private Vector3 startPos;
    // The Position of where the Slamming Down would occur
    private Vector3 slamPos;

    private bool isSlamming = false;
    private bool isReturning = false;

    Quaternion startRot;

    void Start() {
        startPos = transform.position;
        startRot = transform.rotation;
        slamPos = new Vector3(startPos.x, startPos.y - 3.0f, startPos.z - 3.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        KartController kart = other.GetComponent<KartController>();
        if(kart != null) {
            if (!isSlamming && !isReturning) {
                // Only Trigger the Slamming Down, if the Obstacle is not currently slamming or returning
                Debug.Log("ObstacleFall: Starting the Obstacle Fall");
                StartCoroutine(SlamDown());
            }
        }
    }

    private IEnumerator SlamDown() 
    {
        float duration = 3f;
        // Time that is passed so far
        float elapsed = 0f;

        Quaternion endRot = Quaternion.Euler(90f, transform.eulerAngles.y, 0f); 

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRot;

        
        yield return new WaitForSeconds(downTime);

        // Reset the Position
        transform.rotation = startRot;
        transform.position = startPos;
    }
}