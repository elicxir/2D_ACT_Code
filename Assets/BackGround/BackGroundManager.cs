using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class BackGroundManager : MonoBehaviour
{

    [SerializeField] GameObject backGroundPrefab;

    Vector2 getcamera
    {
        get
        {
            return GM.Game.Camera.Position;
        }
    }
    /*
    private void LateUpdate()
    {
        backGroundPrefab.transform.position= getcamera;
    }*/

}
