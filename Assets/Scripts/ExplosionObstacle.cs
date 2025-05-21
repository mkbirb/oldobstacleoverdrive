using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionObstacle : MonoBehaviour
{

    public GameObject explosionEffect;
    public float explosionForce = 500f;
    public float explosionRadius = 5f;

    public AudioSource bombExplosion;

    private Vector3 startPosition;
    private GameObject explosionObstacle;

    private GameObject explosionObject;

    private void Start()
    {
        startPosition = transform.position;
        explosionObstacle = gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // The Explosion Effect is spawned

        if (explosionEffect != null) {
            explosionObject = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Gather all of the Colliders
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders) {
            // Check if it has Rigidbody
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb!= null) {
                // Only apply the Explosion force to those with the Rigidbody.
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                bombExplosion.Play();
                Debug.Log("ExplosionObstacle: Explosion Applied");
            }
        }

        StartCoroutine(CreateNewExploder());
    }

    private IEnumerator CreateNewExploder() {
        // Hide the Explosion
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(3f);
        bombExplosion.Stop();

        // Now create a new Gameobject that would also explode
        GameObject newObstacle = Instantiate(explosionObstacle);

        newObstacle.transform.position = startPosition;

        Destroy(explosionObject);

        Debug.Log("ExplosionObstacle: New Explosion Obstacle Created");

        newObstacle.GetComponent<Renderer>().enabled = true;
        newObstacle.GetComponent<Collider>().enabled = true;

        Destroy(gameObject);
    }
}
