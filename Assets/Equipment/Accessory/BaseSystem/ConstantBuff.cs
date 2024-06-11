using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������̓v���C���[�ɍP��I�ȃo�t��t�^����
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
