using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MyScriptable/Create EnemyPlaceStatusData")]

public class EnemyPlaceStatusData : ScriptableObject
{
    [Header("ŒÄ‚Ño‚·“G‚Ìİ’è")]

    public EnemyManager.EnemyName enemyName;//“G‚Ìí‘°–¼
    [Range(0, 6)] public int VariantIndex = 0;//“¯‚¶í‘°‚Ì’†‚Å‚ÌŒ`®ˆá‚¢

    [Header("“G‚Ìó‘Ôİ’è")]

    public bool facePlayer;
    public Entity.FaceDirection face;

    public Rect BehaviorRect = new Rect(0, 0, 100, 48);
}
