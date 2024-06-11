using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionPanel : MonoBehaviour
{
    [SerializeField]CanvasGroup cg1;
    [SerializeField] CanvasGroup cg2;

    [SerializeField] Lean.Gui.LeanToggle cg;

    public void On()
    {
        cg2.alpha = 1;
    }

    public void Off()
    {
        cg2.alpha = 0;

    }

    public void Transition()
    {
        
        cg.TurnOn();
        Invoke("sOff", 0.05f);
        //cg.TurnOff();
    }

    void sOff()
    {
        cg.TurnOff();
    }


    public void Trans()
    {
        StartCoroutine(AREA_TRANS_OFF());
    }

     IEnumerator AREA_TRANS_OFF()
    {
        cg1.alpha = 1;
        while (cg1.alpha > 0)
        {
            cg1.alpha -= Time.fixedDeltaTime*1.2f;
            yield return new WaitForFixedUpdate();
        }

        cg1.alpha = 0;
        yield break;
    }
}
