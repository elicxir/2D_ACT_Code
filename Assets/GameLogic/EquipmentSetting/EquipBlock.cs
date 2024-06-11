using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EquipBlock : MonoBehaviour
{
    [SerializeField] Image waku;

    [SerializeField] Image image;
    [SerializeField] Text equipname;
    [SerializeField] Text description;

    [SerializeField] RectTransform Rect;

    [SerializeField] CanvasGroup canvas;



    public bool selected
    {
        set
        {
            if (value)
            {
                waku.color = Color.green;

            }
            else
            {
                waku.color = Color.gray;
            }
        }

    }
    /// <summary>
    /// 0:item
    /// 1:spell
    /// 2:accessory
    /// </summary>
    public int mode
    {
        get
        {
            return mode2;
        }
        set
        {
            if (value >= 0 && value <3)
            {
                mode2 = 0;
            }
            else if (value >= 3 && value < 5)
            {
                mode2 = 1;
            }
            else if (value >= 5 && value < 7)
            {
                mode2 = 2;
            }
        }
    }

    int mode2 = 0;


    public bool active
    {
        set
        {
            if (value)
            {
                canvas.alpha = 1;
            }
            else
            {
                canvas.alpha = 0;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public int index
    {
        set
        {
            switch (mode)
            {
                case 0:

                    ConsumableFunction data = Define.EDM.GET_CONSUMABLE_FUNCTION(value);

                    if (data != null)
                    {
                        image.sprite = data.Sprite;
                        equipname.text = data.consumablename;
                        //description.text = data.description1;
                    }

                    break;

                case 1:

                    SpellFunction data1 = Define.EDM.GET_SPELL_FUNCTION(value);

                    if (data1 != null)
                    {
                        image.sprite = data1.Sprite;
                        equipname.text = data1.spellname+" "+data1.StringType;
                        description.text = data1.description;
                    }
                    break;

                case 2:


                    AccessoryFunction data2 = Define.EDM.GET_ACCESSORY_FUNCTION(value);

                    if (data2 != null)
                    {
                        image.sprite = data2.Sprite;
                        equipname.text = data2.accessoryname;
                        description.text = data2.description;
                    }
                    break;

            }
        }
    }

    public bool show
    {
        set
        {
            active = value;
            
        }
    }


}
