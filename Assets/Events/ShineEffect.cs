using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShineEffect : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] Sprite[] Shine;

    float counter=0;

    [SerializeField] Event WatchedEvent;

    private void LateUpdate()
    {
        if (WatchedEvent.EventFlag())
        {
            counter += Time.deltaTime * 8;

            spriteRenderer.sprite = Shine[Mathf.FloorToInt(counter) % 8];

        }
    }
}
