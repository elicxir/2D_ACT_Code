using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    [SerializeField] BoxCollider2D Box;
    [SerializeField] Color color;

    int state;
    /*0:緑 プレイヤー 
      1:赤 敵のパーツ 
      2:青 プレイヤー無敵
      3:黄 敵攻撃
      4:紫 プレイヤー攻撃
      5:白 イベント
      6: 
    */


    // Update is called once per frame
    private void OnDrawGizmos()
    {
        
        Gizmos.color = color; //色指定

        Gizmos.DrawCube(Box.offset+new Vector2(this.transform.position.x, this.transform.position.y), Box.size); //中心点とサイズ
    }
}
