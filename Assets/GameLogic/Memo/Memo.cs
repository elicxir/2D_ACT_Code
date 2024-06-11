using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;

public class Memo : SubPanelExecuter
{
    [SerializeField] Memo_Content memo_Content;
    [SerializeField] List<Memo_Title> memo_Titles;

    [SerializeField] Memo_Title ContainerPrefab;

    [SerializeField] Memo_Tab[] tabs;

    [SerializeField] Slider slider;

    [SerializeField] AnimationCurve curve;

    public int scroll = 0;//リストの一番上のコンテナの番号


    public MemoText[] Tutorials;
    public MemoText[] Quests;
    public MemoText[] Notes;

    void Start()
    {
        SetContainer();
    }

    float scrolltime = 0.16f;

    bool scrolling = false;

    public override IEnumerator Init(gamestate before)
    {
        Selected = MemoText.MemoType.Tutorial;
        ContainerUpdate();
        ContentUpdate();


        yield return StartCoroutine(In(0.3f));
    }

    public override void Updater()
    {
        Select();

        foreach (Memo_Title data in memo_Titles)
        {
            data.OpacityUpdate();
        }

        if (GM.Inputs.ButtonDown(Control.Menu) || GM.Inputs.ButtonDown(Control.Cancel))
        {
            GAME.StateQueue((int)gamestate.Menu);
        }
    }


