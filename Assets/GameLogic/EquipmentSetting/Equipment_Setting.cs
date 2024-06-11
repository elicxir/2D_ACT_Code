using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment_Setting : MonoBehaviour
{
    [SerializeField] Transform cursorimage;

    [SerializeField] Image[] itemGraph;
    [SerializeField] Image[] catalystGraph;
    [SerializeField] Transform[] Pos;
    [SerializeField] Lean.Gui.LeanToggle Toggle;
    [SerializeField] Lean.Gui.LeanToggle SubToggle;

    [SerializeField] EquipBlock[] EquipBlocks;

    [SerializeField] RectTransform ListPanel;

    [SerializeField] AnimationCurve curve;


    [SerializeField] Slider slider;

    /// <summary>
    /// 0:装備欄選択状態
    /// 1:リストを開いた状態
    /// 2:詳細を開いた状態
    /// </summary>
    bool desc_window;

    /// <summary>
    /// false:装備欄選択状態
    /// true:リストを開いた状態
    /// </summary>
    bool list_window;

    int[] EquipItem = new int[3] { -1, -1, -1 };

    int[] EquipSpell = new int[2] { -1, -1 };

    int[] EquipAccessory = new int[2] { -1, -1 };

    List<int> itemID = new List<int>();
    List<int> spellID = new List<int>();
    List<int> accessoryID = new List<int>();


    List<int> ListID = new List<int>();

    int cursorpos
    {
        get
        {
            return cursorvar % 7;
        }
    }
    int cursorvar = 700;

    public int selectedpos = 0;//選択カーソルの画面上からの番号。


    void Set()
    {
        int index = 0;

        if (cursorpos < 3)
        {
            if (EquipItem[cursorpos] != -1)
            {
                index = ListID.IndexOf(EquipItem[cursorpos]);

            }
            else
            {
                index = 0;
            }
        }

        index = 0;

        listvar = index;

        foreach (EquipBlock block in EquipBlocks)
        {

            block.mode = cursorpos;

        }
        /*
        if (index == 0)
        {
            selectedpos = 0;
        }

        if(index==ListID.Count - 1)
        {
            selectedpos = 3;

        }
        else if(index == ListID.Count - 2)
        {
            selectedpos = 2;

        }
        else
        {
            selectedpos = 1;
        }
        */
        selectedpos = 0;

        slidervar = index - selectedpos;

    }

    void UP()
    {
        if (listvar < ListID.Count - 1)
        {
            listvar++;
            selectedpos++;

            if (selectedpos == 3 && slidervar < slider.allnum - 4)
            {
                slidervar++;
                selectedpos--;
            }
        }


    }

    void DOWN()
    {
        if (listvar > 0)
        {
            listvar--;
            selectedpos--;
            if (selectedpos == 0 && slidervar > 0)
            {
                slidervar--;
                selectedpos++;
            }
        }

    }



    /*
    int listpos
    {
        get
        {
            return listvar;
        }
        set
        {
            listvar+=value;

            selectedpos += value;
            if (selectedpos < 0)
            {
                selectedpos = 0;
            }
            if (selectedpos > 3)
            {
                selectedpos = 3;
            }


            if (listvar < 0)
            {
                listvar = 0;
            }
            else if (listvar>=ListID.Count)
            {
                listvar = Mathf.Max( ListID.Count - 1,0);
            }

            if (selectedpos == 0 && slidervar > 0)
            {
                slidervar--;
                selectedpos++;
            }
            if (selectedpos == 3 && slidervar <slider.allnum-4)
            {
                slidervar++;
                selectedpos--;
            }
        }
    }

    */

    int listvar = 0;

    public int slidervar = 0;

    public void ListUpdate()
    {
        //itemID = Define.SDM.GameData.GetItemList();
        //spellID = Define.SDM.GameData.GetSpellList();
        //accessoryID = Define.SDM.GameData.GetAccessoryList();
    }

    public void GraphUpdate()
    {
        ConsumableFunction[] data = new ConsumableFunction[3];
        SpellFunction[] data2 = new SpellFunction[6];

        for (int i = 0; i < EquipItem.Length; i++)
        {
            data[i] = Define.EDM.GET_CONSUMABLE_FUNCTION(EquipItem[i]);
            itemGraph[i].sprite = data[i].Sprite;
        }

        /*
        for (int i = 0; i < EquipSkill.Length; i++)
        {
            data2[i] = Define.EDM.GET_SKILL_DATA(EquipSkill[i]);
            catalystGraph[i].sprite = data2[i].Sprite;
        }
        */

        cursorimage.position = Pos[cursorpos].position;


        for (int i = 0; i < 4; i++)
        {
            EquipBlocks[i].selected = false;
            //EquipBlocks[i].pos = slidervar + i;

            if (slidervar + i > ListID.Count - 1)
            {
                EquipBlocks[i].index = -1;
                EquipBlocks[i].show = false;
            }
            else
            {


                EquipBlocks[i].show = true;

                EquipBlocks[i].index = ListID[slidervar + i];
            }


        }

        EquipBlocks[selectedpos].selected = true;

    }

    public void REFLESH()
    {
        if (list_window)
        {
            SelectSystem2();

            SliderUpdate();

            if (Define.IM.ButtonDown(Control.Decide))
            {
                Equip();
            }

            if (Define.IM.ButtonDown(Control.Cancel) || Define.IM.ButtonDown(Control.Menu))
            {
                ExitList();
            }

        }
        else
        {
            SelectSystem();

            if (Define.IM.ButtonDown(Control.Decide))
            {
                EnterList();
                CheckListID();
            }

            if (Define.IM.ButtonDown(Control.Cancel) || Define.IM.ButtonDown(Control.Menu))
            {
                ExitMenu();
            }
        }

    }

    public void EnterMenu()
    {/*
        EquipmentID id = Define.SDM.GameData.GetEquipment;

        EquipItem = id.ItemID;
        EquipSpell = id.SpellID;
        EquipAccessory = id.AccessoryID;

        itemID = Define.SDM.GameData.GetItemList();
        spellID = Define.SDM.GameData.GetSpellList();
        accessoryID = Define.SDM.GameData.GetAccessoryList();

        cursorvar = 7000;
        Toggle.TurnOn();
        //Define.GM.GameState = gamestate.SetEquipments;
        Define.GM.TimeScale(0);
        */
    }

    public void ExitMenu()
    {/*
        Define.SDM.GameData.SetEquipment(EquipItem, EquipSpell, EquipAccessory);

        Toggle.TurnOff();
        ///Define.GM.GameState = gamestate.SaveMenu;
        Define.GM.TimeScale(0);*/
    }


    public void EnterList()
    {
        ListUpdate();
        CreateList();
        Set();
        list_window = true;

        SubToggle.TurnOn();
    }

    public void ExitList()
    {
        list_window = false;
        SubToggle.TurnOff();
    }



    public void CreateList()
    {
        switch (cursorpos)
        {
            case 0:
            case 1:
            case 2:
                ListID = new List<int>(itemID);
                ListID.Sort();

                for (int i = 0; i < 3; i++)
                {
                    if (i != cursorpos && EquipItem[i] != -1)
                    {
                        ListID.Remove(EquipItem[i]);
                    }
                }

                ListID.Add(-1);

                break;

            case 3:
            case 4:
                ListID = new List<int>(spellID);
                ListID.Sort();

                for (int i = 3; i < 5; i++)
                {
                    if (i != cursorpos && EquipSpell[i - 3] != -1)
                    {
                        ListID.Remove(EquipSpell[i - 3]);
                    }
                }
                if (ListID.Count == 0)
                {
                    ListID.Add(-1);
                }

                break;

            case 5:
            case 6:
                ListID = new List<int>(accessoryID);
                ListID.Sort();

                for (int i = 5; i < 7; i++)
                {
                    if (i != cursorpos && EquipAccessory[i - 5] != -1)
                    {
                        ListID.Remove(EquipAccessory[i - 5]);
                    }
                }

                    ListID.Add(-1);
                break;
        }

        slider.allnum = ListID.Count;

    }



    void CheckListID()
    {
        string a = "";
        foreach (int data in ListID)
        {
            a += data;
            a += ":";
        }
        Debug.LogWarning("ListID" + a);
    }

    public void Equip()
    {

        switch (cursorpos)
        {
            case 0:
            case 1:
            case 2:
                int j = ListID[listvar];
                ListID[listvar] = EquipItem[cursorpos];
                EquipItem[cursorpos] = j;

                CreateList();
                ExitList();

                break;

            case 3:
            case 4:
                int k = ListID[listvar];
                ListID[listvar] = EquipSpell[cursorpos - 3];
                EquipSpell[cursorpos - 3] = k;

                CreateList();
                ExitList();

                break;

            case 5:
            case 6:
                int i = ListID[listvar];
                ListID[listvar] = EquipAccessory[cursorpos - 5];
                EquipAccessory[cursorpos - 5] = i;

                CreateList();
                ExitList();

                break;
        }
    }

    public void Deequip()
    {
        switch (cursorpos)
        {
            case 0:
            case 1:
            case 2:
                EquipItem[cursorpos] = -1;

                break;

            case 3:
            case 4:
                EquipSpell[cursorpos - 3] = -1;

                break;

            case 5:
            case 6:
                EquipAccessory[cursorpos - 5] = -1;


                break;
        }
    }

    /// <summary>
    /// SortAndDistinct(重複を削除して昇順にする。)
    /// </summary>
    public List<int> SAD(List<int> list)
    {
        list.Sort();

        List<int> dat = new List<int>();
        dat.Clear();

        foreach (int num in list)
        {
            if (!dat.Exists(a => (a == num) && (num != -1)))
            {
                dat.Add(num);
            }
        }

        return dat;
    }




    public void SliderUpdate()
    {
        slider.cell = slidervar;
    }

    void SelectSystem()
    {

        if (Define.IM.ButtonDown(Control.Right))
        {
            cursorvar++;
        }
        else if (Define.IM.ButtonDown(Control.Left))
        {
            cursorvar--;
        }
    }

    void SelectSystem2()
    {
        if (Define.IM.ButtonDown(Control.Up))
        {
            DOWN();
        }
        else if (Define.IM.ButtonDown(Control.Down))
        {
            UP();
        }

    }


    float scrolltimer = 0;
    float scroll = 0.15f;
    Vector2Int vector2;
    /*
    private void UP() 
    {
        scrolltimer = scroll;
        vector2 = 50 * Vector2Int.down;
    }
    private void DOWN()
    {
        scrolltimer = scroll;
        vector2 = 50 * Vector2Int.up;

    }
    */
    void ListScroll()
    {
        ListPanel.anchoredPosition = new Vector2(0, 50 * listvar + vector2.y * curve.Evaluate((scroll - scrolltimer) / scroll));

    }



    public Vector2 vector;
    // Update is called once per frame
    void Update()
    {
        if (scrolltimer > 0)
        {
            scrolltimer -= Time.deltaTime;
        }
        //GraphUpdate();
        //ListScroll();
    }
}




