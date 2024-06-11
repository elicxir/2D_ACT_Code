using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Equipment/ArtsData")]

public class ArtsData : ScriptableObject
{
    //�Z�̖��O
    public string artsname;

    //�Z�̉摜
    public Sprite Sprite;

    public float castspeed = 1;

    //�Z�̊ȈՐ���
    [TextArea] public string desc;

    //�Z�̃t���[�o�[�e�L�X�g
    [TextArea] public string flavor;
}
