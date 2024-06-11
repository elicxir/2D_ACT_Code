using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] Lean.Gui.LeanToggle Toggle;

    int select_genre;//0:チュートリアル　1:アイテム　2:物語
    int select_genre2;//0:チュートリアル　1:アイテム　2:物語




    public void REFLESH()
    {
        
            genre_select();

            //Value_Change();
            //VALUE();
            /*
            if (Define.IM.ButtonDown("start"))
            {
                ExitMenu();
            }
        */

    }

    public void EnterMenu()
    {
        Toggle.TurnOn();
       // Define.GM.State(9);
    }

    public void ExitMenu()
    {
        Toggle.TurnOff();
       // Define.GM.State(7);

    }

    void genre_select()
    {/*
        if (Define.IM.ButtonDown("Horizontal2"))
        {
            select_genre2++;
        }
        else if (Define.IM.ButtonDown("Horizontal2"))
        {
            select_genre2--;
        }
        */
        select_genre = select_genre2 % 3;


    }


}

