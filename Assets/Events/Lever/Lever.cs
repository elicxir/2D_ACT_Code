using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Gimmick
{
    enum Dir
    {
        WALL_LEFT, WALL_RIGHT, GROUND_LEFT, GROUND_RIGHT
    }

    [SerializeField] Dir dir = Dir.WALL_LEFT;

    public string LeverID
    {
        get
        {
            return "Lever" + transform.position;
        }
    }
    [SerializeField] GameObject Hand;

    private void OnValidate()
    {
        switch (dir)
        {
            case Dir.WALL_LEFT:
                this.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
                break;

            case Dir.WALL_RIGHT:
                this.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
                this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
                break;

            case Dir.GROUND_LEFT:
                this.gameObject.transform.localEulerAngles = new Vector3(0, 0, 90);
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
                break;

            case Dir.GROUND_RIGHT:
                this.gameObject.transform.localEulerAngles = new Vector3(0, 0, -90);
                this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
                break;

            default:
                break;
        }

        GraphUpdate(Angle);
    }

    [Range(0, 1)]
    public float Angle = 0;

    public void GraphUpdate(float angle)
    {
        Angle = Mathf.Clamp(angle, 0, 1);
        Hand.transform.localEulerAngles = new Vector3(0, 0, -90 * Angle);
    }

}
