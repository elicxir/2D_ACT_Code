using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;

public class EC_SystemMessage : EventCommand
{
    [TextArea] [SerializeField] string text;
    [SerializeField] Color color=Color.white;

    public override IEnumerator Command()
    {
        GM.UI.UI_Warning.SHOW_WARNING(text, color);
        yield return new WaitForSeconds(1.6f);
    }


}
