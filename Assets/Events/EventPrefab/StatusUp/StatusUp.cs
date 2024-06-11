using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUp : Event
{
    /*
    public int item_index;
    [SerializeField] StatusUpItem HP_UP;
    [SerializeField] StatusUpItem MP_UP;
    [SerializeField] StatusUpItem MPreg_UP;
    [SerializeField] StatusUpItem Attack_UP;


    [SerializeField] string ItemName;
    [SerializeField] Sprite ItemSprite;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void OnValidate()
    {
        GetData();
        SetData();
    }

    private void Start()
    {
        SetData();
    }

    void GetData()
    {
        switch (itemType)
        {
            case ItemType.HP:
                ItemName = HP_UP.itemname;
                ItemSprite = HP_UP.sprite;
                break;

            case ItemType.MP:
                ItemName = MP_UP.itemname;
                ItemSprite = MP_UP.sprite;
                break;

            case ItemType.reg:
                ItemName = MPreg_UP.itemname;
                ItemSprite = MPreg_UP.sprite;
                break;

            case ItemType.Attack:
                ItemName = Attack_UP.itemname;
                ItemSprite = Attack_UP.sprite;
                break;
        }
    }

    void SetData()
    {
        message = "";
        spriteRenderer.sprite = ItemSprite;
    }

    enum ItemType
    {
        HP,
        MP,
        reg,
        Attack
    }
    [SerializeField] ItemType itemType;


    bool isValid
    {
        get
        {
            return true;
        }
    }

    bool isGot
    {
        get
        {
            return (Define.SDM.GameData.StatusUpIndex.Contains(item_index));
        }
    }

    public override bool EventFlag()
    {
        return (!isGot);
    }

    IEnumerator Get()
    {
        Define.PM.Player.TimeMult = 0.5f;
        yield return new WaitForSeconds(10);
        Define.PM.Player.TimeMult = 1.5f;


        {
            Define.SDM.GameData.StatusUpIndex.Add(item_index);
        }

        /*
        EventFlag flag = new EventFlag
        {
            //FlagID = EventID,
            flag = true
        };
        Define.SDM.GameData.FlagDataStore(flag);
        Define.GM.TimeScale(1);
        yield break;
    }

   public override void Done()
    {
        StartCoroutine(Get());
        StartCoroutine(Define.UI.UI_StatusUP.PlayAnimation());

    }*/
}

[System.Serializable]
class StatusUpItem
{
    public string itemname;
    public Sprite sprite;
}