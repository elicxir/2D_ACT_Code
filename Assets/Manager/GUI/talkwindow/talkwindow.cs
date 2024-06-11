using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 会話のシステム。色変更にのみ対応している
/// </summary>
public class talkwindow : MonoBehaviour
{
    [SerializeField] RectTransform TextWindow;
    [SerializeField] TextMeshProUGUI TextUGUI;
    [SerializeField] Lean.Gui.LeanToggle toggle;

    int Speed =5;//文字表示のスピード

    public void Talk(string mes,int speed)
    {
        StartCoroutine(TalkProgress(mes, speed));
    }

    IEnumerator TalkProgress(string mes, int speed)
    {
        Define.PM.OnEvent = true;

        string text = mes;

        Regex re2 = new Regex("<.*?>", RegexOptions.Singleline);
        string RevisedText = re2.Replace(text, "");

        string[] SplitText = RevisedText.Split('\n');

        SetWindowSize(SplitText);

        int count = 0;
        int sum = GetStringSum(mes);

        TextUGUI.text = string.Empty;

        toggle.TurnOn();

        yield return new WaitForSeconds(0.2f);


        while (true)
        {
            if (Define.IM.ButtonDown(Control.Decide))
            {                
                if (count > (sum + 1)*100)
                {
                    yield return new WaitForSeconds(0.05f);
                    Define.PM.OnEvent = false;
                    toggle.TurnOff();
                    yield return new WaitForSeconds(0.05f);


                    yield break;
                }
                else{
                    count = sum*100;
                }              
            }
            
            TextUGUI.text = Extraction(text,count/100);

            count+=speed;

            yield return null;
        }
    }

    //<>タグと\nを除いて、stringの前からn文字を抽出する
    string Extraction(string data, int n)
    {
        string[] SplitText = data.Split('\n');

        int num = n;

        string revisedData = string.Empty;


        for (int k = 0; k < SplitText.Length; k++)
        {
            //文字数カウントを無視する記号範囲かどうか
            bool simbol = false;

            string newstring = string.Empty;

            for (int q = 0; q < SplitText[k].Length; q++)
            {
                string c = SplitText[k].Substring(q, 1);

                switch (c)
                {
                    case "<":
                        simbol = true;
                        newstring += c;
                        break;
                    case ">":
                        simbol = false;
                        newstring += c;
                        break;

                    default:
                        if (!simbol)
                        {
                            if (num > 0)
                            {
                                newstring += c;
                                num--;
                            }

                        }
                        else
                        {
                            newstring += c;
                        }
                        break;
                }
            }

            if (k != SplitText.Length - 1)
            {
                newstring += "\n";
                num--; num--; num--;
            }

            revisedData += newstring;

        }

        return revisedData;
    }

    int GetStringSum(string data)
    {
        string[] SplitText = data.Split('\n');

        int sum = 0;

        for (int k = 0; k < SplitText.Length; k++)
        {
            //文字数カウントを無視する記号範囲かどうか
            bool simbol = false;

            for (int q = 0; q < SplitText[k].Length; q++)
            {
                string c = SplitText[k].Substring(q, 1);

                switch (c)
                {
                    case "<":
                        simbol = true;
                        break;
                    case ">":
                        simbol = false;
                        break;

                    default:
                        if (!simbol)
                        {
                            sum++;

                        }

                        break;
                }
            }

            if (k != SplitText.Length - 1)
            {
                sum += 3;
            }

        }

        return sum;

    }

    //テキストのウィンドウサイズを変更する
    void SetWindowSize(string[] datas)
    {
        int maxlength = 0;

        foreach (string data in datas)
        {
            int length = data.Length * 14;

            maxlength = Mathf.Max(maxlength, length);
        }

        int linenum = datas.Length;
        Vector2Int size = new Vector2Int(maxlength + 10, linenum * 17 + 10);
        TextWindow.sizeDelta = size;
    }

    void AdjustPos()
    {

    }



}

