using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EachAreaTileMap : MonoBehaviour
{
    Tilemap[] tilemap;

    public float animationspeed
    {
        set
        {
            foreach(Tilemap tm in tilemap)
            {
                tm.animationFrameRate = value;
            }
        }
    }
}
