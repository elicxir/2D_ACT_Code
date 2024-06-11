using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circlemarker : MonoBehaviour
{
    public int radius = 0;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
