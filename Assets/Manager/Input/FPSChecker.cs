using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <Summary>
/// シーンのフレームレートを計測して画面に表示するスクリプトです。
/// </Summary>
public class FPSChecker : MonoBehaviour
{
    // フレームレートを表示するテキストです。
    public TextMeshProUGUI fpsText;

    // Update()が呼ばれた回数をカウントします。
    int frameCount;
    int fixedCount;

    // 前回フレームレートを表示してからの経過時間です。
    float elapsedTime;
    float elapsedTime2;

    void Start()
    {

    }

    void Update()
    {
        // 呼ばれた回数を加算します。
        frameCount++;

        // 前のフレームからの経過時間を加算します。
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1.0f)
        {
            // 経過時間が1秒を超えていたら、フレームレートを計算します。
            float fps = 1.0f * frameCount / elapsedTime;

            // 計算したフレームレートを画面に表示します。(小数点以下2ケタまで)
            string fpsRate = $"FPS: {fps.ToString("F2")} Fixed:{fps2.ToString("F2")}";
            fpsText.SetText(fpsRate);

            // フレームのカウントと経過時間を初期化します。
            frameCount = 0;
            elapsedTime = 0f;
        }
    }


    float  fps2;
    private void FixedUpdate()
    {
        // 呼ばれた回数を加算します。
        fixedCount++;

        // 前のフレームからの経過時間を加算します。
        elapsedTime2 += Time.deltaTime;

        if (elapsedTime2 >= 1.0f)
        {
            // 経過時間が1秒を超えていたら、フレームレートを計算します。
             fps2 = 1.0f * fixedCount / elapsedTime2;

            // フレームのカウントと経過時間を初期化します。
            fixedCount = 0;
            elapsedTime2 = 0f;
        }
    }
}