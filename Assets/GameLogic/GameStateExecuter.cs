using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class GameStateExecuter : MonoBehaviour
{
    bool firstinitflag = false;

    public virtual IEnumerator Init(gamestate before)
    {

        yield break;
    }

    public virtual IEnumerator FirstInit()
    {
        if (!firstinitflag)
        {


            firstinitflag = true;
        }
        else
        {
            print("2‰ñFistInit‚ğ‚·‚é‚±‚Æ‚Í‚Å‚«‚Ü‚¹‚ñI");
        }
        yield break;
    }

    public virtual void Updater()
    {
    }

    public virtual void LateUpdater()
    {

    }

    public virtual IEnumerator Finalizer(gamestate after)
    {

        yield break;
    }

    protected GameManager GAME
    {
        get
        {
            return GM.Game;
        }
    }

    protected InputSystemManager INPUT
    {
        get
        {
            return GAME.Input_Manager;
        }
    }

}
