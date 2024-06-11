using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using Managers;

public class EC_Talk : EventCommand
{
    [SerializeField] TalkScriptData[] talkScript;

    public void setScript(TalkScriptData[] data)
    {
        talkScript = data;
    }

    TalkWindow TW
    {
        get
        {
            return GM.UI.talkWindow;
        }
    }

    IEnumerator Talk(TalkScriptData data)
    {
        //OutputText:これが出力された文章
        string OutputText = string.Empty;

        string text = data.content;

        Regex regex = new Regex("<.*?>", RegexOptions.Singleline);
        string RevisedText = regex.Replace(text, "");

        string[] SplitText = RevisedText.Split('\n');

        TW.SetWindowSize(SetWindowSize(RevisedText), data.isBubble);

        switch (data.speaker)
        {
            case TalkScriptData.Speaker.Player:
                {
                    Vector2 vector2 = new Vector2(GM.Player.Player.AdjustedPosition.x + data.offset.x, GM.Player.Player.AdjustedPosition.y + data.offset.y);
                    print(vector2);

                    TW.SetWindowPosition(GM.Game.R_Pos(vector2));
                }

                break;
            case TalkScriptData.Speaker.Eventer:
                {
                    Vector2 vector2 = new Vector2(transform.position.x + data.offset.x, transform.position.y + data.offset.y);
                    TW.SetWindowPosition(GM.Game.R_Pos(vector2));
                }

                break;
            default:
                break;
        }



        int count = 0;
        float float_count = 0;
        int sum = GetStringSum(data.content);

        TW.SetText(string.Empty);

        yield return StartCoroutine(TW.Show(0.05f));

        while (true)
        {
            if (INPUT.ButtonDown(Control.Decide))
            {
                if (float_count > (sum + 1))
                {
                    yield return StartCoroutine(TW.Hide(0.05f));
                    yield return new WaitForSeconds(0.05f);
                    yield break;
                }
                else
                {
                    float_count = sum;
                }
            }

            OutputText = Extraction(text, count);

            TW.SetText(OutputText);

            float_count += data.speed * Time.deltaTime;
            count = (int)float_count;
            yield return null;
        }
    }








    public override IEnumerator Command()
    {
        for(int i = 0; i < talkScript.Length; i++)
        {
            yield return StartCoroutine(Talk(talkScript[i]));
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
    Vector2Int SetWindowSize(string datas)
    {
        TW.text.SetText(datas);
        int linenum = datas.Split('\n').Length;


        return new Vector2Int(Mathf.CeilToInt(TW.text.preferredWidth + 8), linenum *16+ 8);
    }
}