    IEnumerator ScrollUp()
    {

        float counter = 0;
        Vector2 startPos = rect.anchoredPosition;

        while (true)
        {
            float counter1 = Mathf.Min(counter + Time.unscaledDeltaTime, scrolltime);

            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, startPos.y - 24 * curve.Evaluate(counter1 / scrolltime));

            counter = counter1;
            if (counter1 == scrolltime)
            {
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, Mathf.RoundToInt(rect.anchoredPosition.y));
                scrolling = false;
                yield break;
            }
            yield return null;
        }
    }
    IEnumerator ScrollDown()
    {
        float counter = 0;
        Vector2 startPos = rect.anchoredPosition;

        while (true)
        {
            float counter1 = Mathf.Min(counter + Time.unscaledDeltaTime, scrolltime);

            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, startPos.y + 24 * curve.Evaluate(counter1 / scrolltime));

            counter = counter1;
            if (counter1 == scrolltime)
            {
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, Mathf.RoundToInt(rect.anchoredPosition.y));
                scrolling = false;
                yield break;
            }
            yield return null;
        }
    }

    int NeededNum
    {
        get
        {
            return Mathf.Max(Tutorials.Length, Quests.Length, Notes.Length, 8);
        }
    }

    [SerializeField] GameObject Container;
    RectTransform rect
    {
        get
        {
            return Container.transform as RectTransform;
        }
    }

    void SetContainer()
    {
        for (int i = 0; i < memo_Titles.Count; i++)
        {
            Destroy(memo_Titles[i]);
        }


        memo_Titles.Clear();

        if (memo_Titles.Count < NeededNum)
        {
            for (int i = memo_Titles.Count; i < NeededNum; i++)
            {

                memo_Titles.Add(Instantiate(ContainerPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), Container.transform));

                memo_Titles[i].rectTransform.anchoredPosition     = new Vector3(0, 88 - 20 - 10 - 24 * i);
            }
        }


    }


    int SelectedNum = 0;
    MemoText.MemoType Selected = MemoText.MemoType.Tutorial;

    void ContentUpdate()
    {
        MemoText[] data = null;
        switch (Selected)
        {
            case MemoText.MemoType.Note:
                data = Notes;
                break;
            case MemoText.MemoType.Tutorial:
                data = Tutorials;
                break;
            case MemoText.MemoType.Quest:
                data = Quests;
                break;
        }
        memo_Content.Reflesh(data[SelectedNum]);

        for (int i = 0; i < memo_Titles.Count; i++)
        {
            memo_Titles[i].Selected = false;
        }
        memo_Titles[SelectedNum].Selected = true;

        slider.cell = scroll;
        //rect.anchoredPosition = Vector2.up * 24 * scroll;



    }

    void ContainerUpdate()
    {
        foreach (Memo_Tab TAB in tabs)
        {
            TAB.Selected = false;
        }
        tabs[(int)Selected].Selected = true;

        MemoText[] data = null;
        switch (Selected)
        {
            case MemoText.MemoType.Note:
                data = Notes;
                break;
            case MemoText.MemoType.Tutorial:
                data = Tutorials;
                break;
            case MemoText.MemoType.Quest:
                data = Quests;
                break;
        }

        for (int i = 0; i < memo_Titles.Count; i++)
        {
            if (i < data.Length)
            {
                memo_Titles[i].gameObject.SetActive(true);
                memo_Titles[i].str = data[i].title;
                memo_Titles[i].TextUpdate();
            }
            else
            {
                memo_Titles[i].gameObject.SetActive(false);
            }
        }

        slider.allnum = data.Length;
        SelectedNum = Mathf.Min(SelectedNum, data.Length - 1);
    }


    public void REFLESH()
    {



        if (GM.Inputs.ButtonDown(Control.Menu) || GM.Inputs.ButtonDown(Control.Cancel))
        {
            //ExitMenu();
        }
    }

    void Select()
    {
        bool flag = true;

        if (GM.Inputs.ButtonDown(Control.Right))
        {
            Right();
        }
        else if (GM.Inputs.ButtonDown(Control.Left))
        {
            Left();
        }
        else if (GM.Inputs.ButtonDown(Control.Up))
        {
            Up();

        }
        else if (GM.Inputs.ButtonDown(Control.Down))
        {
            Down();
        }
        else
        {
            flag = false;
        }

        if (flag)
        {
            ContainerUpdate();
            SetScroll();
            ContentUpdate();

        }


    }

    int SelectedCapacity
    {
        get
        {
            switch (Selected)
            {
                case MemoText.MemoType.Note:
                    return Notes.Length;

                case MemoText.MemoType.Tutorial:
                    return Tutorials.Length;

                case MemoText.MemoType.Quest:
                    return Quests.Length;
            }

            return 0;
        }
    }

    private void Up()
    {
        if (!scrolling)
        {
            SelectedNum = Mathf.Max(0, SelectedNum - 1);

        }

    }
    private void Down()
    {
        if (!scrolling)
        {
            SelectedNum = Mathf.Min(SelectedCapacity - 1, SelectedNum + 1);

        }

    }

    void SetScroll()
    {
        int mem = scroll;

        if (SelectedNum - 2 < scroll)
        {
            scroll = Mathf.Max(SelectedNum - 2, 0);
        }
        if (SelectedNum - 5 > scroll)
        {
            scroll = Mathf.Min(SelectedNum - 5, SelectedCapacity - 8);
        }
        if (scroll > SelectedCapacity - 8)
        {
            print("dd");
            scroll = SelectedCapacity - 8;

        }

        if (scroll - mem == -1)
        {
            scrolling = true;
            StartCoroutine(ScrollUp());
        }
        else if (scroll - mem == 1)
        {
            scrolling = true;
            StartCoroutine(ScrollDown());
        }




    }


    void Right()
    {
        switch (Selected)
        {
            case MemoText.MemoType.Tutorial:
                Selected = MemoText.MemoType.Quest;
                break;

            case MemoText.MemoType.Quest:
                Selected = MemoText.MemoType.Note;
                break;

            case MemoText.MemoType.Note:
                Selected = MemoText.MemoType.Tutorial;
                break;
        }
    }

    void Left()
    {
        switch (Selected)
        {
            case MemoText.MemoType.Tutorial:
                Selected = MemoText.MemoType.Note;
                break;

            case MemoText.MemoType.Quest:
                Selected = MemoText.MemoType.Tutorial;
                break;

            case MemoText.MemoType.Note:
                Selected = MemoText.MemoType.Quest;
                break;
        }
    }



    public void EnterMenu()
    {
        Selected = MemoText.MemoType.Tutorial;
        ContainerUpdate();
        ContentUpdate();
        Define.GM.TimeScale(0);
        //Define.GM.GameState = gamestate.Memo;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
