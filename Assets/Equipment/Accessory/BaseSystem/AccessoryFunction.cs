using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class AccessoryFunction : MonoBehaviour
{
    protected EquipmentDataManager EDM;

    protected string Name
    {
        get
        {
            return this.gameObject.name;
        }
    }

    protected PlayerManager PM
    {
        get
        {
            return GM.Player;
        }       

    }
    [SerializeField] AccessoryData data;
    protected virtual void OnValidate()
    {
       EDM = FindObjectOfType<EquipmentDataManager>();

       int index= transform.GetSiblingIndex();

    }



    public string accessoryname { get { return data.consumablename; } }
    public Sprite Sprite { get { return data.Sprite; } }

    public string description { get { return data.desc; } }

    public string flavor { get { return data.flavor; } }


    public virtual bool Condition()
    {
        return true;
    }

    public virtual void Passive()
    {

    }

    public void Updater()
    {
        if (Condition())
        {
            Passive();
        }
    }


}

