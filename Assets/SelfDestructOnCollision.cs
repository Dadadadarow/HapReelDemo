using UnityEngine;

public class SelfDestructOnCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // 衝突した瞬間に自身を破棄
        Destroy(gameObject);
    }
}