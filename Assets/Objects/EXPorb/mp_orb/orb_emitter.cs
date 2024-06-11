using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orb_emitter : MonoBehaviour
{
    [SerializeField] private mp_orb orb;

    int width = 4;


    public void Instance(int amount, int width = 4)
    {
        int amount2 = amount;

        while (amount2 > 0)
        {
            if (amount2 < 40)
            {
                Create(amount2);
                amount2 = 0;
            }
            else
            {
                int num2 = Random.Range(30, 40);
                Create(num2);
                amount2 -=num2;
            }
        }
    }

    void Create(int num)
    {
        
        var neworb = Instantiate(this.orb);
        neworb.transform.position = transform.position;
        //neworb.Set(num);

    }

    
}
