using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// unity2D�p�x�W�F�Ȑ��N���X
/// BezzierCurve�F������ ����_�ƕ�������^���ăx�W�F�Ȑ��𐶐�
/// GetPoints�F���������Ȑ��̒��_�z��
/// GetPoints3�F��Ɠ���(�������������Vector3)
/// GetPoint(n)�Fn�Ԗڂ̒��_���W
/// length�F���������x�W�F�Ȑ��̒���
/// GoAlong(l)�F�Ȑ��ɉ�����l�����i�񂾈ʒu�̍��W
/// Cal(t)�Ft(0-1)�ɑΉ���������ʒu�̍��W
/// </summary>
/// 
//�v�Z�ʓI��5���ȏ�͂ނ�݂₽��Ɏg���ׂ��ł͂Ȃ�
public class BezierCurve
{
    public void DrawGizmos()
    {
        Gizmos.color = Color.white;

        for(int i = 1; i < Point.Length; i++)
        {
            Gizmos.DrawLine(GetPoint(i), GetPoint(i-1));
        }
    }

    /// <summary>
    /// ����_�ƕ�������^���ăx�W�F�Ȑ����v�Z����
    /// </summary>
    public BezierCurve(Vector2[] c_points, int devide)
    {
        ControllPoint = c_points;
        Point = new Vector2[devide + 1];
        Devider();
        length = Length();
    }
    public BezierCurve(Transform[] c_points, int devide)
    {
        Vector2[] res = new Vector2[c_points.Length];
        for (int i = 0; i < res.Length; i++)
        {
            res[i] = c_points[i].position;
        }

        ControllPoint = res;
        Point = new Vector2[devide + 1];
        Devider();
        length = Length();
    }







    Vector2[] ControllPoint;
    Vector2[] Point;
    public float length;

    public Vector2[] GetPoints
    {
        get
        {
            return Point;
        }
    }
    public Vector3[] GetPoints3
    {
        get
        {
            Vector3[] v3 = new Vector3[Point.Length];
            
            for(int i = 0; i < v3.Length; i++)
            {
                v3[i] = new Vector3(Point[i].x, Point[i].y, 0);
            }
            return v3;
        }
    }

    public Vector2 GetPoint(int index)
    {
        index = Mathf.Max(Mathf.Min(index,Point.Length-1), 0);

        return Point[index];
    }

    public float GetRotationPoint(int index)
    {
        index = Mathf.Clamp(index, 1, Point.Length - 2);

        Vector2 v = Point[index + 1] - Point[index - 1];

        float a = Vector2.Angle(Vector2.right, v);

        if (v.y > 0)
        {
            return a;
        }
        else
        {
            return -a;
        }

    }

    public float GetRotationSide(float l)
    {
        if (l <= 0)
        {
            return 0;
        }
        else if (l >= length)
        {
            return 0;
        }

        float val = l;
        int i = 0;

        while (true)
        {
            float nextlength = (Point[i + 1] - Point[i]).magnitude;

            if (val >= nextlength)
            {
                val -= nextlength;
                i++;
            }
            else
            {
                return Vector2.SignedAngle(Vector2.right, Point[i+1]-Point[i]);
            }

        }
    }



    void Devider()
    {
        for(int q = 0; q < Point.Length; q++)
        {
            Point[q] = Cal((float)q / (Point.Length - 1));
        }
    }

    public Vector2 Cal(float t)
    {
        Vector2[] data=ControllPoint;
        Vector2[] results;

        t = Mathf.Max(Mathf.Min(t, 1), 0);

        while (true)
        {
            results = new Vector2[data.Length - 1];

            for (int j = 0; j < results.Length; j++)
            {
                results[j] = Vector2.Lerp(data[j], data[j + 1], t);
            }

            data = results;

            if (data.Length == 1)
            {
                return data[0];
            }
        }
    }

    float Length()
    {
        float sum = 0;
        for(int i = 0; i < Point.Length-1; i++)
        {
            sum += (Point[i + 1] - Point[i]).magnitude;
        }
        return sum;
    }

    public Vector2 GoAlong(float l)
    {
        if (l <= 0)
        {
            return Point[0];
        }
        else if (l >= length)
        {
            return Point[Point.Length-1];
        }

        float val = l;
        int i = 0;

        while (true)
        {
            float nextlength= (Point[i + 1] - Point[i]).magnitude;

            if (val >=nextlength)
            {
                val -= nextlength;
                i++;
            }
            else
            {
                return Vector2.Lerp(Point[i],Point[i+1],val/nextlength);
            }

        }

    }
}



