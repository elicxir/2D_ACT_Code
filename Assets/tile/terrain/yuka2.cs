using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yuka2 : MonoBehaviour
{
    BoxCollider2D c;
    float timer = 0.0f;
    bool flag = false;

    private void Start()
    {
        c = GetComponent<BoxCollider2D>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer <= 0.0f)
        {
            if (Input.GetAxisRaw("Vertical") < 0&&flag)
            {
                c.enabled = false;
                timer = 0.20f;

            }
            else
            {
                c.enabled = true;
            }
        }  
        else
        {
            timer -= Time.fixedDeltaTime;

        }




    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            flag = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            flag = false;
        }
    }
}
