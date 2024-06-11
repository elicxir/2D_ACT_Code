using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Equipment/ConsumableData")]
public class ConsumableData : ScriptableObject
{
    //�A�C�e���̖��O
    public string consumablename;
    //�A�C�e���̉摜
    public Sprite Sprite;
    //�A�C�e���̎g�p��
    public int MaxUseCount;
    //�A�C�e���̊ȈՐ���
    [TextArea]public string desc;
    //�A�C�e���̃t���[�o�[�e�L�X�g
    [TextArea]public string flavor;

}