/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment_Setting : MonoBehaviour
{
    [SerializeField] Transform cursorimage;

    [SerializeField] Image[] itemGraph;
    [SerializeField] Image[] catalystGraph;
    [SerializeField] Transform[] Pos;
    [SerializeField] Lean.Gui.LeanToggle Toggle;
    [SerializeField] Lean.Gui.LeanToggle SubToggle;

    [SerializeField] EquipBlock[] EquipBlocks;

    [SerializeField] RectTransform ListPanel;

    [SerializeField] AnimationCurve curve;


    [SerializeField] Slider slider;

    /// <summary>
    /// 0:装備欄選択状態
    /// 1:リストを開いた状態
    /// 2:詳細を開いた状態
    /// </summary>
    bool desc_window;

    /// <summary>
    /// false:装備欄選択状態
    /// true:リストを開いた状態
    /// </summary>
    bool list_window;

    int[] EquipItem = new int[3] {-1,-1,-1 };
    int[] EquipCatalyst = new int[6];

    List<int> itemID = new List<int> { 0, 1, 3, 4, 6, 7, 8, 9 };
    List<int> catalystID = new List<int>();

    List<int> ListID = new List<int>();


    int cursorpos
    {
        get
        {
            return cursorvar % 9;
        }
    }
    int cursorvar = 900;


    public int selectedpos = 0;//選択カーソルの画面上からの番号。


    void Set()
    {

    }

    void UP()
    {
        if (listvar < ListID.Count-1)
        {
            listvar++;
        }



    }

    


    int listpos
    {
        get
        {
            return listvar;
        }
        set
        {
            listvar+=value;

            selectedpos += value;
            if (selectedpos < 0)
            {
                selectedpos = 0;
            }
            if (selectedpos > 3)
            {
                selectedpos = 3;
            }


            if (listvar < 0)
            {
                listvar = 0;
            }
            else if (listvar>=ListID.Count)
            {
                listvar = Mathf.Max( ListID.Count - 1,0);
            }

            if (selectedpos == 0 && slidervar > 0)
            {
                slidervar--;
                selectedpos++;
            }
            if (selectedpos == 3 && slidervar <slider.allnum-4)
            {
                slidervar++;
                selectedpos--;
            }
        }
    }
    int listvar = 0;

    public int slidervar = 0;

    public void ListUpdate()
    {
        itemID = Define.PM.GetItemList();
        catalystID = Define.PM.GetCatalystList();

    }

    public void GraphUpdate()
    {
        ItemData[] data = new ItemData[3];
        CatalystData[] data2= new CatalystData[6];

        for (int i = 0; i < data.Length; i++)
        {
            data[i]= Define.EDM.GET_ITEM_DATA(EquipItem[i]);
            itemGraph[i].sprite = data[i].Sprite;
        }

        for (int i = 0; i < data2.Length; i++)
        {
            data2[i] = Define.EDM.GET_CATALYST_DATA(EquipCatalyst[i]);
            //itemGraph[i].sprite = data2[i].Sprite;
        }


        cursorimage.position = Pos[cursorpos].position;

        for(int i = 0; i < 4; i++)
        {
            EquipBlocks[i].selected = false;
            //EquipBlocks[i].pos = slidervar + i;
            EquipBlocks[i].index = ListID[Mathf.Min(slidervar+i,ListID.Count-1)];
        }

        EquipBlocks[selectedpos].selected = true;

    }

    public void REFLESH()
    {
        if (Define.GM.GameState == 12)
        {
            if (list_window)
            {
                SelectSystem2();

                SliderUpdate();

                if (Define.IM.ButtonDown(Control.Jump))
                {
                    Equip();
                }

                if (Define.IM.ButtonDown("Start"))
                {
                    ExitList();
                }

            }
            else
            {
                SelectSystem();

                if (Define.IM.ButtonDown(Control.Jump))
                {
                    EnterList();
                }

                if (Define.IM.ButtonDown("Start"))
                {
                    ExitMenu();
                }
            }
        }
    }

    public void EnterMenu()
    {
        cursorvar = 9000;

        Toggle.TurnOn();
        Define.GM.State(12);
        Define.GM.TimeScale(0);

    }

    public void ExitMenu()
    {

        Toggle.TurnOff();
        Define.GM.State(4);
        Define.GM.TimeScale(1);
    }


    public void EnterList()
    {

        CreateList();

        list_window = true;
        

        SubToggle.TurnOn();
    }

    public void ExitList()
    {
        list_window = false;
        SubToggle.TurnOff();
    }



    public void CreateList()
    {
        if (cursorpos < 3)
        {
            ListID = new List<int>(itemID);
            ListID.Add(-1);
            ListID.Sort();


            for (int i = 0; i < 3; i++)
            {
                if(i!= cursorpos&& EquipItem[i] != -1)
                {                  
                        ListID.Remove(EquipItem[i]);                   
                }
            }
        }
        else if (cursorpos < 9)
        {
            EquipItem[cursorpos] = ListID[listpos];

        }


        slider.allnum = ListID.Count;

    }

    void CheckitemID()
    {
        string a = "";
        foreach (int data in itemID)
        {
            a += data;
            a += ":";
        }
        Debug.LogWarning("itemID" + a);
    }

    void CheckListID()
    {
        string a = "";
        foreach (int data in ListID)
        {
            a += data;
            a += ":";
        }
        Debug.LogWarning("ListID" + a);
    }

    public void Equip()
    {
        if (cursorpos < 3)
        {
            int j = ListID[listpos];
            ListID[listpos] = EquipItem[cursorpos];
            EquipItem[cursorpos] = j;

            //ListID = SAD(ListID);
            CreateList();
            ExitList();
        }
        else if (cursorpos < 9)
        {
            EquipItem[cursorpos] = ListID[listpos];

        }
    }

    public void Deequip()
    {
        if (cursorpos < 3)
        {
            EquipItem[cursorpos] =-1;
        }
        else if (cursorpos < 9)
        {
            EquipItem[cursorpos] = -1;

        }
    }

    /// <summary>
    /// SortAndDistinct(重複を削除して昇順にする。)
    /// </summary>
    public List<int> SAD(List<int> list)
    {
        list.Sort();

        List<int> dat = new List<int>();
        dat.Clear();

        foreach(int num in list)
        {
            if (!dat.Exists(a => (a == num)&&(num!=-1)))
            {
                dat.Add(num);
            }
        }

        return dat;
    }




    public void SliderUpdate()
    {
        slider.cell = slidervar;
    }

    void SelectSystem()
    {

        if (Define.IM.ButtonDown("Horizontal2") == 1)
        {
            cursorvar++;
        }
        else if (Define.IM.ButtonDown("Horizontal2") == -1)
        {
            cursorvar--;
        }
    }

    void SelectSystem2()
    {
        if (Define.IM.ButtonDown("Vertical2") == 1)
        {
            //UP();
            listpos = -1;
        }
        else if (Define.IM.ButtonDown("Vertical2") == -1)
        {
            //DOWN();
            listpos = 1;
        }

        //Debug.Log(listpos);
    }


    float scrolltimer = 0;
    float scroll = 0.15f;
    Vector2Int vector2;
    
    private void UP() 
    {
        scrolltimer = scroll;
        vector2 = 50 * Vector2Int.down;
    }
    private void DOWN()
    {
        scrolltimer = scroll;
        vector2 = 50 * Vector2Int.up;

    }
    
void ListScroll()
{
    ListPanel.anchoredPosition = new Vector2(0, 50 * listvar + vector2.y * curve.Evaluate((scroll - scrolltimer) / scroll));

}

// Start is called before the first frame update
void Start()
{

}


public Vector2 vector;
// Update is called once per frame
void Update()
{
    if (scrolltimer > 0)
    {
        scrolltimer -= Time.deltaTime;
    }
    GraphUpdate();
    //ListScroll();
}
}

 
 */