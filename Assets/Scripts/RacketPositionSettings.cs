using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketPositionSettings : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // //オブジェクトの回転をクォータニオンで取得
        // Quaternion rotation = this.transform.rotation;

        // //クォータニオンからオイラー角に変換
        // Vector3 angle = rotation.eulerAngles;

        // //X軸基準に+5°回転
        // angle.x = -angle.x;

        // //オイラー角からクォータニオンに戻してオブジェクトに適用
        // this.transform.rotation = Quaternion.Euler(angle);        
    }
}
