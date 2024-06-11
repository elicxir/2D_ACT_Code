using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    public GameObject P;
    public GameObject CAM;

    public int y_min=0;
    public int y_max;

    Transform pos;
    Transform pos2;
    Transform c_pos;

    Vector2 aaaa;


    // Start is called before the first frame update
    void Start()
    {
        pos = transform;
        pos2 = P.transform;
        c_pos = CAM.transform;
    }

    // Update is called once per frame
    void Update()
    {
        

        aaaa.x = c_pos.position.x;
        aaaa.y = 160;
          //  -(40*pos2.position.y) / (y_max - y_min);

        pos.position = aaaa;
    }
}
