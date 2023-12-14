// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RespawnBalls : MonoBehaviour
// {
//     public GameObject respawnPoint;

//     public GameObject ball;
//     [SerializeField] private float serve_speed = 0.007f;

//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Space))
//         {
//             RespawnBall();
//         }
//     }
//     private void RespawnBall()
//     {
//         Vector3 ballPosition = respawnPoint.transform.position;

//         GameObject newBall = Instantiate(ball, ballPosition, transform.rotation);

//         // サーブ方向をランダムに設定
//         // float randomAngle = Random.Range(-2.0f, 2.0f); // -45度から45度のランダムな角度を生成
//         float randomAngle = 90;
//         // Vector3 serveDirection = Quaternion.Euler(0, randomAngle, 0);
//         Vector3 serveDirection = Quaternion.Euler(0, randomAngle, 0) * Vector3.back;

//         newBall.GetComponent<Rigidbody>().AddForce(serveDirection * serve_speed, ForceMode.Impulse);
//         newBall.name = ball.name;

//         Destroy(newBall, 10f);
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // シーンマネジメントのために追加

public class RespawnBalls : MonoBehaviour
{
    public GameObject respawnPoint;
    public GameObject ball;
    [SerializeField] private float serve_speed = 0.007f;
    [SerializeField] private float respawnInterval = 3f; // ボールが出る間隔

    private float timeSinceLastRespawn = 0f;
    private float totalTimeElapsed = 0f;

    void Update()
    {
        timeSinceLastRespawn += Time.deltaTime;
        totalTimeElapsed += Time.deltaTime;

        if (timeSinceLastRespawn >= respawnInterval)
        {
            RespawnBall();
            timeSinceLastRespawn = 0f; // リセットして次のカウントを開始
        }

        // 60秒経過したらシーンを停止
        if (totalTimeElapsed >= 60f)
        {
            StopScenePlayback();
        }
    }

    private void RespawnBall()
    {
        Vector3 ballPosition = respawnPoint.transform.position;

        GameObject newBall = Instantiate(ball, ballPosition, transform.rotation);

        // サーブ方向をランダムに設定
        float randomAngle = 90;
        Vector3 serveDirection = Quaternion.Euler(0, randomAngle, 0) * Vector3.back;

        newBall.GetComponent<Rigidbody>().AddForce(serveDirection * serve_speed, ForceMode.Impulse);
        newBall.name = ball.name;

        Destroy(newBall, 10f);
    }

    // シーンの再生を停止するメソッド
    private void StopScenePlayback()
    {
        // シーンの再生を停止するためのコード
        // 例えば、以下のように現在のシーンを再読み込みすることでシーンを停止します。
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // または、アプリケーション全体を終了させたい場合は以下のようにします。
        Application.Quit();
    }
}
