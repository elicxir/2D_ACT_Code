using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MemoText", menuName = "ScriptableObjects/MemoText")]
public class MemoText : ScriptableObject
{
    public enum MemoType
    {
        Tutorial,
        Quest,
        Note,
    }
    public MemoType memoType;

    public string title;

    [TextArea(6, 7)]
    public string content;  


}
