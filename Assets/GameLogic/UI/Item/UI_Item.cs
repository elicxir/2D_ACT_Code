using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Managers;

public class UI_Item : MonoBehaviour
{
    [SerializeField] Image ItemGraph;
    [SerializeField] Image WindowFrame;

    [SerializeField] TextMeshProUGUI itemcount;
    [SerializeField] Sprite none;

    [SerializeField] Sprite Item_Frame;
    [SerializeField] Sprite Spell_Frame;


    public void SetSpellGraph(SpellFunction function)
    {
        WindowFrame.sprite = Spell_Frame;
        if (function != null)
        {
            ItemGraph.sprite = function.Sprite;
            itemcount.text = string.Empty;
        }
        else
        {
            ItemGraph.sprite = none;
            itemcount.text = string.Empty;
        }
    }

    public void SetItemGraph(ConsumableFunction function)
    {
        WindowFrame.sprite = Item_Frame;

        if (function != null)
        {
            ItemGraph.sprite = function.Sprite;
            itemcount.SetText("{0}", function.useCount);
        }
        else
        {
            ItemGraph.sprite = none;
            itemcount.text = string.Empty;
        }
    }
}
