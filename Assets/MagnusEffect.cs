using UnityEngine;

public class MagnusEffect : MonoBehaviour
{
    public float magnusCoefficient = 0.0001f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;
        Vector3 angularVelocity = rb.angularVelocity;
        Vector3 magnusForce = Vector3.Cross(angularVelocity, velocity) * magnusCoefficient;

        rb.AddForce(magnusForce);
    }
}
