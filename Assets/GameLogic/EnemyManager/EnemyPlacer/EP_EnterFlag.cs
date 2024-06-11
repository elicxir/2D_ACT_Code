using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

//エネミー設置情報:エリア進入時に一度だけ生成される
public class EP_EnterFlag : EnemyPlacer
{
    /// <summary>
    /// このフラグがtrueのときのみ敵を出す
    /// </summary>
    public string FlagName;
    public bool reverseMode;//trueならばfalseの時のみ出る

    public bool BossFlag;//trueならばUIのboss体力に登録される

    bool isFlagTrue
    {
        get
        {
            return GM.Game.PlayData.GetGameFlag(FlagName).isTrue!= reverseMode;
        }
    }

    protected override void InitFunction()
    {
        if (isFlagTrue)
        {
            foreach (EnemyPlaceData epd in enemyPlaceDatas)
            {
                if (epd.enemyMarker != null)
                {
                    GM.UI.BossHP.Boss.Add(Place(epd, epd.enemyMarker.Position));

                }
                else
                {
                    GM.UI.BossHP.Boss.Add(Place(epd));

                    
                }
            }
        }
    }
}
