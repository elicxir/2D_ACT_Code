using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//宝箱→取得後も残骸が残るタイプ
public class TreasureBox : Event
{

    new string message = "Inspect";


    public override bool EventFlag()
    {
        return true;
    }




    
}



/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//宝箱→取得後も残骸が残るタイプ
public class TreasureBox : Event
{

    new string message = "Inspect";


    public override bool EventFlag()
    {
        return true;
    }

    public override void OnActivated()
    {

    }



    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Define.PM.nowevent.SetEvent(type, num);

            this.gameObject.SetActive(!Define.PM.FLAG(type, num));

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Define.PM.nowevent.UnsetEvent();
        }
    }

    
}
*/