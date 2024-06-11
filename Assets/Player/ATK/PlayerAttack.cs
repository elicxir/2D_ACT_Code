using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using UnityEngine.Tilemaps;

public class PlayerAttack : MonoBehaviour
{
    public SpellFunction[] spellFunctions = new SpellFunction[2];
    public AccessoryFunction[] accessoryFunctions = new AccessoryFunction[2];

    public void SetData()
    {/*
        EquipmentID equipmentID = GM.Game.PlayData.GetEquipment;

        spellFunctions[0] = Define.EDM.GET_SPELL_FUNCTION(equipmentID.SpellID[0]);
        spellFunctions[1] = Define.EDM.GET_SPELL_FUNCTION(equipmentID.SpellID[1]);

        accessoryFunctions[0] = Define.EDM.GET_ACCESSORY_FUNCTION(equipmentID.AccessoryID[0]);
        accessoryFunctions[1] = Define.EDM.GET_ACCESSORY_FUNCTION(equipmentID.AccessoryID[1]);*/
    }


    bool Skill1;
    float Timer1 = 0;



    float Timer2 = 0;

    float CastTimer = 0;
    float CastTime = 0.2f;

    bool Skill2;

    float shot_cd = 0.75f;



    public void FixedUpdater()
    {
        //アクセサリーの処理
        //if (Define.GM.GameState == gamestate.inGame)
        {
            //accessoryFunctions[0].Updater();
            //accessoryFunctions[1].Updater();

        }
        /*
        //スペル1の処理
        {
            int num = 0;
            spellFunctions[num].FixedUpdater();
        }

        //スペル2の処理
        {
            int num = 1;
            spellFunctions[num].FixedUpdater();
        }*/
    }

    //Tilemap





    private void Update()
    {

        /*
        if (Define.PM.Controll && GM.Game.isGaming)
        {
            CastTimer = Mathf.Max(0,CastTimer- Time.deltaTime);
           
            
            //スペル1
            {
                int num = 0;
                string button = "Spell1";
                float timer = CastTimer;

                switch (spellFunctions[num].spellType)
                {
                    case SpellType.Trigger:
                        if (Define.IM.ButtonDown(button) && timer == 0)
                        {
                            spellFunctions[num].Trigger();
                            CastTimer = CastTime;

                        }
                        break;

                    case SpellType.Switch:
                        if (Define.IM.ButtonDown(button) && timer == 0)
                        {
                            spellFunctions[num].Switch();
                            CastTimer = CastTime;

                        }

                        break;

                    case SpellType.Increase:


                        if (Define.IM.ButtonDown(button) && timer == 0)
                        {
                            spellFunctions[num].IncreaseStart();
                        }
                        else if (Define.IM.ButtonUp(button))
                        {
                            spellFunctions[num].IncreaseEnd();
                            CastTimer = CastTime;

                        }
                        else if (Define.IM.Button(button))
                        {

                        }

                        break;


                }



            }

            //スペル2
            {
                int num = 1;
                string button = "Spell2";
                float timer = CastTimer;

                switch (spellFunctions[num].spellType)
                {
                    case SpellType.Trigger:
                        if (Define.IM.ButtonDown(button) && timer == 0)
                        {
                            spellFunctions[num].Trigger();
                            CastTimer = CastTime;

                        }
                        break;

                    case SpellType.Switch:
                        if (Define.IM.ButtonDown(button) && timer == 0)
                        {
                            spellFunctions[num].Switch();
                            CastTimer = CastTime;

                        }

                        break;

                    case SpellType.Increase:


                        if (Define.IM.ButtonDown(button) && timer == 0)
                        {
                            spellFunctions[num].IncreaseStart();
                        }
                        else if (Define.IM.ButtonUp(button))
                        {
                            spellFunctions[num].IncreaseEnd();
                            CastTimer = CastTime;

                        }
                        else if (Define.IM.Button(button))
                        {

                        }

                        break;


                }



            }
        }*/
    }



}

public class AttackData
{
    int val;
}