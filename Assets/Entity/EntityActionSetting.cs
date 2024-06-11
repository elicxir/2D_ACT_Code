using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MyScriptable/Create EntityActionSetting")]
public class EntityActionSetting : ScriptableObject
{
    public AC_set[] ac_Sets;
    public HitBox_set[] hitbox_set;

    public Vector2 offset;
}
