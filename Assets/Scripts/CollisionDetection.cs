using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public DataSender dataSender;
    void OnCollisionEnter(Collision collision)
    {
        // Box Collider の表面上の中心点を取得
        Vector3 center = GetComponent<BoxCollider>().bounds.center;
        
        // 衝突点を取得
        Vector3 collisionPoint = collision.contacts[0].point;

        // 衝突点と中心点の距離を計算
        float distance = Vector3.Distance(center, collisionPoint);


        // 例: 距離に応じて値を決定し、送信する
        Vector3 localCollisionPoint = transform.InverseTransformPoint(collisionPoint);
        // Debug.Log(localCollisionPoint.x);
        // racket position >>> left |0|1|2|3|4| right <<<

        // 2つのモータで提示する力覚の方向を指示 0:STOP, 1:FORTH, 2:BACK, 3:LEFT, 4:RIGHT
        if (-0.1f <= localCollisionPoint.x && localCollisionPoint.x < -0.06)
        {
            dataSender.SendMotorControlPacket(3,0.2f, 10, 0.0f, 50f, 10.0f);
            Debug.Log("Detect 0");
        }
        else if (-0.06f <= localCollisionPoint.x && localCollisionPoint.x < -0.02f)
        {
            dataSender.SendMotorControlPacket(3, 0.2f, 10, 0.0f, 50f, 10.0f);
            Debug.Log("Detect 1");
        }
        else if (-0.02f <= localCollisionPoint.x && localCollisionPoint.x < 0.02f)
        {
            dataSender.SendMotorControlPacket(2, 0.2f, 10, 0.0f, 50f, 10.0f);
            Debug.Log("Detect 2");
        }
        else if (0.02f <= localCollisionPoint.x && localCollisionPoint.x < 0.06f)
        {
            dataSender.SendMotorControlPacket(4, 0.2f, 10, 0.0f, 50f, 10.0f);
            Debug.Log("Detect 3");
        }
        else if (0.06f <= localCollisionPoint.x && localCollisionPoint.x <= 0.1f)
        {
            dataSender.SendMotorControlPacket(4, 0.2f, 10, 0.0f, 50f, 10.0f);
            Debug.Log("Detect 4");
        }
    }
}
