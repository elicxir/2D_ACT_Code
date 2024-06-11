using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/SpriteAnimation")]

public class SpriteAnimation : ScriptableObject
{
    public SpriteAnimationData data;

    public Sprite UpdateSprite(float time)
    {
        return data.Sprites[Mathf.FloorToInt(time * data.Speed)% data.Sprites.Length];
    }
}
