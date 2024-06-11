using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EntityやProjectileなどの時間を伴うすべてのオブジェクトについて必ずTimeManagerを用いる。
public class TimeManager : MonoBehaviour
{
    /// <summary>
    /// Update,FixedUpdateの実行間隔は変わらない
    /// </summary>

    public float TimeMult {
        get
        {
            return TimeMultVar;
        }
        set
        {
            TimeMultVar = value;
        }

    }
    float TimeMultVar=1;

    public float MultDeltaTime
    {
        get
        {
            return TimeMult * Time.deltaTime;

        }
    }

}
