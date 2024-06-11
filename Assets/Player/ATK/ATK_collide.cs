using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//攻撃の当たり判定をやり取りする。

public class ATK_collide : MonoBehaviour
{
    [SerializeField] bool player_attack;//0:プレイヤーのもの　1:敵のもの
    [SerializeField] bool pierce;//地形を貫通するかどうか

    [SerializeField] int hitcount=2;//ヒット回数

    string[] hitlist=null;//すでに命中したものの名前のリスト

    int damage=50;//ダメージ


    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "terrain"|| collision.tag == "MoveFloor")
        {
            if (!pierce)
            {
                Destroy(this.gameObject);
            }
        }

        if (collision.tag == "Player")
        {

        }

        if (collision.tag == "enemyparts")
        {          
        }
    }
}
