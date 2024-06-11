using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    [Header("フェードスピード")] public float speed = 1.0f;
    [Header("上昇量")] public float moveDis = 0.0f;
    [Header("時間")] public float moveTime = 0.5f;
    [Header("キャンバスグループ")] public CanvasGroup cg;

    private bool isEnter = false;


    private Vector3 defaltPos;
    private float timer = 0.0f;
    Canvas canvas;

    private void Start()
    {
        //初期化
        if (cg == null)
        {
            Destroy(this);
        }
        else
        {
            cg.alpha = 0.0f;
            defaltPos = cg.transform.position;
            cg.transform.position = defaltPos - Vector3.up * moveDis;
            canvas = this.transform.Find("Canvas").GetComponent<Canvas>();
        }
    }

    private void Update()
    {
        //プレイヤーが範囲内に入った
        if (isEnter)
        {
            canvas.enabled = true;
            //上昇しながらフェードインする
            if (cg.transform.position.y < defaltPos.y || cg.alpha < 1.0f)
            {
                cg.alpha = timer / moveTime;
                cg.transform.position += Vector3.up * (moveDis / moveTime) * speed * Time.deltaTime;
                timer += speed * Time.deltaTime;
            }
            //フェードイン完了
            else
            {
                cg.alpha = 1.0f;
                cg.transform.position = defaltPos;
            }
        }
        //プレイヤーが範囲内にいない
        else
        {
            //下降しながらフェードアウトする
            if (cg.transform.position.y > defaltPos.y - moveDis || cg.alpha > 0.0f)
            {
                cg.alpha = timer / moveTime;
                cg.transform.position -= Vector3.up * (moveDis / moveTime) * speed * Time.deltaTime;
                timer -= speed * Time.deltaTime;
            }
            //フェードアウト完了
            else
            {
                timer = 0.0f;
                cg.alpha = 0.0f;
                cg.transform.position = defaltPos - Vector3.up * moveDis;
                canvas.enabled = false;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEnter = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEnter = false;
        }
    }
}