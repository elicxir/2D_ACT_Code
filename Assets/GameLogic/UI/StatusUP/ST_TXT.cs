using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ST_TXT : MonoBehaviour
{
    //[SerializeField] TextMeshProGeometryAnimator Startanimator;
    //[SerializeField] TextMeshProGeometryAnimator Endanimator;

    public void INIT()
    {
       // Endanimator.enabled = false;
       // Startanimator.enabled = false;
    }

    [ContextMenu("data")]
    public void ST()
    {
       // Endanimator.enabled = false;
       // Startanimator.enabled = true;
       //// Startanimator.Play();
    }

    [ContextMenu("data2")]
    public void ED()
    {
       // Startanimator.enabled = false;
      //  Endanimator.enabled = true;

       // Endanimator.Play();
    }

}
