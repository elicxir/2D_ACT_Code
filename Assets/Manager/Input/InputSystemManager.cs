using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using GameConsts;

public class InputSystemManager : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;

    class InputButton
    {
        public string id;

        InputAction inputAction;

        //n flame‘O‚Ì“ü—Íî•ñ
        public bool[] buffer = new bool[240];

        public bool Buttondown;//‰Ÿ‚³‚ê‚½uŠÔ‚©‚Ç‚¤‚©
        public bool Button;//‰Ÿ‚³‚ê‚Ä‚¢‚é‚©
        public bool Buttonup;//—£‚³‚ê‚½uŠÔ‚©‚Ç‚¤‚©

        public void Init(PlayerInput input)
        {
            inputAction = input.actions[id];

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = false;
            }
        }

        public void Update()
        {
            {
                Button = inputAction.ReadValue<float>() > 0;

                for (int i = 0; i < buffer.Length - 1; i++)
                {
                    buffer[buffer.Length - i - 1] = buffer[buffer.Length - i - 2];
                }
                buffer[0] = Button;
            }

            if (buffer[0] && !buffer[1])
            {
                Buttondown = true;

            }
            else
            {
                Buttondown = false;
            }

            if (!buffer[0] && buffer[1])
            {
                Buttonup = true;
            }
            else
            {
                Buttonup = false;
            }


            if (DebugMode.input && Button)
            {
                print($"input:{id}");
            }
        }

    }

    List<InputButton> inputButtons = new List<InputButton>();

    public void Init()
    {
        for (int q = 0; q < Enum.GetNames(typeof(Control)).Length; q++)
        {
            InputButton data = new InputButton { id = ((Control)q).ToString() };
            inputButtons.Add(data);
        }

        foreach (InputButton data in inputButtons)
        {
            data.Init(playerInput);
        }
    }

    public void Updater()
    {
        foreach (InputButton data in inputButtons)
        {
            data.Update();
        }
    }

    int GetIndex(string id)
    {
        for (int q = 0; q < inputButtons.Count; q++)
        {
            if (inputButtons[q].id == id)
            {
                return q;
            }
        }
        return 0;
    }


    public bool Button(Control control)
    {
        return inputButtons[(int)control].Button;
    }
    public bool ButtonUp(Control control)
    {
        return inputButtons[(int)control].Buttonup;
    }
    public bool ButtonDown(Control control)
    {
        return inputButtons[(int)control].Buttondown;
    }

    public Vector2 InputVector()
    {
        Vector2 result = Vector2.zero;

        if (Button(Control.Up))
        {
            result += Vector2.up;
        }
        if (Button(Control.Left))
        {
            result += Vector2.left;
        }
        if (Button(Control.Right))
        {
            result += Vector2.right;
        }
        if (Button(Control.Down))
        {
            result += Vector2.down;
        }
        return result;
    }

}

public enum Control
{

    Decide,
    Cancel,

    Jump,

    Attack,
    Special,

    Evade,

    Menu,
    Map,

    Up,
    Down,
    Right,
    Left,

    SpecialChange,
}



/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using GameConsts;

public class InputSystemManager : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;

    [SerializeField] string[] buttonID;

    class InputStick
    {

    }

    class InputButton
    {
        public string id;

        InputAction inputAction;

        //n fixedflame‘O‚Ì“ü—Íî•ñ
        public bool[] buffer = new bool[240];

        public bool Buttondown;//‰Ÿ‚³‚ê‚½uŠÔ‚©‚Ç‚¤‚©
        public bool Button;//‰Ÿ‚³‚ê‚Ä‚¢‚é‚©
        public bool Buttonup;//—£‚³‚ê‚½uŠÔ‚©‚Ç‚¤‚©

        public void Init(PlayerInput input)
        {
            inputAction = input.actions[id];

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = false;
            }

        }

        public void Update()
        {
            {
                Button = inputAction.ReadValue<float>() > 0;

                for (int i = 0; i < buffer.Length - 1; i++)
                {
                    buffer[buffer.Length - i - 1] = buffer[buffer.Length - i-2];
                }
                buffer[0] = Button;
            }

            if (buffer[0] && !buffer[1])
            {
                Buttondown = true;

            }
            else
            {
                Buttondown = false;
            }

            if (!buffer[0] && buffer[1])
            {
                Buttonup = true;

            }
            else
            {
                Buttonup = false;
            }


            if (DebugMode.input && Button)
            {
                print($"input:{id}");
            }
        }

    }

    List<InputButton> inputButtons = new List<InputButton>();

    private void Awake()
    {
        for (int q = 0; q < buttonID.Length; q++)
        {
            InputButton data = new InputButton { id = buttonID[q] };
            inputButtons.Add(data);
        }

        foreach (InputButton data in inputButtons)
        {
            data.Init(playerInput);
        }
    }

    public void Updater()
    {

        foreach (InputButton data in inputButtons)
        {
            data.Update();
        }
    }


    int GetIndex(string id)
    {
        for (int q = 0; q < inputButtons.Count; q++)
        {
            if (inputButtons[q].id == id)
            {
                return q;
            }
        }
        return 0;
    }

    public bool Button(Control control)
    {
        return inputButtons[(int)control].Button;
    }
    public bool ButtonUp(Control control)
    {
        return inputButtons[(int)control].Buttonup;
    }
    public bool ButtonDown(Control control)
    {
        return inputButtons[(int)control].Buttondown;
    }




    public bool Button(string id)
    {
        return inputButtons[GetIndex(id)].Button;
    }
    public bool ButtonUp(string id)
    {
        return inputButtons[GetIndex(id)].Buttonup;
    }
    public bool ButtonDown(string id)
    {
        return inputButtons[GetIndex(id)].Buttondown;
    }

    public Vector2 InputVector()
    {
        Vector2 result = Vector2.zero;
        
        if (Button("Up"))
        {
            result += Vector2.up;
        }
        if (Button("Left"))
        {
            result += Vector2.left;
        }
        if (Button("Right"))
        {
            result += Vector2.right;
        }
        if (Button("Down"))
        {
            result += Vector2.down;
        }
        return result;
    }

}

public enum Control { 
    Decide,
    Cancel,
    Menu,
    Jump,
    Spell1,
    Spell2,
    Evade,
    Item,
    Map,
    Up,
    Down,
    Right,
    Left,
}

 */