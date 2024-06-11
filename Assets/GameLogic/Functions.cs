using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using UnityEngine.Assertions;

namespace Functions
{
    namespace GameInput
    {
        public class Input
        {

        }
    }

    public class Function
    {
       public bool LogicalDisjunction(bool[] data)
        {
            bool flag = false;

            foreach(bool item in data)
            {
                if (item == true)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        public bool LogicalProduct(bool[] data)
        {
            bool flag = true;

            foreach (bool item in data)
            {
                if (item == false)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }


        public int RangeInt(int val,int min,int max)
        {
            return Mathf.Min(Mathf.Max(min, val), max);
        }
        public float RangeFloat(float val, float min, float max)
        {
            return Mathf.Min(Mathf.Max(min, val), max);
        }


        public string Line_System(string word)
        {
            string character2 = null;
            string character = word;

            string[] part = character.Split('@');

            for (int q = 0; q < part.Length; q++)
            {
                character2 += part[q] + "\n";
            }
            return character2;
        }

        public int BoolInt(bool a)
        {
            if (a)
            {
                return 1;
            }
            else
            {
                return 0;

            }
        }

        public bool IntBool(int a)
        {
            if (a == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public float DirToRad(bool isRight)
        {
            float dir;
            if (isRight)
            {
                dir = 0;
            }
            else
            {
                dir = 180;

            }

            return dir * Mathf.Deg2Rad;
        }

        public Vector3 V2ToV3(Vector2 v2)
        {

            return new Vector3(v2.x, v2.y, 0);
        }
        public Vector2 V3ToV2(Vector3 v3)
        {
            return new Vector2(v3.x, v3.y);
        }
    }
}

