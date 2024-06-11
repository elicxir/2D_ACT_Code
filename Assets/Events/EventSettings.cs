using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MyScriptable/EventSettings/EventSettings")]
public class EventSettings : ScriptableObject
{

    [Header("�C�x���g����ɓ��������̃��b�Z�[�W")]
    public string message = "Inspect";
    public Color color = Color.white;

    [Header("�C�x���g����̑傫��")]
    public Rect Hitbox = new Rect(0, 0, 48, 32);

    [Header("�͈͓��ɓ������Ƃ��Ɏ����ŃC�x���g���J�n����邩�ǂ���")]
    public bool AutoEvent = false;

    [Header("false�̏ꍇ�v���C���[���C�x���g���ł�����\(���p����)")]
    public bool BindEvent = true;

    [Header("�v���C���[�̌������C�x���g����ɉe�����邩")]
    public bool Directional;

    [Header("�C�x���g���L���łȂ��Ă��\������邩(�X�C�b�`�Ȃǂł̓I���ɂ���)")]
    public bool ShowOnDeactivated = true;


}
