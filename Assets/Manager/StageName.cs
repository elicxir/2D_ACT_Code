using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageName : MonoBehaviour
{
    IEnumerator stage;

    [SerializeField]Lean.Gui.LeanToggle StageNameToggle;
    [SerializeField]Text text;

    [SerializeField] CanvasGroup cg;

    [SerializeField] AnimationCurve cc;

    // Start is called before the first frame update
    void Start()
    {
       
        stage = SHOW_NAME();

    }

    public void Show(string stagename)
    {
        text.text = stagename;
        StopCoroutine(stage);
        stage = SHOW_NAME();
        StartCoroutine(stage);
    }



    IEnumerator SHOW_NAME()
    {
        StageNameToggle.TurnOff();
        StageNameToggle.TurnOn();
        StageNameToggle.TurnOff();

        //text.transform.position = new Vector3(240* Define.GM.GAME_SIZE(), 190* Define.GM.GAME_SIZE(), 0);
        cg.alpha = 0;

        yield return new WaitForSeconds(0.35f);
        StageNameToggle.TurnOn();
        yield return new WaitForSeconds(2.0f);
        StageNameToggle.TurnOff();

    }

}
