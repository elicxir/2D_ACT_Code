using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using TMPro;

public class UI_StageName : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;

    [SerializeField] TextMeshProUGUI[] Texts;

    [SerializeField] RectTransform[] Lines;

    void LineWidth(int width)
    {
        foreach (RectTransform r in Lines)
        {
            r.sizeDelta = Vector2.up * 2 + Vector2.right * width;
        }
    }

    public void interrupt()
    {
        canvasGroup.alpha = 0;
    }

    private void UpdateAnimation(TextMeshProUGUI text, ref Vector3[] Base, float time)
    {
        TMP_TextInfo textInfo;

        text.ForceMeshUpdate(true);
        textInfo = text.textInfo;


        var count = Mathf.Min(textInfo.characterCount, textInfo.characterInfo.Length);

        for (int i = 0; i < count; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }

            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            if (Base == null)
            {
                Base = textInfo.meshInfo[materialIndex].vertices;
            }

            TextMeshUpdate(i, vertexIndex, ref textInfo.meshInfo[materialIndex].vertices, ref textInfo.meshInfo[materialIndex].colors32, Base, time);
        }

        for (int i = 0; i < textInfo.materialCount; i++)
        {
            if (textInfo.meshInfo[i].mesh == null) { continue; }

            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;  // •ÏX
            textInfo.meshInfo[i].mesh.colors32 = textInfo.meshInfo[i].colors32;  // •ÏX

            text.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }

    const float delta = 0.1f;


    void TextMeshUpdate(int index, int vertexIndex, ref Vector3[] vertex, ref Color32[] colors, Vector3[] Base, float timer)
    {
        TextMeshFunction(timer - delta * index, ref vertex[vertexIndex], ref vertex[vertexIndex + 1], ref vertex[vertexIndex + 2], ref vertex[vertexIndex + 3], ref colors[vertexIndex], ref colors[vertexIndex + 1], ref colors[vertexIndex + 2], ref colors[vertexIndex + 3], Base[vertexIndex], Base[vertexIndex + 1], Base[vertexIndex + 2], Base[vertexIndex + 3]);
    }


    const float Phase1 =1;
    const float Phase2 = 1.3f;
    const float Phase3 = 1;

    [SerializeField] AnimationCurve curve1;

    void TextMeshFunction(float timer, ref Vector3 LD, ref Vector3 LU, ref Vector3 RU, ref Vector3 RD, ref Color32 C_LD, ref Color32 C_LU, ref Color32 C_RU, ref Color32 C_RD, Vector3 Base_LD, Vector3 Base_LU, Vector3 Base_RU, Vector3 Base_RD)
    {
        timer = Mathf.Clamp(timer, 0, Phase1 + Phase2 + Phase3);

        Rect base_r = new Rect((Base_LD + Base_RU) / 2, Base_RU - Base_LD);

        if (timer <= Phase1)
        {
            float progress = timer / Phase1;
            float round = Mathf.PI * 2 * curve1.Evaluate(progress);

            Vector2 basecenter = new Vector2(0, base_r.y);

            Rect r = new Rect(Vector2.Lerp(basecenter, base_r.position, curve1.Evaluate(progress)), new Vector2(base_r.width * Mathf.Cos(round), base_r.size.y));

            C_LD.a = (byte)(255 * curve1.Evaluate(progress));
            C_LU.a = (byte)(255 * curve1.Evaluate(progress));
            C_RU.a = (byte)(255 * curve1.Evaluate(progress));
            C_RD.a = (byte)(255 * curve1.Evaluate(progress));

            LD = LeftDown(r);
            LU = LeftUp(r);
            RU = RightUp(r);
            RD = RightDown(r);
        }
        else if ((timer <= Phase1 + Phase2))
        {
            C_LD.a = (byte)255;
            C_LU.a = (byte)255;
            C_RU.a = (byte)255;
            C_RD.a = (byte)255;

            LD = Base_LD;
            LU = Base_LU;
            RU = Base_RU;
            RD = Base_RD;
        }
        else
        {
            float progress = (timer - (Phase1 + Phase2)) / Phase3;
            float round = Mathf.PI * 2 * (1 - curve1.Evaluate(progress));

            Vector2 basecenter = new Vector2(0, base_r.y);

            Rect r = new Rect(Vector2.Lerp(basecenter, base_r.position, (1 - curve1.Evaluate(progress))), new Vector2(base_r.width * Mathf.Cos(round), base_r.size.y));

            C_LD.a = (byte)(255 * (1 - curve1.Evaluate(progress)));
            C_LU.a = (byte)(255 * (1 - curve1.Evaluate(progress)));
            C_RU.a = (byte)(255 * (1 - curve1.Evaluate(progress)));
            C_RD.a = (byte)(255 * (1 - curve1.Evaluate(progress)));

            LD = LeftDown(r);
            LU = LeftUp(r);
            RU = RightUp(r);
            RD = RightDown(r);
        }


    }

    public IEnumerator ShowStageName(string name, float delay)
    {
        yield return new WaitForSeconds(delay);

        TXT(name);

        int linewidth = 0;

        foreach (TextMeshProUGUI t in Texts)
        {
            t.ForceMeshUpdate(true);
            linewidth = Mathf.Max(linewidth, Mathf.RoundToInt(t.preferredWidth + 8));
        }

        Vector3[] Base = null;
        float timer = 0;

        foreach (TextMeshProUGUI t in Texts)
        {
            UpdateAnimation(t, ref Base, timer);
        }
        UpdateLineWidth(timer, linewidth, Base.Length);

        canvasGroup.alpha = 1;

        while (timer < Phase1 + Phase2 + Phase3 + delta * Base.Length / 4)
        {
            if (GM.Game.GameState == gamestate.MainGame)
            {
                timer += Time.deltaTime;
            }

            foreach (TextMeshProUGUI t in Texts)
            {
                UpdateAnimation(t, ref Base, timer);
            }

            UpdateLineWidth(timer,linewidth, Base.Length);

            yield return null;
        }
        canvasGroup.alpha = 0;
    }

    void UpdateLineWidth(float timer,int max,int length)
    {
        if (timer <= Phase1)
        {
            float progress = timer / Phase1;
            LineWidth(Mathf.CeilToInt(max * curve1.Evaluate(progress)));
        }
        else if ((timer <= Phase1 + Phase2+length*delta/4))
        {
            LineWidth(max);
        }
        else
        {
            float progress = (timer - (Phase1 + Phase2 + length * delta/4)) / Phase3;
            LineWidth(Mathf.CeilToInt(max * (1-curve1.Evaluate(progress))));
        }
    }


    Vector2 LeftUp(Rect rect)
    {
        return Vector2.right * rect.x + Vector2.up * rect.y + Vector2.left * rect.width * 0.5f + Vector2.up * rect.height * 0.5f;
    }
    Vector2 RightUp(Rect rect)
    {
        return Vector2.right * rect.x + Vector2.up * rect.y + Vector2.right * rect.width * 0.5f + Vector2.up * rect.height * 0.5f;
    }
    Vector2 LeftDown(Rect rect)
    {
        return Vector2.right * rect.x + Vector2.up * rect.y + Vector2.left * rect.width * 0.5f + Vector2.down * rect.height * 0.5f;
    }
    Vector2 RightDown(Rect rect)
    {
        return Vector2.right * rect.x + Vector2.up * rect.y + Vector2.right * rect.width * 0.5f + Vector2.down * rect.height * 0.5f;
    }


    void TXT(string s)
    {
        foreach (TextMeshProUGUI t in Texts)
        {
            t.text = s;
        }
    }
}
