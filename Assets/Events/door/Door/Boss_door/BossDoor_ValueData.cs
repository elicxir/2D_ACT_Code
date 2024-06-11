using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/BossDoor_ValueData")]

public class BossDoor_ValueData : ScriptableObject
{
    public float animate_timer;
    public float open_timer;

    public float close_wait;
    public float close_timer;

    public Sprite[] sprite;

    public Boss_Door boss_door;

    public AnimationCurve open_curve;
    public AnimationCurve close_curve;
}
