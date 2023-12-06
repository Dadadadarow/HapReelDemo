using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSender : MonoBehaviour
{
    public SerialHandler serialHandler;

    [System.Serializable]
    public struct MotorControlPacket
    {       
        public int   direction;  // 2つのモータで提示する力覚の方向を指示 0:STOP, 1:FORTH, 2:BACK, 3:LEFT, 4:RIGHT

        public float returntime; // モータを戻す時間
        public float M;          // モータの最大回転角度[度]（トルク量）
        public float freq;       // モータの周波数(未使用)
        public float A;          // 減衰振動の振幅（衝撃力の大きさ）
        public float B;          // 減衰振動の減衰率（モータを戻す時間により変化）
    }

    public MotorControlPacket motorControlPacket;

    int[] directions = new int[] {0, 1, 2, 3, 4}; // 2つのモータで提示する力覚の方向を指示 0:STOP, 1:FORTH, 2:BACK, 3:LEFT, 4:RIGHT
    int clickCount = 0;

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     SendMotorControlPacket(directions[clickCount], 0.2f, 10, 0.0f, 50f, 10.0f);
            
        //     if(clickCount < 4)  clickCount++;
        //     else clickCount = 0;
        // }
    }

    public void SendMotorControlPacket(int direction, float returntime, float M, float freq, float A, float B)
    {
        motorControlPacket.direction = direction;
        motorControlPacket.returntime = returntime;
        motorControlPacket.M = M;
        motorControlPacket.freq = freq;
        motorControlPacket.A = A;
        motorControlPacket.B = B;

        string message = motorControlPacket.direction.ToString() + "," + motorControlPacket.returntime.ToString() + "," + motorControlPacket.M.ToString() + "," + motorControlPacket.freq.ToString() + "," + motorControlPacket.A.ToString() + "," + motorControlPacket.B.ToString();
        serialHandler.SendData(message);
        // Debug.Log(message);
    }
}