using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using GameConsts;
using Managers;

public class UI_SystemText : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] CanvasGroup cg;

    string key
    {
        get
        {
            return Glyph.UP;
        }
    }

    private void LateUpdate()
    {
        if (cg.alpha == 1)
        {
            Vector2 P_R_Pos = GM.Game.R_Pos(GM.Player.Player.Position);
            rect.anchoredPosition = new Vector2Int(Mathf.RoundToInt(P_R_Pos.x), Mathf.RoundToInt(P_R_Pos.y + 32));
        }
    }



    public void SHOW_SYSTEMTEXT(string mes, Color color)
    {
        cg.alpha = 1;

        text.color = color;
        text.text = key+mes;

        rect.sizeDelta = new Vector2((int)text.preferredWidth+8, 18);
    }
    public void HIDE_SYSTEMTEXT()
    {
        cg.alpha = 0;
    }

}





/*
 
public class UI_SystemText : MonoBehaviour
{
    [SerializeField] Lean.Gui.LeanToggle toggle;
    [SerializeField] TextMeshProUGUI text;

    public void SHOW_SYSTEMTEXT(string mes, Color color)
    {
        toggle.TurnOff();

        text.color = color;
        text.text = mes;

        toggle.TurnOn();
    }
    public void HIDE_SYSTEMTEXT()
    {
        toggle.TurnOff();
    }

    private void LateUpdate()
    {
        transform.position = Define.PM.R_Pos+new Vector2(240,180)+ new Vector2(0, 20);
    }







    IEnumerator Warning()
    {
        toggle.TurnOn();
        yield return new WaitForSeconds(0.4f);
        toggle.TurnOff();
    }

    IEnumerator routine;

    public void SHOW_WARNING(string mes ,Color color)
    {
        text.color = color;
        text.text = mes;
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
        
        routine = Warning();
        StartCoroutine(routine);
    }
}

 
 */