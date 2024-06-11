using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] InputMappingSheet InputMapping;

    class InputData
    {
        public string id;
        public string input;

        public bool Buttondown;//押された瞬間かどうか
        public bool Button;//押されているか
        public bool Buttonup;//離された瞬間かどうか

        public int upInput;
        public int intInput;

        public void update()
        {
            /*
            if (Input.GetButtonDown(input))
            {
                Buttondown = true;
            }
            else
            {
                Buttondown = false;
            }

            if (Input.GetButtonUp(input))
            {
                Buttonup = true;
                upInput = intInput;
            }
            else
            {
                Buttonup = false;
            }

            if (Input.GetButton(input))
            {
                Button = true;
                intInput += (int)(Time.deltaTime * 1000);
            }
            else
            {
                Button = false;
                intInput = 0;
            }
            */
        }

    }

    List<InputData> inputs = new List<InputData>();

    void Awake()
    {
        for (int q = 0; q < InputMapping.sheets[0].list.Count; q++)
        {
            InputData data = new InputData { id = InputMapping.sheets[0].list[q].InputID, input = InputMapping.sheets[0].list[q].InputName };
            inputs.Add(data);
        }
    }

    void Update()
    {
        foreach (InputData data in inputs)
        {
            data.update();
        }
    }

    int GetIndex(string id)
    {
        for (int q = 0; q < inputs.Count; q++)
        {
            if (inputs[q].id == id)
            {
                return q;
            }
        }

        Debug.LogWarning("インプット登録エラー");
        return -1;
    }


    public bool Button(string id)
    {
        if (GetIndex(id) != -1)
        {
            return inputs[GetIndex(id)].Button;
        }
        else
        {
            return false;
        }
    }
    /*
    public bool InputMultButton(string[] id)
    {
        bool result = false;

        foreach (string item in id)
        {
            if (GetIndex(item) != -1)
            {
                result = result | inputs[GetIndex(item)].Button;
            }
            else
            {
                result = result | false;

            }
        }

        return result;
    }
    */
    //押された瞬間かどうか
    public bool ButtonDown(string id)
    {
        if (GetIndex(id) != -1)
        {
            return inputs[GetIndex(id)].Buttondown;
        }
        else
        {
            return false;
        }
    }

    public bool ButtonUp(string id)
    {
        if (GetIndex(id) != -1)
        {
            return inputs[GetIndex(id)].Buttonup;
        }
        else
        {
            return false;
        }
    }

    

}


/*
public class InputMap
{
    public string Jump
    {
        get
        {
            return jump;
        }
    }
    public string Attack
    {
        get
        {
            return attack;
        }
        set
        {
            attack = value;
        }
    }
    public string Shot1
    {
        get
        {
            return shot1;
        }
        set
        {
            shot1 = value;
        }
    }

    public string Shot2
    {
        get
        {
            return shot2;
        }
        set
        {
            shot2 = value;
        }
    }

    public string Decide
    {
        get
        {
            return decide;
        }
        set
        {
            decide = value;
        }
    }
    public string Cancel
    {
        get
        {
            return cancel;
        }
        set
        {
            cancel = value;
        }
    }

    string up;
    string down;
    string left;
    string right;

    string jump;
    string attack;
    string shot1;
    string shot2;
    string step;
    string item;

    string start;
    string select;

    string cancel;
    string decide;


}

*/