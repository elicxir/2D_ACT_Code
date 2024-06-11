using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConsts;

public class MapGrid : MonoBehaviour
{
    [SerializeField] Color gridline;
    [SerializeField] Color gridline2;

    [SerializeField] Color[] areacolor = new Color[12];

    [SerializeField] int width = 64;
    [SerializeField] int height = 32;


    private void OnDrawGizmos()
    {
        {
            for (int w = 0; w <= 1600; w++)
            {
                    Gizmos.color = new Color(0.5f,0.5f,0.5f,0.25f);

                Vector2 point1 = new Vector2(16 * w, 0);
                Vector2 point2 = new Vector2(16 * w, Map.tile_size * Map.height * height);
                Gizmos.DrawLine(point1, point2);
            }
            for (int h = 0; h <= 800; h++)
            {
                Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.25f);

                Vector2 point1 = new Vector2(0, 16 * h);
                Vector2 point2 = new Vector2(Map.tile_size * Map.width * width, 16 * h);
                Gizmos.DrawLine(point1, point2);

            }

        }






        for (int w = 0; w <= width; w++)
        {
            if (w % 5 == 0)
            {
                Gizmos.color = gridline2;
            }
            else
            {
                Gizmos.color = gridline;
            }

            Vector2 point1 = new Vector2(Map.tile_size * Map.width * w, 0);
            Vector2 point2 = new Vector2(Map.tile_size * Map.width * w, Map.tile_size * Map.height * height);
            Gizmos.DrawLine(point1, point2);
        }
        for (int h = 0; h <= height; h++)
        {
            if (h % 5 == 0)
            {
                Gizmos.color = gridline2;
            }
            else
            {
                Gizmos.color = gridline;
            }
            Vector2 point1 = new Vector2(0, Map.tile_size * Map.height * h);
            Vector2 point2 = new Vector2(Map.tile_size * Map.width * width, Map.tile_size * Map.height * h);
            Gizmos.DrawLine(point1, point2);

        }
    }

}

