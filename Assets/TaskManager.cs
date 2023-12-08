using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public GameObject ball;
    public GameObject racket;
    public Transform respawnLL;
    public Transform respawnL;
    public Transform respawnM;
    public Transform respawnR;
    public Transform respawnRR;
    public bool isTorque = false;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Cキーを押すとConceptモードを開始
        if (Input.GetKeyDown(KeyCode.S))
        {
            // 2秒ごとにランダムな位置にボールを生成する処理を開始し、6回繰り返す
            InvokeRepeating("SpawnRandomBall", 0f, 2f);
            Invoke("ChangeFeedbackMode", 10f); // 6秒後にモードを変更
            Invoke("StopSpawning", 20f); // 12秒後に処理を停止
        }
        //Qキーを押すとQuestionモードを開始
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CanvasFader.Begin (canvas, isFadeOut:false, duration:0.2f);
            // CanvasFader.Begin (target:gameObject, isFadeOut:true, duration:0.2f, ignoreTimeScale:true, onFinished:OnFinished);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopSpawning();
            CanvasFader.Begin (canvas, isFadeOut:true, duration:0.2f);
        }
    }

    // ランダムな位置にボールを生成するメソッド
    void SpawnRandomBall()
    {
        Vector3 randomRespawnPosition = GetRandomRespawnPosition();
        SpawnBall(randomRespawnPosition);
    }

    // ボールを生成して、5秒後に破棄するメソッド
    void SpawnBall(Vector3 position)
    {
        racket.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        GameObject newBall = Instantiate(ball, position, Quaternion.identity);
        Destroy(newBall, 5f);
    }

    // ランダムな位置を取得するメソッド
    Vector3 GetRandomRespawnPosition()
    {
        int randomIndex = Random.Range(0, 4);

        switch (randomIndex)
        {
            case 0:
                return respawnL.position;
            case 1:
                return respawnLL.position;
            case 2:
                return respawnM.position;
            case 3:
                return respawnR.position;
            case 4:
                return respawnRR.position;
            default:
                return respawnL.position;
        }
    }

    // 処理を停止するメソッド
    void StopSpawning()
    {
        CancelInvoke("SpawnRandomBall");
    }

    void ChangeFeedbackMode()
    {
        if (!isTorque)
        {
            isTorque = true;
        }
    }
}
