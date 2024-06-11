using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using TMPro;

public class UI_StatusUP : MonoBehaviour
{
    public enum mode
    {
        HP,
        MP,
        ATK,
        REG,
    }
    [SerializeField] mode Mode;

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI[] Texts;

    [SerializeField] RectTransform rect;

    float text_Right
    {
        get
        {
            return rect.anchoredPosition.x + rect.sizeDelta.x / 2;
        }
    }
    float text_Left
    {
        get
        {
            return rect.anchoredPosition.x - rect.sizeDelta.x / 2;

        }
    }


    void SetPos()
    {
        rect.anchoredPosition = GM.Player.R_Pos + Vector2.up * 36;

        while (text_Right > 150)
        {
            rect.anchoredPosition += Vector2.left;
        }
        while (text_Left < -150)
        {
            rect.anchoredPosition += Vector2.right;
        }

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

        // ③ メッシュを更新
        for (int i = 0; i < textInfo.materialCount; i++)
        {
            if (textInfo.meshInfo[i].mesh == null) { continue; }

            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;  // 変更
            textInfo.meshInfo[i].mesh.colors32 = textInfo.meshInfo[i].colors32;  // 変更

            text.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }

    const float delta = 0.1f;


    void TextMeshUpdate(int index, int vertexIndex, ref Vector3[] vertex, ref Color32[] colors, Vector3[] Base, float timer)
    {
        TextMeshFunction(timer - delta * index, ref vertex[vertexIndex], ref vertex[vertexIndex + 1], ref vertex[vertexIndex + 2], ref vertex[vertexIndex + 3], ref colors[vertexIndex], ref colors[vertexIndex + 1], ref colors[vertexIndex + 2], ref colors[vertexIndex + 3], Base[vertexIndex], Base[vertexIndex + 1], Base[vertexIndex + 2], Base[vertexIndex + 3]);
    }


    const float Phase1 = 0.70f;
    const float Phase2 = 0.90f;
    const float Phase3 = 0.25f;

    const int anim_H = 24;

    [SerializeField] AnimationCurve curve1;
    [SerializeField] AnimationCurve curve3;

    void TextMeshFunction(float timer, ref Vector3 LD, ref Vector3 LU, ref Vector3 RU, ref Vector3 RD, ref Color32 C_LD, ref Color32 C_LU, ref Color32 C_RU, ref Color32 C_RD, Vector3 Base_LD, Vector3 Base_LU, Vector3 Base_RU, Vector3 Base_RD)
    {
        timer = Mathf.Clamp(timer, 0, Phase1 + Phase2 + Phase3);

        Rect base_r = new Rect((Base_LD + Base_RU) / 2, Base_RU - Base_LD);



        if (timer <= Phase1)
        {
            float progress = timer / Phase1;
            float round = Mathf.PI * 2 * curve1.Evaluate(progress);

            Rect r = new Rect(base_r.position + Vector2.up * anim_H * (1 - curve1.Evaluate(progress)), new Vector2(base_r.width * Mathf.Cos(round), base_r.size.y));

            C_LD.a = (byte)(255 * curve1.Evaluate(progress));
            C_LU.a = (byte)(255 * curve1.Evaluate(progress));
            C_RU.a = (byte)(255 * curve1.Evaluate(progress));
            C_RD.a = (byte)(255 * curve1.Evaluate(progress));

            LD = LeftDown(r);
            LU = LeftUp(r);
            RU = RightUp(r);
            RD = RightDown(r);
        }
        else if (timer <= Phase1 + Phase2)
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
            Rect r = new Rect(base_r.position, base_r.size * (1 + curve3.Evaluate(progress) * 0.15f));

            C_LD.a = (byte)(255 * (1 - curve3.Evaluate(progress)));
            C_LU.a = (byte)(255 * (1 - curve3.Evaluate(progress)));
            C_RU.a = (byte)(255 * (1 - curve3.Evaluate(progress)));
            C_RD.a = (byte)(255 * (1 - curve3.Evaluate(progress)));

            LD = LeftDown(r);
            LU = LeftUp(r);
            RU = RightUp(r);
            RD = RightDown(r);
        }
    }

    public IEnumerator StatusUpText(StatusType type)
    {
        SetData(type);
        SetPos();

        foreach(TextMeshProUGUI t in Texts)
        {
            t.ForceMeshUpdate(true);
        }

        Vector3[] Base = null;
        float timer = 0;

        foreach (TextMeshProUGUI t in Texts)
        {
            UpdateAnimation(t, ref Base, timer);

        }

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

            yield return null;
        }
        canvasGroup.alpha = 0;
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

    void SetData(StatusType type)
    {
        switch (type)
        {
            case StatusType.HP:
                TXT("HP MAX UP");
                break;

            case StatusType.MP:
                TXT("MP MAX UP");
                break;

            case StatusType.ATK:
                TXT("Attack UP");
                break;
        }
    }
}


/*

    [System.Serializable]
    class TextMeshData
    {
        public Vector3[] vertices = new Vector3[4];
        public Color32[] colors = new Color32[4];

        public Rect rect;
        public Color32 DefaultColor;
    }


    TextMeshData[] SetTextMeshData(TextMeshProUGUI TMP)
    {
        List<TextMeshData> datas = new List<TextMeshData>();

        for (int i = 0; i < TMP.textInfo.characterInfo.Length; i++)
        {
            int verticeindex = TMP.textInfo.characterInfo[i].vertexIndex;
            int materialindex = TMP.textInfo.characterInfo[i].materialReferenceIndex;

            TMP_MeshInfo info = TMP.textInfo.meshInfo[materialindex];

            TextMeshData data = new TextMeshData();

            Rect rect = new Rect();

            data.vertices = new Vector3[4];
            data.colors = new Color32[4];

            for (int j = 0; j < data.vertices.Length; j++)
            {
                data.vertices[j] = info.vertices[verticeindex + j];
                data.colors[j] = info.colors32[verticeindex + j];
            }

            rect.center = (data.vertices[0] + data.vertices[1] + data.vertices[2] + data.vertices[3]) / 4;
            rect.size = data.vertices[2] - data.vertices[0];
            data.DefaultColor = TMP.color;
            data.rect = rect;

            datas.Add(data);
        }

        return datas.ToArray();
    }
    Vector3[] GetMeshVertices(TextMeshData[] data)
    {
        List<Vector3> list = new List<Vector3>();

        foreach (TextMeshData t in data)
        {
            foreach (Vector3 v in t.vertices)
            {
                list.Add(v);
            }
        }
        return list.ToArray();
    }
    Color32[] GetMeshColors(TextMeshData[] data)
    {
        List<Color32> list = new List<Color32>();

        foreach (TextMeshData t in data)
        {
            foreach (Color32 v in t.colors)
            {
                list.Add(v);
            }
        }
        return list.ToArray();
    }





    void Reflesh(TextMeshData[] data, ref TextMeshProUGUI TMP)
    {
        for (int i = 0; i < TMP.textInfo.meshInfo.Length; i++)
        {
            TMP.textInfo.meshInfo[i].vertices = GetMeshVertices(data);
            TMP.textInfo.meshInfo[i].colors32 = GetMeshColors(data);

            TMP.textInfo.meshInfo[i].mesh.vertices = TMP.textInfo.meshInfo[i].vertices;
            TMP.textInfo.meshInfo[i].mesh.colors32 = TMP.textInfo.meshInfo[i].colors32;

            TMP.UpdateGeometry(TMP.textInfo.meshInfo[i].mesh, i);
        }
    }

*/