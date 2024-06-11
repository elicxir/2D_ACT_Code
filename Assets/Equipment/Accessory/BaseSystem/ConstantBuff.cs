using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装備中はプレイヤーに恒常的なバフを付与する
/// </summary>
public class ConstantBuff : AccessoryFunction
{
    [SerializeField] Buff buff;

    protected override void OnValidate()
    {
        base.OnValidate();

        buff.ID = Name;
        buff.timer = 0.016f;
    }



    public override void Passive()
    {
        PM.Player.buffManager.ADD(buff);
    }
}
