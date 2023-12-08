using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    public AudioClip normalHitSE;
    public AudioClip smashHitSE;
    [Range(0f, 1f)] public float normalHitVolume = 0.2f;
    [Range(0f, 1f)] public float smashHitVolume = 0.2f;

    void Start()
    {
    }

    void Update()
    {
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Racket"))
        {
            BoxCollider boxCollider = col.gameObject.GetComponent<BoxCollider>();
            Vector3 collisionPoint = col.contacts[0].point;
            Vector3 paddleCenter = col.transform.TransformPoint(boxCollider.center);

            float distanceFromCenter = Vector3.Distance(collisionPoint, paddleCenter);

            // ラケットの中央部分との距離のしきい値を設定
            float centerThreshold = 0.02f;

            if (distanceFromCenter <= centerThreshold)
            {
                AudioSource.PlayClipAtPoint(smashHitSE, transform.position, smashHitVolume);
            }
        }
        AudioSource.PlayClipAtPoint(normalHitSE, transform.position, normalHitVolume);
    }
}
