using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartController : MonoBehaviour
{
    public float acceleration = 15f;
    
    // How fast the Kart would be able to turn
    public float steering = 1.0f;
    public float maxSpeed = 20f;

    // Holds the Players Input
    private Rigidbody rb;
    
    // For forwards and backwards
    private float moveInput;

    // For going to the sides
    private float steerInput;

    // Controls the speed of the Kart
    private float speedMultiplier = 1f;

    public float becomeUprightIn = 3f;

    private bool isFlipping = false;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        // Movement
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");

        // Moving forward
        if (rb.linearVelocity.magnitude < maxSpeed) {
            // Where it is Negative Right, as the Model not imported right
            rb.AddForce(-transform.right * moveInput * acceleration * speedMultiplier, ForceMode.Acceleration);
        }

        // Steering
        if (rb.linearVelocity.magnitude > 0.1f) {
            // Provides sharper turns
            float velocityFactor = Mathf.Clamp(rb.linearVelocity.magnitude, 0.5f, 2f);

            float turn = steerInput * steering * Time.deltaTime * velocityFactor;
            
            // Apply the Rotation
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

            rb.MoveRotation(rb.rotation * turnRotation);
        }


        // Get the Kart to be upright, for when it is upside down
        if (Vector3.Dot(transform.up, Vector3.up) < -0.7f && !isFlipping) {
            StartCoroutine(BecomeUpright());
        }
    }

    private IEnumerator BecomeUpright() {
        isFlipping = true;

        yield return new WaitForSeconds(becomeUprightIn);

        Debug.Log("KartController: Became Upright");

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f);

        isFlipping = false;
    }

    public void SetSpeedMultiplier(float multiplier) {
        speedMultiplier = multiplier;
    }
}
