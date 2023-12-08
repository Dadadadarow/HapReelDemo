using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketCollisionMarker : MonoBehaviour
{
    public GameObject markerPrefab; // マークのプレハブ
    private GameObject previousMarker1; // 前回のマーク
    private GameObject previousMarker2; // 前回のマーク


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // 前回のマークが存在する場合は削除
            if (previousMarker1 != null)
            {
                Destroy(previousMarker1);
            }
            if (previousMarker2 != null)
            {
                Destroy(previousMarker2);
            }
            // ボールとの衝突を感知したら新しいマークを付ける
            ContactPoint contactPoint = collision.contacts[0];
            Vector3 collisionPoint = contactPoint.point;
            Vector3 markerPoint1 = new Vector3(collisionPoint.x+.02f, collisionPoint.y, collisionPoint.z);
            Vector3 markerPoint2 = new Vector3(collisionPoint.x-.02f, collisionPoint.y, collisionPoint.z);

            // 新しいマークを生成して記録
            previousMarker1 = CreateMarker(markerPoint1);
            previousMarker2 = CreateMarker(markerPoint2);
        }
    }

    private GameObject CreateMarker(Vector3 position)
    {
        // マークのインスタンスを生成
        GameObject marker = Instantiate(markerPrefab, position, Quaternion.identity);

        // マークをラケットの子オブジェクトにする（ラケットの動きに追従させる）
        marker.transform.parent = transform;

        // ここでマークの見た目やサイズ、表示時間などを調整できます

        return marker; // 生成したマークを返す
    }
}
