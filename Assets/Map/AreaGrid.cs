using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConsts;

public class AreaGrid : MonoBehaviour
{
    [SerializeField] Color color;

    int width = 80;
    int height = 40;

    [SerializeField] Vector2Int leftdown;
    [SerializeField] Vector2Int size=new Vector2Int(5,5);


    private void OnDrawGizmos()
    {
        Gizmos.color = color;

        for (int w = leftdown.x; w < leftdown.x+size.x; w++)
        {
            for (int h = leftdown.y; h < leftdown.y + size.y; h++)
            {
                Gizmos.DrawCube(new Vector2(Map.tile_size * Map.width * w, Map.tile_size * Map.height * h) + new Vector2(Map.tile_size * Map.width, Map.tile_size * Map.height) * 0.5f, new Vector2(Map.tile_size * Map.width, Map.tile_size * Map.height));
            }
        }

    }





}
