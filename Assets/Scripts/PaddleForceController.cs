using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Valve.VR.InteractionSystem;

public class PaddleForceController : MonoBehaviour
{
    public VelocityEstimator ve;
    [SerializeField] private float forceMultiplier = 0.2f;
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
            float velocityMultiplier = ve.GetVelocityEstimate().magnitude;
            // Vector3 hitDirection = ve.GetVelocityEstimate();
            Vector3 hitDirection = new Vector3(1, 0, 0);

            //normal vector of collision
            Vector3 collisionNormal = collision.contacts[0].normal;
            //calculate the rotation force depending on the racket speed and normal vector of collision
            // Vector3 rotationForce = Vector3.Cross(hitDirection, collisionNormal) * rotationMultiplier;
            Vector3 rotationForce = hitDirection * rotationMultiplier*velocityMultiplier;

            // Debug.Log(rotationForce);
            ballRb.AddForce(hitDirection * forceMultiplier, ForceMode.Impulse);
            ballRb.AddTorque(rotationForce, ForceMode.Impulse);
        }
    }
}
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using Valve.VR.InteractionSystem;

// public class PaddleForceController : MonoBehaviour
// {
//     public VelocityEstimator ve;
//     [SerializeField] private float forceMultiplier = 0.03f;
//     [SerializeField] private float rotationMultiplier = 10000f;
//     [SerializeField] private float velocityContribution = 0.2f; // 速度方向の寄与の割合
//     [SerializeField] private Vector3 forwardDirection = Vector3.forward; // 指定した正面方向
//     private Rigidbody rb;

//     private void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//     }

//     private void OnCollisionEnter(Collision collision)
//     {
//         if (collision.gameObject.CompareTag("Ball"))
//         {
//             Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
//             Vector3 hitDirection = ve.GetVelocityEstimate().normalized;

//             // 衝突した面の法線ベクトル
//             Vector3 collisionNormal = collision.contacts[0].normal;

//             // 速度方向と法線ベクトルの合成ベクトルを求める
//             Vector3 combinedDirection = (hitDirection * (1 - velocityContribution) + collisionNormal * velocityContribution).normalized;

//             // 合成ベクトルに対して直線的な力を加える
//             ballRb.AddForce(combinedDirection * forceMultiplier, ForceMode.Impulse);

//             // 速度方向の寄与を抑えつつ回転力を計算
//             Vector3 rotationForce = Vector3.Cross(forwardDirection, combinedDirection) * rotationMultiplier;

//             Debug.Log(rotationForce);
//             ballRb.AddTorque(rotationForce, ForceMode.Impulse);
//         }
//     }
// }
