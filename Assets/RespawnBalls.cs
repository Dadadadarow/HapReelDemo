using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBalls : MonoBehaviour
{
    public GameObject respawnPoint;

    public GameObject ball;
    [SerializeField] private float serve_speed = 0.007f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RespawnBall();
        }
    }
    private void RespawnBall()
    {
        Vector3 ballPosition = respawnPoint.transform.position;

        GameObject newBall = Instantiate(ball, ballPosition, transform.rotation);

        // サーブ方向をランダムに設定
        // float randomAngle = Random.Range(-2.0f, 2.0f); // -45度から45度のランダムな角度を生成
        float randomAngle = 90;
        // Vector3 serveDirection = Quaternion.Euler(0, randomAngle, 0);
        Vector3 serveDirection = Quaternion.Euler(0, randomAngle, 0) * Vector3.back;

        newBall.GetComponent<Rigidbody>().AddForce(serveDirection * serve_speed, ForceMode.Impulse);
        newBall.name = ball.name;

        Destroy(newBall, 10f);
    }
}