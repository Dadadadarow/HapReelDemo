/*
    DataReceived イベントを使用した実装、データを受け取ることができないのでポーリング方式に切り替え
    受信処理は別スレッドにする
*/

using System.IO.Ports;
using UnityEngine;
using System.Threading;
using System;

public class SerialHandler : MonoBehaviour
{
    public string portName = "COM8";  // 適切なポート名を設定
    public int baudRate = 115200;
    private SerialPort serialPort;
    private Thread thread;

    public string readMessage;
    private bool isRunning = false;

    private void Start()
    {
        OpenConnection();
        Thread.Sleep(2000);  // 2秒待機
    }

    private void OpenConnection()
    {
        try
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
                Debug.Log("Port closed.");
            }

            serialPort = new SerialPort(portName, baudRate);
            serialPort.Open();
            // serialPort.ReadTimeout = 500;

            thread = new Thread(Read);
            thread.Start();

            isRunning = true;
            Debug.Log("Port opened.");
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
    // private void Update()
    // {
    //     // スペースキーを押すとデータを送信
    //     // if (Input.GetKeyDown(KeyCode.Space))
    //     // {
    //         // 例として、時間に応じて変化する2つのfloat値を送信します
    //         float value1 = Mathf.Sin(Time.time);
    //         float value2 = Mathf.Cos(Time.time);

    //         string message = value1.ToString("F4") + "," + value2.ToString("F4"); // カンマ区切りの文字列
    //         SendData(message);
    //     // }
    // }

    private void Read() 
    {
        while (isRunning) 
        {
            if (serialPort != null && serialPort.IsOpen) 
            {
                try 
                {
                    // if (serialPort.BytesToRead > 0)
                    // {
                    //     string readMessage = serialPort.ReadLine();
                    //     Debug.Log("Received: " + readMessage);
                    // }
                    // Debug.Log("BytesToRead: " + serialPort.BytesToRead);
                    while (serialPort.BytesToRead > 0)
                    {
                        string readMessage = serialPort.ReadLine();
                        Debug.Log("Received: " + readMessage);
                    }
                }
                catch (System.Exception e) 
                {
                    Debug.LogWarning(e.Message);
                }
            }
            Thread.Sleep(10); // CPUを圧迫しないための小休止
        }
    }

    public void SendData(string message)
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.WriteLine(message);
            // Debug.Log("Sent: " + message);
        }
    }

    private void OnApplicationQuit()
    {
        isRunning = false;
        if (thread != null && thread.IsAlive) 
        {
            thread.Join();
            Debug.Log("Thread closed.");
        }

        if (serialPort != null && serialPort.IsOpen) 
        {
            serialPort.Close();
            serialPort.Dispose();
            Debug.Log("Port closed.");
        }
    }
    void OnDestroy()
    {
        isRunning = false;
        if (thread != null && thread.IsAlive) 
        {
            thread.Join();
        }

        if (serialPort != null && serialPort.IsOpen) 
        {
            serialPort.Close();
            serialPort.Dispose();
        }
    }
}