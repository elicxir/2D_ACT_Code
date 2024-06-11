using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Memo_Content : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] MemoText testData;

    private void OnValidate()
    {
        if (testData != null)
        {
            Reflesh(testData);
        }
    }

    public void Reflesh(MemoText data)
    {
        title.text = data.title;
        text.text = data.content;

    }

}
