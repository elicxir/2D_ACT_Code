using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataTypes
{
    namespace General
    {
        namespace Item
        {

        }
    }

    namespace Map
    {

    }

    namespace GameData
    {
        //�ݒ�̃f�[�^
        [System.Serializable]
        public class ConfigData
        {
            public enum ScreenMode
            {
                Window_1,
                Window_2,
                Window_3,
                Window_4,
                FullScreen
            }

            /// <summary>
            /// ����͂Ƃ肠�������{��Ɖp��̂�
            /// </summary>
            public enum Language
            {
                Japanese,English
            }

            public int MasterVolume = 20;
            public int SE = 20;
            public int BGM = 20;

            public ScreenMode screenMode;
            public Language language;

            public void Init()
            {
                screenMode = ScreenMode.Window_1;
                language = Language.Japanese;
                MasterVolume = 20;
                SE = 20;
                BGM = 20;
            }

            public string ScreenModeName
            {
                get
                {
                    switch (screenMode)
                    {
                        case ScreenMode.Window_1:
                            return "window x1(320x240)";
                        case ScreenMode.Window_2:
                            return "window x2(640x480)";
                        case ScreenMode.Window_3:
                            return "window x3(960x720)";
                        case ScreenMode.Window_4:
                            return "window x4(1280x960)";
                        case ScreenMode.FullScreen:
                            return "FullScreen";
                        default:
                            return "asdasdasd";
                    }
                }
            }

            public void Validate()
            {
                MasterVolume = Mathf.Clamp(MasterVolume, 0, 20);
                SE = Mathf.Clamp(SE, 0, 20);
                BGM = Mathf.Clamp(BGM, 0, 20);
            }

        }


        //�ݒ�̃f�[�^
        [System.Serializable]
        public class PlayData
        {
            public float PlayTime = 0;
            public string Filename = "New Game";

            public FileData fileData()
            {
                FileData data = new FileData();
                data.filename = Filename;
                data.playtime = ((int)((int)PlayTime / 60)).ToString("D2") + ":" + (((int)PlayTime % 60)).ToString("D2");

                return data;

            }
            public Vector2 StartPoint = new Vector2(3330, 3464);//�Q�[���̊J�n�n�_

            //�K�ꂽ���Ƃ̂���O���b�h
            public List<Vector2Int> VisitedGrid = new  List<Vector2Int>();

            //�o���l(����)
            public int exp = 0;

            public EquipData equipData = new EquipData();


            //�����A�C�e���̕�[
            public void FillConsumable()
            {
                Debug.Log("fillConsumable");

                foreach (int ID in GetConsumableList())
                {
                    ConsumableFunction data = Define.EDM.GET_CONSUMABLE_FUNCTION(ID);

                    if (data != null)
                    {
                        data.useCount = data.MaxUseCount;
                    }
                }

            }

            /// <summary>
            /// �擾�ς݃X�e�[�^�X�㏸�A�C�e����ID���X�g
            /// </summary>
            [SerializeField] List<int> StatusUpFlagList = new List<int> { };

            public bool AddStatusUpFlag(int id)
            {
                if (!StatusUpFlagList.Contains(id))
                {
                    StatusUpFlagList.Add(id);
                    return true;
                }
                return false;
            }
            public bool FindStatusUpFlag(int id)
            {
                return StatusUpFlagList.Contains(id);
            }

            public int HP_UP = 0;
            public int MP_UP = 0;
            public int ATK_UP = 0;



            /// <summary>
            /// �����A�C�e��ID�̃��X�g
            /// </summary>
            [SerializeField] List<int> KeyItemList = new List<int> { };

            public List<int> GetKeyItemList()
            {
                List<int> list = new List<int>(KeyItemList);
                return list;
            }

            public bool AddKeyItem(int id)
            {
                if (!KeyItemList.Contains(id))
                {
                    KeyItemList.Add(id);
                    return true;
                }
                return false;
            }
            public bool FindKeyItem(int id)
            {
                return KeyItemList.Contains(id);
            }




            /// <summary>
            /// �����A�C�e��ID�̃��X�g
            /// </summary>
            [SerializeField] List<int> ConsumableList = new List<int> { 0 };

            public List<int> GetConsumableList()
            {
                List<int> list = new List<int>(ConsumableList);
                return list;
            }

            public bool AddConsumable(int id)
            {
                if (Define.EDM.ValidID_Consumable(id) && !ConsumableList.Contains(id))
                {
                    ConsumableList.Add(id);
                    return true;
                }
                return false;
            }

            /// <summary>
            /// id�̃A�C�e���������Ă��邩
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public bool FindConsumableList(int id)
            {
                return ConsumableList.Contains(id);
            }

            public void CheckConsumableList()
            {
                string a = "";
                foreach (int data in ConsumableList)
                {
                    a += data;
                    a += ":";
                }
                Debug.LogWarning("ItemList" + a);
            }

            /// <summary>
            /// �����X�y��ID�̃��X�g
            /// </summary>
            [SerializeField] List<int> SpellList = new List<int> { 0,4,7};

            public List<int> GetSpellList()
            {
                List<int> list = new List<int>(SpellList);
                return list;
            }
            public bool AddSpell(int id)
            {
                if (Define.EDM.ValidID_Spell(id) && !SpellList.Contains(id))
                {
                    SpellList.Add(id);
                    return true;
                }
                return false;
            }

            public bool FindSpell(int id)
            {
                return SpellList.Contains(id);
            }

            /// <summary>
            /// �����A�N�Z�T���[ID�̃��X�g
            /// </summary>
            [SerializeField]
            List<int> AccessoryList = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

            public List<int> GetAccessoryList()
            {
                List<int> list = new List<int>(AccessoryList);
                return list;
            }
            public bool AddAccessory(int id)
            {
                if (Define.EDM.ValidID_Accessory(id) && !AccessoryList.Contains(id))
                {
                    AccessoryList.Add(id);
                    return true;
                }
                return false;
            }

            public bool FindAccessory(int id)
            {
                return AccessoryList.Contains(id);
            }





            /// <summary>
            /// �����`���[�g���A���̃��X�g
            /// </summary>
            [SerializeField] List<int> TutorialList = new List<int> { 0 };

            public List<int> GetTutorialList()
            {
                List<int> list = new List<int>(TutorialList);
                return list;
            }
            public void AddTutorials(int id)
            {
                TutorialList.Add(id);
            }

            /// <summary>
            /// �����N�G�X�g�����̃��X�g
            /// </summary>
            [SerializeField] List<int> QuestList = new List<int> { };

            public List<int> GetQuestList()
            {
                List<int> list = new List<int>(QuestList);
                return list;
            }
            public void AddQuests(int id)
            {
                QuestList.Add(id);
            }

            /// <summary>
            /// ���������̃��X�g
            /// </summary>
            [SerializeField] List<int> NoteList = new List<int> { };

            public List<int> GetNoteList()
            {
                List<int> list = new List<int>(NoteList);
                return list;
            }
            public void AddNotes(int id)
            {
                NoteList.Add(id);
            }


            [SerializeField] List<EventFlag> EventFlagList = new List<EventFlag>();

            public EventFlag EventFlagCheck(string name)
            {
                foreach (EventFlag ef in EventFlagList)
                {
                    if (ef.FlagID == name)
                    {
                        return ef;
                    }
                }
                return null;
            }
            public void FlagDataStore(EventFlag data)
            {
                for (int i = 0; i < EventFlagList.Count; i++)
                {
                    if (EventFlagList[i].FlagID == data.FlagID)
                    {
                        EventFlagList[i] = data;
                    }
                }

                EventFlagList.Add(data);

            }



            [SerializeField] List<GameFlag> GameFlagList = new List<GameFlag>();

            public GameFlag GetGameFlag(string name)
            {
                foreach (GameFlag ef in GameFlagList)
                {
                    if (ef.FlagID == name)
                    {
                        return ef;
                    }
                }

                if (name == string.Empty)
                {
                    Debug.LogError("�Q�[���t���O�̐ݒ肪�s�\��");
                }

                GameFlag data = new GameFlag
                {
                    FlagID = name
                };

                GameFlagList.Add(data);


                return data;
            }
            public void SetGameFlag(GameFlag data)
            {

                for (int i = 0; i < GameFlagList.Count; i++)
                {
                    if (GameFlagList[i].FlagID == data.FlagID)
                    {
                        GameFlagList[i] = data;
                        return;
                    }
                }

                GameFlagList.Add(data);

            }


        }





        [System.Serializable]
        public class EventFlag
        {
            public string FlagID;
            public int progress;
            public bool flag;
        }


        [System.Serializable]
        public class FileData
        {
            public string filename = "New Game";
            public float map = 0;
            public string playtime = "0:00";
            public string datastate;
            public Sprite sprite;
        }


        [System.Serializable]
        public class EquipData
        {
            public int[] consumableID = { -1, -1, -1 };
            public int[] spellID = { 0, 4, -1, -1 };
            public int[] accessoryID = { -1, -1, -1, -1 };
        }


    }


    public enum DamageRatio
    {
        Normal,//�ʏ�̃_���[�W   
        Resist_25,//�_���[�W25%�J�b�g
        Resist_50,//�_���[�W50%�J�b�g
        Resist_75,//�_���[�W75%�J�b�g
        Weak_125,//1.25�{�̃_���[�W
        Weak_200,//2�{�̃_���[�W
        Absorb,//�z��(50%�̗̑͂��z������)
        NoEffect,//�_���[�W����      
    }

    public enum DamageType
    {
        Physical,
        Fire,
        Ice,
        Thunder,
        Magic,
        Physical_Fire,
        Physical_Ice,
        Physical_Thunder,
    }





    namespace Element
    {
        public class Element_Attack
        {
            public int ATK;
            public DamageRatio Slash=DamageRatio.Normal;
        }


            [System.Serializable]
        public class Element
        {
            public int slash = 0;
            public int hack = 0;
            public int magic = 0;

            public int fire = 0;
            public int ice = 0;
            public int thunder = 0;

            public int poison = 0;
            public int hurt = 0;
            public int curse = 0;

            public int[] values
            {
                get
                {
                    int[] vs = new int[9];

                    vs[0] = slash;
                    vs[1] = hack;
                    vs[2] = magic;
                    vs[3] = fire;
                    vs[4] = ice;
                    vs[5] = thunder;
                    vs[6] = poison;
                    vs[7] = hurt;
                    vs[8] = curse;

                    return vs;
                }
            }

            public void SetValues(int[] vs)
            {
                if (vs.Length == 9)
                {
                    slash = vs[0];
                    hack = vs[1];
                    magic = vs[2];
                    fire = vs[3];
                    ice = vs[4];
                    thunder = vs[5];
                    poison = vs[6];
                    hurt = vs[7];
                    curse = vs[8];
                }
                else
                {
                    Debug.LogError("Element index error");
                }


            }

            public int Sum
            {
                get
                {
                    return slash + hack + magic + fire + ice + thunder;
                }
            }

        }


        [System.Serializable]
        public class BaseStats
        {
            public int MaxHP = 100;
            public int MaxMP = 100;

            public int BaseAttack = 100;

            public float BaseHPreg = 0;
            public float BaseMPreg = 10;

            public int BasePoisonResist = 0;
            public int BaseHurtResist = 0;
            public int BaseCurseResist = 0;

        }

        public static class Cal
        {
            /// <summary>
            /// �U���͂Ɣ{��������ۂ̃_���[�W���Z�o����
            /// </summary>
            public static Element Deal_Damage(Element attack, Element ratio)
            {
                int[] a = attack.values;
                int[] r = ratio.values;

                int[] res = new int[9];
                Element element = new Element();

                for (int i = 0; i < 9; i++)
                {
                    res[i] = (a[i] * r[i]) / 100;
                }

                element.SetValues(res);

                return element;

            }

            /// <summary>
            /// �_���[�W�Ɩh��͂���󂯂�_���[�W���Z�o����
            /// </summary>
            public static Element Take_Damage(Element damage, Element deffence)
            {
                int[] d1 = damage.values;
                int[] d2 = deffence.values;

                int[] res = new int[9];
                Element element = new Element();

                for (int i = 0; i < 9; i++)
                {
                    if (d2[i] == -100)
                    {
                        d2[i] = -99;
                    }
                    res[i] = (d1[i] * 100) / (100 + d2[i]);
                }

                element.SetValues(res);

                return element;
            }

            public static Element Add(Element e1, Element e2)
            {
                int[] d1 = e1.values;
                int[] d2 = e2.values;

                int[] res = new int[9];
                Element element = new Element();

                for (int i = 0; i < 9; i++)
                {
                    res[i] = d1[i] + d2[i];
                }

                element.SetValues(res);

                return element;

            }


        }


        
    }

    namespace Functions
    {
        /// <summary>
        /// ���S�Ƒ傫���ɂ���`�͈̔͂𓾂邽�߂̃N���X()
        /// </summary>
        public static class C_Rect
        {
            public static Vector2 Center(Rect rect)
            {
                return rect.position;
            }

            public static float Right(Rect rect)
            {
                return rect.position.x + rect.width / 2;
            }
            public static float Left(Rect rect)
            {
                return rect.position.x - rect.width / 2;
            }
            public static float Up(Rect rect)
            {
                return rect.position.y + rect.height / 2;
            }
            public static float Down(Rect rect)
            {
                return rect.position.y - rect.height / 2;
            }

            public static Vector2 RightUp(Rect rect)
            {
                return new Vector2(Right(rect), Up(rect));
            }
            public static Vector2 LeftUp(Rect rect)
            {
                return new Vector2(Left(rect), Up(rect));
            }
            public static Vector2 RightDown(Rect rect)
            {
                return new Vector2(Right(rect), Down(rect));
            }
            public static Vector2 LeftDown(Rect rect)
            {
                return new Vector2(Left(rect), Down(rect));
            }
        }

        public static class Rand
        {

            
            public static int Range(int min, int max)
            {
                return Random.Range(min, max + 1);
            }
        }
        /*
        public struct BezierCurve
        {/*
            public BezierCurve(Vector2[] c_points, int devide)
            {
                ControllPoint = c_points;
                Point = new Vector2[devide + 1];
                //Devider();
                //length = Length();
            }
            
            Vector2[] ControllPoint;
            Vector2[] Point;
            public float length;

            public Vector2[] GetPoints
            {
                get
                {
                    return Point;
                }
            }



        }

    */











    }

}



[System.Serializable]
public class GameFlag
{
    public string FlagID;
    public string data = string.Empty;
    public int progress = 0;
    public bool isTrue = false;
}

[System.Serializable]
public class ItemUse
{
    public int ID;
    public int count;
}