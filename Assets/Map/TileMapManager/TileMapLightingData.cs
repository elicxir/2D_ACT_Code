using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Experimental.Rendering.Universal;

[CreateAssetMenu(menuName = "MyScriptable/TileMap/TileMapLightingData")]
//タイルマップの特定の種類のタイルに対して2Dライトを設置するためのデータ
public class TileMapLightingData : ScriptableObject
{
    public TileBase tile;
    public TileMapLighting light;

    public Vector2 offset;
}
