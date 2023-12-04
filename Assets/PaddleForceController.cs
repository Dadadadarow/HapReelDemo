using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleForceController : MonoBehaviour
{
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
            // Vector3 hitDirection = _velocity.normalized;
            Vector3 hitDirection = (collision.transform.position - lastPosition).normalized;
            ballRb.AddForce(hitDirection * forceMultiplier, ForceMode.Impulse);
        }
    }
    // public void OnVelocity(InputValue value)
    // {
    //     _velocity = value.Get<Vector3>();
    //     Debug.Log(_velocity);
    // }
}
