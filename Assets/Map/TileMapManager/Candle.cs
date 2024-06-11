using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : TileMapLighting
{
    public int inner_min;
    public int inner_max;

    float T = 2;

    public override void Updater(float time)
    {
        light2D.pointLightInnerRadius = (inner_min+ inner_max)/2 + (inner_max - inner_min) * 0.5f * Mathf.Sin(2 * Mathf.PI * time / T);
    }
}
