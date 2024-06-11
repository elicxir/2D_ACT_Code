using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create PlayerActionValues")]

public class PlayerActionValues : ScriptableObject
{
    public float WalkAcceleration;
    public float StopAcceleration;
    public float JumpPower;
    public float AirAcceleration;
    public float AirStopAcceleration;
    public float GroundBaseSpeed;
    public float AirBaseSpeed;
}
