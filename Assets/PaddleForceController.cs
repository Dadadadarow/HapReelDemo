using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Valve.VR.InteractionSystem;

public class PaddleForceController : MonoBehaviour
{
    public VelocityEstimator ve;
    [SerializeField] private float forceMultiplier = 0.01f;

    private Vector3 lastPosition;
    // private Vector3 _velocity;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        lastPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 hitDirection = ve.GetVelocityEstimate();
            Debug.Log(ve.GetVelocityEstimate());
            ballRb.AddForce(hitDirection * forceMultiplier, ForceMode.Impulse);
        }
    }
}
