using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
[CreateAssetMenu(menuName = "MyScriptable/SpriteObjectData/Create SpriteObjectData")]

public class SpriteObjectData : ScriptableObject
{
    protected SpriteObjectManager SOM
    {
        get
        {
            return GM.Game.mainGame.spriteObjectManager;
        }
    }


    [SerializeField] protected Color color=Color.white;

    protected LayerMask TerrainMask = 1 << 8;
    public virtual Sprite UpdateSprite(float time,SpriteObject so)
    {
        so.spriteRenderer.color = color;
        return sprite;
    }

    public Sprite sprite;

    public enum Terrain
    {
        Through,//�n�`���ђʂ���
        NotThrough,//�n�`�ɓ�����Ə�����
    }
    public Terrain terrainMode;



    public enum GeneratePrefab
    {
        SO,//�ʏ�̂���
        Specific,
    }
    public GeneratePrefab generatePrefab;
    public ActiveMode activeMode;

    public enum LayerMode
    {
        FrontPlayer,
        BehindPlayer
    }

    public LayerMode layerMode;

    public Vector2 Accelerate;


    public virtual IEnumerator EndCoroutine(SpriteObject so)
    {
        so.Deactivate();
        yield break;

    }

    public float DeactiveTimer = 5;

    public virtual bool DeactivateFunction(SpriteObject so)
    {
        switch (activeMode)
        {
            case ActiveMode.NoLoop:
                break;
            case ActiveMode.OutOfScreen:
                return !so.spriteRenderer.isVisible;
                
            case ActiveMode.OutOfSection:

                break;

            case ActiveMode.Terrain:
                return Physics2D.OverlapBox(so.Position, so.spriteRenderer.bounds.size*0.8f, so.transform.eulerAngles.z, TerrainMask);

            case ActiveMode.Timer:
                return so.OwnTimeSiceStart > DeactiveTimer;
            default:
                break;
        }

        return false;
    }

    public virtual void SpecificMove(SpriteObject so)
    {

    }
}

[System.Serializable]
public class SpriteAnimationData
{
    public Sprite[] Sprites;

    /// <summary>
    /// Speed:1�b�Ԃɐi�ރR�}��
    /// </summary>
    [Range(1, 20)]
    public int Speed = 1;


}


public enum ActiveMode
{
    NoLoop,//�A�j���[�V�������ꏄ�����i�K�ŏ���
    OutOfScreen,//��ʊO�ɏo��Ə���
    OutOfSection,//�Z�N�V�����O�ɏo��Ə���
    Terrain,//�n�`�Փ˂ŏ���
    Timer,//���Ԃŏ���
    Specofic,//
}
