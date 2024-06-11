using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class EventManager : Managers_MainGame
{
    [SerializeField] GameObject Event_and_Gimmick;

    [SerializeField] Event[] Events;
    Event_StatusUP[] StatusUP;

    bool isEventActivatable = true;//イベントを開始できるか

    List<Event> ActivatedEventList = new List<Event>();//アクティブなイベント
    List<Event> TouchingEventList = new List<Event>();//接触しているイベント

    public bool isTouching(Event @event)
    {
        return TouchingEventList.Contains(@event);
    }



    public override void ManagerUpdater(MainGame caller)
    {
        foreach (Event events in ActivatedEventList)
        {
                events.Updater();
        }

        GM.UI.HIDE_SYSTEMTEXT();
        CheckTouchingEvent();

        if(TouchingEventList.Count>0&& isEventActivatable)
        {
            foreach (Event TouchingEvent in TouchingEventList)
            {
                if (TouchingEvent.AutoEvent)
                {
                    if (TouchingEvent.AutoeventFlag)
                    {
                        StartCoroutine(StartEvent(TouchingEvent));
                        break;
                    }
                }
                else
                {
                    if (GM.Player.Player.isGround && TouchingEvent.EventFireCondition())
                    {
                        GM.UI.SHOW_SYSTEMTEXT(TouchingEvent.message, TouchingEvent.color);

                        if (GM.Inputs.ButtonDown(Control.Up))
                        {
                            StartCoroutine(StartEvent(TouchingEvent));
                            break;
                        }
                    }
                }
            }

        }    
    }




    void CheckTouchingEvent()
    {
        TouchingEventList.Clear();
        foreach (Event @event in ActivatedEventList)
        {
            if (@event.CheckTouchEvent(GM.Player.Player.AdjustedPosition))
            {
                TouchingEventList.Add(@event);
            }
        }

    }












    public override void OnSectionChanged(Vector2Int newGrid)
    {
        ActivateEvents(newGrid);
    }

    //イベントのアクティブ状態の切り替え
    void ActivateEvents(Vector2Int newGrid)
    {
        ActivatedEventList.Clear();
        foreach (Event events in Events)
        {
            Vector2Int P = new Vector2Int(Mathf.RoundToInt(events.Pos.x), Mathf.RoundToInt(events.Pos.y));

            if (Map.InSameSectionForActivate(newGrid, P) && (events.EventFlag() || events.ShowOnDeactivated))
            {
                events.gameObject.SetActive(true);
                events.Init();
                ActivatedEventList.Add(events);
            }
            else
            {
                if (events.gameObject.activeSelf)
                {
                    events.gameObject.SetActive(false);
                }
            }
        }
    }

    IEnumerator StartEvent(Event @event)
    {

        if (@event.BindEvent)
        {
            isEventActivatable = false;
            GM.Player.OnEvent = true;
            GM.Player.Player.buffManager.ADD(new Buff { contents = new BuffContent[1] { new BuffContent { buffType = BuffType.Invincible, amount = 1 } }, ID = "EventInvincible", timer = 10000 });

            GM.Player.Player.BaseVelocity = Vector2.zero;
        }

        yield return StartCoroutine(@event.DoEvent());

        isEventActivatable = true;
        GM.Player.OnEvent = false;
        GM.Player.Player.buffManager.REMOVE("EventInvincible");

        ActivateEvents(GM.Player.Player.NowMapGrid);
    }


    private void OnValidate()
    {
        GetEvents();
    }

    void GetEvents()
    {

        Events = Event_and_Gimmick.GetComponentsInChildren<Event>(true);

        StatusUP = Event_and_Gimmick.GetComponentsInChildren<Event_StatusUP>(true);

        for (int i = 0; i < StatusUP.Length; i++)
        {

            StatusUP[i].SetID(i);
        }
    }

    public override void GameExit()
    {
        foreach (Event @event in ActivatedEventList)
        {
            @event.ExitGame();
        }
    }
    public override void GameEnter()
    {
        foreach (Event @event in ActivatedEventList)
        {
            @event.EnterGame();
        }
    }
}


