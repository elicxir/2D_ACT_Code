using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�Ώۂ̃C�x���g��EventFlag�ɉ�����
public class Switch : Gimmick
{
    public override int UpdateRange => 80;

    [SerializeField] GameObject Switchable;//EventFlag�ɂ���ăA�N�e�B�u��Ԃ��ς��
    [SerializeField] Event Flag;

    public override bool Updater()
    {
        if (Flag.EventFlag())
        {
            if (Switchable != null)
            {
                if (!Switchable.activeSelf)
                {
                    Switchable.SetActive(true);
                    return true;
                }
            }

        }
        else
        {
            if (Switchable != null)
            {
                if (Switchable.activeSelf)
                {
                    Switchable.SetActive(false); return true;

                }
            }
        }

        return false;



    }
}
