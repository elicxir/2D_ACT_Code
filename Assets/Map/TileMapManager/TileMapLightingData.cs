using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Experimental.Rendering.Universal;

[CreateAssetMenu(menuName = "MyScriptable/TileMap/TileMapLightingData")]
//�^�C���}�b�v�̓���̎�ނ̃^�C���ɑ΂���2D���C�g��ݒu���邽�߂̃f�[�^
public class TileMapLightingData : ScriptableObject
{
    public TileBase tile;
    public TileMapLighting light;

    public Vector2 offset;
}
