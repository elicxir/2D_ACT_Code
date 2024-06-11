using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameConsts;

public class Tutorial_Block : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI input;
    [SerializeField] TextMeshProUGUI desc;

    [ContextMenu("test")]
    void test()
    {
        input.text = Glyph.DOWN;
    }

}
