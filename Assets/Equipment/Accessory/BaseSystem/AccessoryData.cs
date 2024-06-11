using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Equipment/AccessoryData")]

public class AccessoryData : ScriptableObject
{
    //�A�C�e���̖��O
    public string consumablename;
    //�A�C�e���̉摜
    public Sprite Sprite;
   
    //�A�C�e���̊ȈՐ���
    [TextArea] public string desc;
    //�A�C�e���̃t���[�o�[�e�L�X�g
    [TextArea] public string flavor;
}
