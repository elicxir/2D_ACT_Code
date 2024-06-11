using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �C�x���g�̌����ڂ��A�j���[�V����������X�N���v�g
/// </summary>
public class Event_Animation : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] Sprite[] Animation;

    public float animationspeed=8;

    float counter = 0;

    [SerializeField] Event WatchedEvent;

    public void Updater()
    {
        if (WatchedEvent.EventFlag())
        {
            counter += Time.deltaTime * animationspeed;

            PosFunction(counter);

            spriteRenderer.sprite = Animation[Mathf.FloorToInt(counter) % Animation.Length];
        }
    }

    public virtual void PosFunction(float counter)
    {

    }

}
