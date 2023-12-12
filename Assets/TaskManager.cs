using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class TaskManager : MonoBehaviour
{
    // stage setting
    public GameObject ball;
    public GameObject racket;
    public Transform respawnLL;
    public Transform respawnL;
    public Transform respawnM;
    public Transform respawnR;
    public Transform respawnRR;
    // task setting
    GameObject canvas;
    // GameObject panel;
    public Fade panel;
    TextMeshProUGUI text1;
    int state = 0;
    // TextPosition tp;

    string strIntro1 = "Vibration";
    string strIntro2 = "Vibration + Torque";
    string strGuess = "Can you guess the collision point?";
    string strAns = "The collision point is...\n(Select 1-5 key)";
    string strConf = "How confident are you in your answers?\n(Select 1-7 key)";
    int taskCount = 1;
    int randomIndex;
    bool isConceptStarted = false;
    bool isQuestionStarted = false;
    public bool isTorque = false;
    static LogSave csv = null; //staticにすると複数回呼び出された時に初期化されない、、、？？？

    // Start is called before the first frame update
    void Start()
    {
        if (csv == null)
        {
            csv = new LogSave();
        }
        //Canvasを取得してcanvasに代入
        canvas = GameObject.Find("Canvas");
        text1 = canvas.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown())
        // {
        //     state += 1;
        //     switch(state)
        //     {
        //         case 1:
        //             QuestionStart();
        //     }
        // }
        
        switch(state)
        {
            case 0:
                if (!isConceptStarted)
                {
                    panel.FadeIn(1f);
                    StartCoroutine(ConceptMode());
                    isConceptStarted = true;
                }
                break;
            case 1:
                if (!isQuestionStarted)
                {
                    StartCoroutine(QuestionMode());
                    isQuestionStarted = true;
                }
                break;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #elif UNITY_STANDALONE
                UnityEngine.Application.Quit();
            #endif
        }
    }

    IEnumerator ConceptMode()
    {
        // 【Conceptモード】
        // 2秒ごとにランダムな位置にボールを生成する処理を開始し、10回繰り返す
        // panel.FadeIn(1f);
        // yield return new WaitForSeconds(1.5f);
        yield return new WaitForSeconds(2f);
        canvas.SetActive(true);
        text1.SetText(strIntro1);
        for (int i = 0; i < 5; i++)
        {
            SpawnRandomBall(false, i);
            yield return new WaitForSeconds(2f);
        }
        ChangeFeedbackMode();
        text1.SetText(strIntro2);
        for (int i = 0; i < 5; i++)
        {
            SpawnRandomBall(false, i);
            yield return new WaitForSeconds(2f);
        }
        state++;
    }

    IEnumerator QuestionMode()
    {
        int correctCount = 0;
        panel.FadeOut(1f);
        text1.SetText(strGuess);
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(CountDown(3));
            yield return new WaitForSeconds(3f);
            SpawnRandomBall(true, 0);

            // 位置の回答
            int correctIndex = randomIndex+1;
            int selectedAnswerPos = -1;
            bool validInput = false;

            text1.SetText(strAns);
            while (!Input.GetKeyDown(KeyCode.Return) || selectedAnswerPos == -1)
            {
                for (int j = 1; j <= 5; j++)
                {
                    if (Input.GetKeyDown(j.ToString()))
                    {
                        selectedAnswerPos = j;
                        text1.SetText("Your choise is " + j.ToString() + "\nEnter to next");
                        validInput = true;
                        break;
                    }
                }
                yield return null;
            }

            //自信度
            text1.SetText(strConf);
            validInput = false;
            int selectedAnswerConf = -1;
            while (!Input.GetKeyDown(KeyCode.Return) || selectedAnswerConf == -1)
            {
                Debug.Log("here5");

                for (int j = 1; j <= 7; j++)
                {
                    if (Input.GetKeyDown(j.ToString()))
                    {
                        selectedAnswerConf = j;
                        text1.SetText("Your choise is " + j.ToString() + "\nEnter to next");
                        validInput = true;
                        break;
                    }
                }
                yield return null;
            }

            if (correctIndex == selectedAnswerPos)
            {
                correctCount++;
                // Debug.Log();
            }

            // 回答のCSVへの蓄積
            csv.logSave("," + correctIndex + "," + selectedAnswerPos + "," + selectedAnswerConf);
        }
        text1.SetText("Your accuracy is " + ((correctCount/5)*100).ToString() + "%");
    }

    IEnumerator CountDown(int sec)
    {
        for (int i = 0; i < sec; i++)
        {
            text1.SetText((sec-i).ToString());
            yield return new WaitForSeconds(1f);
        }
    }
    // ランダムな位置にボールを生成するメソッド
    void SpawnRandomBall(bool randomFlag, int spawnPosition)
    {
        Vector3 randomRespawnPosition = GetRandomRespawnPosition(randomFlag, spawnPosition);
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
    Vector3 GetRandomRespawnPosition(bool randomFlag, int spawnPosition)
    {
        if (randomFlag)
            randomIndex = Random.Range(0, 4);
        else
            randomIndex = spawnPosition;

        switch (randomIndex)
        {
            case 0:
                return respawnLL.position;
            case 1:
                return respawnL.position;
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

    void ChangeFeedbackMode()
    {
        if (!isTorque)
        {
            isTorque = true;
        }
    }
}

public class LogSave
{
    string filename;
    FileInfo fi;

    public LogSave()
    {
        System.DateTime thisDay = System.DateTime.Now;
        string day = //thisDay.Year.ToString() +
            thisDay.Month.ToString() +
            thisDay.Day.ToString() +
            "_" +
            thisDay.Hour.ToString() +
            thisDay.Minute.ToString();
        //filename = @"C:/Users/yuki/Documents/FileName" + day + ".csv";
        //↑このように絶対パスでフォルダ位置を指定することもできる
        filename = "test" + day + ".csv";
        //↑Assetsと同じフォルダに保存される。
        //ProjectウィンドウからAssetsを右クリックしShowInExplorerを選ぶとフォルダが開ける

        fi = new FileInfo(filename);
        //fi = new FileInfo(Application.dataPath + "../FileName.csv");

        using (var sw = new StreamWriter(
                fi.Create(),
                System.Text.Encoding.UTF8))
        {
            sw.WriteLine("," + "SelectedAnswerPos" +
                            "," + "CorrectAnswer" +
                            "," + "SelectedAnswerConfident");
        }
    }

    public void logSave(string txt)
    {
        //作ったファイルに追加で書き込む場合はAppendText
        using (StreamWriter sw = fi.AppendText())
        {
            sw.WriteLine(txt);
        }
    }
}