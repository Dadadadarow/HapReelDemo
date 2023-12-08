using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Valve.VR.InteractionSystem;

public class PaddleForceController : MonoBehaviour
{
    public VelocityEstimator ve;
    [SerializeField] private float forceMultiplier = 0.03f;
    [SerializeField] private float rotationMultiplier = 10000f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 hitDirection = ve.GetVelocityEstimate();

            //normal vector of collision
            Vector3 collisionNormal = collision.contacts[0].normal;
            //calculate the rotation force depending on the racket speed and normal vector of collision
            Vector3 rotationForce = Vector3.Cross(hitDirection, collisionNormal) * rotationMultiplier;

            Debug.Log(rotationForce);
            ballRb.AddForce(hitDirection * forceMultiplier, ForceMode.Impulse);
            ballRb.AddTorque(rotationForce, ForceMode.Impulse);
        }
    }
}
