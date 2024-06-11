using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class inGameUI : MonoBehaviour
{
    float bosshp = 1.00f;
    float playerhp = 1.00f;


    bool show = true;//ボスの体力バーの表示の可否

    [SerializeField] Image playerhpbar;
    [SerializeField] Image playermpbar;


    [SerializeField] Image bosshpbar;

    [SerializeField] Image ItemGraph;

    [SerializeField] Text warningtext;

    [SerializeField] Text systemtext;

    [SerializeField] Text itemcount;


    [SerializeField] talkwindow talkwindow;


    [SerializeField] Lean.Gui.LeanToggle toggle;

    [SerializeField] Lean.Gui.LeanToggle mestoggle;
    [SerializeField] Lean.Gui.LeanToggle systoggle;



    [SerializeField] Text bossname;


    //アイテム入手ウィンドウ用
    [SerializeField] Lean.Gui.LeanToggle itemgettoggle;
    [SerializeField] Text itemtext;
    [SerializeField] RectTransform itempanel;
    [SerializeField] RectTransform itemgraph;
    [SerializeField] RectTransform itemtextrect;
    [SerializeField] Image GetItemGraph;


    public void Talk(TalkScriptData data,Vector2 pos)
    {
        talkwindow.Talk(data.content, data.speed);
    }

    public void SetItemGraph(int index, int count)
    {
        ItemGraph.sprite = Define.EDM.GET_CONSUMABLE_FUNCTION(index).Sprite;
        itemcount.text = count.ToString();
    }



    [SerializeField] Image[] buffimage;
    [SerializeField] BuffImageData BuffImageData;
/*
    public void SetBuffGraph(BuffData buffdata)
    {

        for (int q = 0; q < 8; q++)
        {
            buffimage[7-q].sprite = BuffImageData.GetSprite(buffdata.buffID[q], buffdata.positive[q]);
        }

        foreach (Image data in buffimage)
        {


            if (data.sprite == null)
            {
                data.color = Color.clear;
            }
            else
            {
                data.color = Color.white;
            }
        }

    }

    */


    IEnumerator routine;








    private void Start()
    {
        routine = Warning();
    }

    void Update()
    {
        warningtext.rectTransform.position = new Vector3Int((int)Define.PM.R_Pos.x + 240, (int)Define.PM.R_Pos.y + 180 + 35, 0);
        systemtext.rectTransform.position = new Vector3Int((int)Define.PM.R_Pos.x + 240, (int)Define.PM.R_Pos.y + 180 + 35, 0);
        /*
                for(int n=0;n<MPUnit.Length;n++)
                {
                    if (n + 1 > Define.PM.MaxMP)
                    {
                        MPUnit[n].enable = false;
                    }
                    else {
                        MPUnit[n].enable = true;
                    }
                }
                for (int n = 0; n < MPUnit.Length; n++)
                {
                    MPUnit[n].mana = Define.PM.MP - n;
                    MPUnit[n].energy = Define.PM.Energy - n;
                }*/
    }



    /*
        // Update is called once per frame
        void LateUpdate()
        {

            if (playerhp < 0)
            {
                playerhp = 0;
            }
            else if (playerhp >1)
            {
                playerhp = 1;
            }
            if (bosshp < 0)
            {
                bosshp = 0;
            }
            else if (bosshp > 1)
            {
                bosshp = 1;
            }

            playerhpbar.fillAmount = playerhp;
            if (show)
            {
                bosshpbar.fillAmount = bosshp;
            }

        }
    */
    public void PLAYER_HP(float player)
    {
        playerhp = player;
        if (playerhp < 0)
        {
            playerhp = 0;
        }
        else if (playerhp > 1)
        {
            playerhp = 1;
        }

        playerhpbar.fillAmount = playerhp;

    }

    public void PLAYER_MP(float playermp)
    {

        playermpbar.fillAmount = playermp;

    }

    public void SHOW_BOSS_HP(string name)
    {
        bossname.text = name;
        toggle.TurnOn();
    }

    public void HIDE_BOSS_HP()
    {
        toggle.TurnOff();
    }



    IEnumerator Warning()
    {
        mestoggle.TurnOn();
        yield return new WaitForSeconds(0.4f);
        mestoggle.TurnOff();
    }


    public void SHOW_WARNING(string mes, Color color)
    {

        warningtext.color = color;
        warningtext.text = mes;
        StopCoroutine(routine);
        routine = null;
        routine = Warning();
        StartCoroutine(routine);
    }

    public void SHOW_SYSTEMTEXT(string mes, Color color)
    {
        systoggle.TurnOff();

        systemtext.color = color;
        systemtext.text = mes;

        systoggle.TurnOn();
    }
    public void HIDE_SYSTEMTEXT()
    {
        systoggle.TurnOff();

    }

    public void EQUIP_GET_WINDOW(string name, Sprite sprite)
    {

        itemtext.text = name;
        itemtext.color = Color.white;

        GetItemGraph.sprite = sprite;


        int width = (int)itemtext.preferredWidth + 44;
        width += width % 2;

        itempanel.sizeDelta = new Vector2Int(width, (int)itempanel.rect.height);
        itemgraph.anchoredPosition = new Vector2Int((int)(-width * 0.5f) + 21, 0);

        StartCoroutine(ITEM_GET_WINDOW_CLOSE());

    }

    IEnumerator ITEM_GET_WINDOW_CLOSE()
    {
        itemgettoggle.TurnOn();
        Define.PM.OnEvent = true;

        yield return new WaitForSeconds(1.0f);

        while (true)
        {
            yield return null;
            if (Define.IM.ButtonDown(Control.Decide))
            {
                yield return new WaitForSeconds(0.05f);

                Define.PM.OnEvent = false;
                itemgettoggle.TurnOff();
                yield break;

            }


        }
        /*yield return null;
        yield break;

        yield return new WaitForSeconds(3.0f);
        itemgettoggle.TurnOff();*/
    }







}
