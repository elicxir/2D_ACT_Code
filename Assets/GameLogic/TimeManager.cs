using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Entity��Projectile�Ȃǂ̎��Ԃ𔺂����ׂẴI�u�W�F�N�g�ɂ��ĕK��TimeManager��p����B
public class TimeManager : MonoBehaviour
{
    /// <summary>
    /// Update,FixedUpdate�̎��s�Ԋu�͕ς��Ȃ�
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
