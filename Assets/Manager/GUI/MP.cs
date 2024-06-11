using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MP : MonoBehaviour
{
    [SerializeField] Sprite mp_image;//MPの画像
    [SerializeField] Sprite mp2_image;//活性MPの画像
    [SerializeField] Sprite mp3_image;//MPの画像(チャージ中)
    [SerializeField] Sprite mp4_image;//活性MPの画像(チャージ中)

    [SerializeField] Image playermpblock;//MP枠の画像
    [SerializeField] Image MP1;//MP
    [SerializeField] Image MP2;//活性MP

    public bool enable=false;

    bool mp
    {
        get
        {
            if (mana >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    bool emp
    {
        get
        {
            if (energy >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public float energy=0;
    public float mana=0;


    // Update is called once per frame
    void Update()
    {
        if (enable)
        {
            playermpblock.enabled = true;
            MP1.enabled = true;
            MP2.enabled = true;

            if (mp)
            {
                MP1.sprite = mp_image;
                MP1.fillAmount = 1;
            }
            else
            {
                MP1.sprite = mp3_image;
                MP1.fillAmount = mana;

            }

            if (emp)
            {
                MP2.sprite = mp2_image;
                MP2.fillAmount = 1;
            }
            else
            {
                MP2.sprite = mp4_image;
                MP2.fillAmount = energy;
            }

        }
        else
        {
            playermpblock.enabled = false;
            MP1.enabled = false;
            MP2.enabled = false;
        }

        

    }
}
