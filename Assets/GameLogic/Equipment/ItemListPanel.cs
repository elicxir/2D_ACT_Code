using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class ItemListPanel : MonoBehaviour
{
    public readonly int width = 9;

    [SerializeField] CanvasGroup group;

    [SerializeField] EQ_cursor cursor;

    [SerializeField] Transform ListTransform;

    public float wave_timer = 0;
    public void Wave()
    {
        wave_timer += Time.deltaTime * 11;
        cursor.SetDelta = Mathf.RoundToInt(Mathf.Sin(wave_timer));
    }

    public void Show()
    {
        group.alpha = 1;
    }

    public void Hide()
    {
        group.alpha = 0;
    }

    [SerializeField] ItemList_Item prefab;

    [SerializeField] List<ItemList_Item> items = new List<ItemList_Item>();

    [SerializeField] List<int> list = new List<int>();

    public int GetSelectedIndex
    {
        get
        {
            return list[selectedIndex];
        }
    }

    int selectedIndex;
    Vector2Int selectedPos;

    Vector2 GetItemPos(int index)
    {
        return new Vector2(-128 + GetPos(index).x * 32, 32 - GetPos(index).y * 32);
    }

    Vector2Int GetPos(int index)
    {
        return new Vector2Int(index % width, index / width);
    }

    int GetIndex(Vector2Int pos)
    {
        return pos.x + pos.y * width;
    }

    bool IndexValidate(int index)
    {
        return (index >= 0) && (index < list.Count);

    }

    public void GenerateList(ItemType mode, int selected, int[] equiped)
    {
        list = new List<int> { -1 };

        switch (mode)
        {
            case ItemType.Consumable:
                list.AddRange(GM.Game.PlayData.GetConsumableList());
                break;

            case ItemType.Accessory:
                list.AddRange(GM.Game.PlayData.GetAccessoryList());
                break;

            case ItemType.Spell:
                list.AddRange(GM.Game.PlayData.GetSpellList());
                break;

            case ItemType.KeyItem:
                list.AddRange(GM.Game.PlayData.GetKeyItemList());
                break;
        }

        foreach (int num in equiped)
        {
            if (num != -1)
            {
                if (num != selected)
                {
                    list.Remove(num);
                }
            }
        }

        selectedIndex = list.IndexOf(selected);
        selectedPos = GetPos(selectedIndex);
        cursor.AnchoredPos = GetItemPos(selectedIndex);

        while (items.Count < list.Count)
        {
            ItemList_Item i = Instantiate(prefab, ListTransform);
            i.Pos = GetItemPos(items.Count);
            items.Add(i);
        }

        for (int n = 0; n < list.Count; n++)
        {
            items[n].isActive = true;
        }

        if (items.Count > list.Count)
        {
            for (int n = list.Count; n < items.Count; n++)
            {
                items[n].isActive = false;
            }
        }



    }

    public void GenerateList(ItemType mode)
    {
        switch (mode)
        {
            case ItemType.Consumable:
                list.AddRange(GM.Game.PlayData.GetConsumableList());
                break;

            case ItemType.Accessory:
                list.AddRange(GM.Game.PlayData.GetAccessoryList());
                break;

            case ItemType.Spell:
                list.AddRange(GM.Game.PlayData.GetSpellList());
                break;

            case ItemType.KeyItem:
                list.AddRange(GM.Game.PlayData.GetKeyItemList());
                break;
        }

        selectedIndex = 0;
        selectedPos = GetPos(selectedIndex);

        if (list.Count > 0)
        {
            cursor.AnchoredPos = new Vector2(-200,0);
        }
        else
        {
            cursor.AnchoredPos = GetItemPos(selectedIndex);
        }

        while (items.Count < list.Count)
        {
            ItemList_Item i = Instantiate(prefab, ListTransform);
            i.Pos = GetItemPos(items.Count);
            items.Add(i);
        }

        for (int n = 0; n < list.Count; n++)
        {
            items[n].isActive = true;
        }

        if (items.Count > list.Count)
        {
            for (int n = list.Count; n < items.Count; n++)
            {
                items[n].isActive = false;
            }
        }



    }



    public void ListUP()
    {
        if (canselect)
        {
            if (IndexValidate(GetIndex(GetPos(selectedIndex) + Vector2Int.down)))
            {
                Vector2 from = GetItemPos(selectedIndex);
                selectedIndex = GetIndex(GetPos(selectedIndex) + Vector2Int.down);
                Vector2 to = GetItemPos(selectedIndex);
                StartCoroutine(MoveCursor(from, to));
            }
            else
            {
                print("invalidate");
            }
        }
    }
    public void ListRIGHT()
    {
        if (canselect)
        {
            if (IndexValidate(GetIndex(GetPos(selectedIndex) + Vector2Int.right)))
            {
                Vector2 from = GetItemPos(selectedIndex);
                selectedIndex = GetIndex(GetPos(selectedIndex) + Vector2Int.right);
                Vector2 to = GetItemPos(selectedIndex);
                StartCoroutine(MoveCursor(from, to));
            }
            else
            {
                print("invalidate");
            }
        }

    }
    public void ListLEFT()
    {
        if (canselect)
        {
            if (IndexValidate(GetIndex(GetPos(selectedIndex) + Vector2Int.left)))
            {
                Vector2 from = GetItemPos(selectedIndex);
                selectedIndex = GetIndex(GetPos(selectedIndex) + Vector2Int.left);
                Vector2 to = GetItemPos(selectedIndex);
                StartCoroutine(MoveCursor(from, to));
            }
            else
            {
                print("invalidate");
            }
        }
    }
    public void ListDOWN()
    {
        if (canselect)
        {
            if (IndexValidate(GetIndex(GetPos(selectedIndex) + Vector2Int.up)))
            {
                Vector2 from = GetItemPos(selectedIndex);
                selectedIndex = GetIndex(GetPos(selectedIndex) + Vector2Int.up);
                Vector2 to = GetItemPos(selectedIndex);
                StartCoroutine(MoveCursor(from, to));
            }
            else
            {
                print("invalidate");
            }
        }
    }

    public void PosUpdate()
    {
    }

    bool canselect = true;
    const float MoveTime = 0.1f;
    [SerializeField] AnimationCurve curve;

    IEnumerator MoveCursor(Vector2 from, Vector2 to)
    {
        canselect = false;

        bool updateflag = true;

        float timer = 0;

        while (MoveTime > timer)
        {
            timer += Time.deltaTime;

            timer = Mathf.Clamp(timer, 0, MoveTime);
            float progress = curve.Evaluate(timer / MoveTime);

            Vector2 p = Vector2.Lerp(from, to, progress);

            cursor.AnchoredPos = p;

            if (progress > 0.5f && updateflag)
            {
                updateflag = false;
            }

            yield return null;

        }
        canselect = true;
    }



    public void IconUpdate(ItemType mode)
    {
        for (int i = 0; i < list.Count; i++)
        {
            switch (mode)
            {
                case ItemType.Consumable:
                    items[i].SetSprite(GM.EDM.GET_CONSUMABLE_FUNCTION(list[i]).Sprite);
                    break;
                case ItemType.Accessory:
                    items[i].SetSprite(GM.EDM.GET_ACCESSORY_FUNCTION(list[i]).Sprite);
                    break;
                case ItemType.Spell:
                    items[i].SetSprite(GM.EDM.GET_SPELL_FUNCTION(list[i]).Sprite);
                    break;
                case ItemType.KeyItem:
                    items[i].SetSprite(GM.EDM.GET_KEYITEM_DATA(list[i]).Sprite);
                    break;
            }
        }

    }

}
