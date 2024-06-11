using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/SpriteObjectData/Create SOD_Animate")]


public class SOD_Animate : SpriteObjectData
{

    public override bool DeactivateFunction(SpriteObject so)
    {
        switch (activeMode)
        {
            case ActiveMode.NoLoop:
                return so.OwnTimeSiceStart >= StartTime + MiddleTime;

            case ActiveMode.OutOfScreen:
                return !so.spriteRenderer.isVisible;

            case ActiveMode.OutOfSection:
                break;

            case ActiveMode.Terrain:
                return false;

            case ActiveMode.Timer:
                return so.OwnTimeSiceStart > DeactiveTimer;
            default:

                break;
        }
        return false;
    }

    public override Sprite UpdateSprite(float time, SpriteObject so)
    {
        so.spriteRenderer.color = color;

        if (so.isActivated)
        {
            if (time < StartTime)
            {
                return Start.Sprites[Mathf.FloorToInt(time * Start.Speed)];
            }
            else
            {
                float t2 = time - StartTime;
                return Middle.Sprites[Mathf.FloorToInt(t2 * Middle.Speed) % Middle.Sprites.Length];
            }
        }
        else
        {
            float t2 = Mathf.Clamp(time, 0, EndTime - float.Epsilon);
            return End.Sprites[Mathf.FloorToInt(t2 * End.Speed)];
        }


    }

    public SpriteAnimationData Start;
    public SpriteAnimationData Middle;
    public SpriteAnimationData End;

    public float StartTime;
    public float MiddleTime;
    public float EndTime;

    private void OnValidate()
    {
        SetData(ref StartTime, Start);
        SetData(ref MiddleTime, Middle);
        SetData(ref EndTime, End);
    }

    void SetData(ref float t, SpriteAnimationData d)
    {
        if (d.Sprites.Length > 0)
        {
            t = (float)d.Sprites.Length / d.Speed;
        }
        else
        {
            t = 0;
        }
    }

    public override IEnumerator EndCoroutine(SpriteObject so)
    {
        float timer=0;

        while (timer < EndTime)
        {
            UpdateSprite(timer,so);

            timer += so.OwnDeltaTime;
            yield return null;
        }

        so.Deactivate();
    }

}
