using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve_v2 : MonoBehaviour
{
    [SerializeField] Transform[] c_p;

    Vector2[] pos
    {
        get {

            Vector2[] res=new Vector2[c_p.Length];
            for(int a=0;a<res.Length;a++)
            {
                res[a] = c_p[a].position;
            }
            return res;
             }
    }

    BezierCurve bezierCurve;

    [SerializeField]LineRenderer l;

    [SerializeField] int devideNUM;

    [ContextMenu("Test")]
    void test()
    {
        print("called");
        bezierCurve=new BezierCurve(pos, devideNUM);
        l.positionCount = bezierCurve.GetPoints3.Length;
        l.SetPositions(bezierCurve.GetPoints3);

        //StartCoroutine("Moving");

    }

    [SerializeField] Transform obj;

    public IEnumerator Moving()
    {
        print("Start");
        float sum = 0;
        while (true)
        {
            obj.position=bezierCurve.GoAlong(sum);
            print(obj.position);
            sum += 0.2f;

            if(sum>= bezierCurve.length)
            {
                yield break;
            }

            yield return new WaitForSecondsRealtime(0.1f);

        }
    }


}
