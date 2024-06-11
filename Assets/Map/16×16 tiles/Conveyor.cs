using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : Gimmick
{
    enum Mode
    {
        Right,// 4dot/s
        Left,
        RightFast,
        LeftFast,
    }
    [SerializeField] Mode mode;

   
}
