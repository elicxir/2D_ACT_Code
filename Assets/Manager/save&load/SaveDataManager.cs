using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DataTypes.General;

public class SaveDataManager : MonoBehaviour
{

    int data_num = 0;//読み込んでいるデータ番号(1-4)    

    [System.Serializable]
    public class GeneralData
    {
        public int musicvolume = 10;
        public int soundvolume = 10;
        public int resolution = 1;
        public int language = 0;

    }

    [System.Serializable]
    public class SELECTDATA
    {
        public string[] filename = new string[4] { "NO DATA", "NO DATA", "NO DATA", "NO DATA" };

        public int[] hour = new int[4] { 0, 0, 0, 0 };
        public int[] minute = new int[4] { 0, 0, 0, 0 };
        public int[] second = new int[4] { 0, 0, 0, 0 };
    }


    [System.Serializable]
    public class PersonalData
    {
        public float totalplaytime;
        public string savepointname = "NO DATA";
        public string startmap = "mainmap";
        public Vector2 StartPoint = new Vector2(3820, 1780);//ゲームの開始地点
        public Vector2 ReStartPoint = new Vector2(17900, 6564);//ゲームの再開地点

        //経験値(お金)
        public int exp = 0;



        //各セーブポイントを解放しているか
        bool[] savepoint = new bool[10] { false, false, false, false, false, false, false, false, false, false };//false:未到達　true:到達

        //装備アイテムのデータ(アイテムIDと使用回数)
        [SerializeField] int[] equip = new int[3] { -1, -1, -1 };
        [SerializeField] int[] equipcount = new int[3] { 0, 0, 0 };

        int selectnum = 0;

        //装備のデータ(ID)
        [SerializeField] int[] spellID = new int[2] {0,1};
        [SerializeField] int[] accessoryID = new int[2] { 0, 1};




        int SelectedItemID
        {
            get
            {
                List<int> list = new List<int>();
                foreach (int num in equip)
                {
                    if (Define.EDM.ValidID_Consumable(num))
                    {
                        list.Add(num);
                    }
                }

                if (list.Count == 0)
                {
                    return -1;
                }
                else
                {
                    return list[selectnum % list.Count];
                }
            }
        }

        public void ItemSelect()
        {
            selectnum++;
            ItemUIUpdate();

        }

        public void ItemUIUpdate()
        {
            int index = -1;

            for (int q = 0; q < 3; q++)
            {
                if (equip[q] == SelectedItemID)
                {
                    index = q;
                }
            }

            int count;

            if (index == -1)
            {
                count = 0;
            }
            else
            {
                count = equipcount[index];
            }

            //Define.UI.ItemGraph(SelectedItemID, count);

        }

        public void UseItem()
        {

            int index = -1;

            for (int q = 0; q < 3; q++)
            {
                if (equip[q] == SelectedItemID)
                {
                    index = q;
                }
            }

            int count;

            if (index == -1)
            {
                count = 0;
            }
            else
            {
                count = equipcount[index];
            }

            if (count > 0 && Define.EDM.ValidID_Consumable(SelectedItemID))
            {
                ConsumableFunction data = Define.EDM.GET_CONSUMABLE_FUNCTION(SelectedItemID);

                if (data.Require())
                {
                    data.Active();
                    equipcount[index]--;
                }
                else
                {
                    Color color = Color.gray;
                    //Define.UM.SHOW_WARNING("-No Effect-", color);
                }
            }
            ItemUIUpdate();

        }


        //装備アイテムの補充
        public void FillEquip()
        {
            for (int q = 0; q < 3; q++)
            {
                if (equip[q] != -1)
                {
                    //ConsumableFunction data = Define.EDM.GET_CONSUMABLE_FUNCTION(equip[q]);
                    //equipcount[q] = data.duration;
                }
                else
                {
                    equipcount[q] = 0;
                }
            }
            selectnum = 0;
            ItemUIUpdate();
        }

        /// <summary>
        /// 装備品データの取得
        /// </summary>
        /*public EquipmentID GetEquipment
        {
            get
            {
                EquipmentID data = new EquipmentID();

                data.ItemID = equip;
                data.ItemCount = equipcount;
                data.SpellID = spellID;
                data.AccessoryID = accessoryID;

                return data;
            }

        }*/

        public void SetEquipment(int[] item, int[] spell,int[] accessory)
        {
            if (item.Length == 3)
            {
                equip = item;

                for (int i = 0; i < 3; i++)
                {
                    //equipcount[i] = Mathf.Min(equipcount[i], Define.EDM.GET_CONSUMABLE_FUNCTION(equip[i]).duration);
                }
            }
            if (spell.Length == 2)
            {
                spellID = spell;
            }
            if (accessory.Length == 2)
            {
                accessoryID = accessory;
            }

            Define.PM.PlayerAttack.SetData();

            selectnum = 0;


            ItemUIUpdate();

        }




        /// <summary>
        /// 所持アイテムIDのリスト
        /// </summary>
        [SerializeField] List<int> ItemList = new List<int> {0};

        public List<int> GetItemList()
        {
            List<int> list = new List<int>(ItemList);
            return list;
        }

        public void AddConsumable(int id)
        {
            if (Define.EDM.ValidID_Consumable(id) && !ItemList.Contains(id))
            {
                ItemList.Add(id);
            }
        }

        /// <summary>
        /// idのアイテムを持っているか
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool FindItem(int id)
        {
            return ItemList.Contains(id);
        }

        public void CheckitemList()
        {
            string a = "";
            foreach (int data in ItemList)
            {
                a += data;
                a += ":";
            }
            Debug.LogWarning("ItemList" + a);
        }

        /// <summary>
        /// 所持スペルIDのリスト
        /// </summary>
        [SerializeField] List<int> SpellList = new List<int> {0,1,2,3,4,5,6};

        public List<int> GetSpellList()
        {
            List<int> list = new List<int>(SpellList);
            return list;
        }
        public void AddSpell(int id)
        {
            if (Define.EDM.ValidID_Spell(id) && !SpellList.Contains(id))
            {
                SpellList.Add(id);
            }
        }

        public bool FindSpell(int id)
        {
            return SpellList.Contains(id);
        }

        /// <summary>
        /// 所持アクセサリーIDのリスト
        /// </summary>
        [SerializeField] List<int> AccessoryList = new List<int> { 0, 1};

        public List<int> GetAccessoryList()
        {
            List<int> list = new List<int>(AccessoryList);
            return list;
        }
        public void AddAccessory(int id)
        {
            if (Define.EDM.ValidID_Accessory(id) && !AccessoryList.Contains(id))
            {
                AccessoryList.Add(id);
            }
        }

        public bool FindAccessory(int id)
        {
            return AccessoryList.Contains(id);
        }



        

        /// <summary>
        /// 所持チュートリアルのリスト
        /// </summary>
        [SerializeField] List<int> TutorialList = new List<int> { 0};

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
        /// 所持クエストメモのリスト
        /// </summary>
        [SerializeField] List<int> QuestList = new List<int> {};

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
        /// 所持メモのリスト
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

        public List<int> StatusUpIndex=new List<int>();


        /*
        [SerializeField] List<EventFlag> EventFlagList = new List<EventFlag>();

        public EventFlag EventFlagCheck(string name)
        {
            foreach(EventFlag ef in EventFlagList)
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
            for(int i = 0; i < EventFlagList.Count; i++)
            {
                if (EventFlagList[i].FlagID == data.FlagID)
                {
                    EventFlagList[i] = data;
                }
            }

            EventFlagList.Add(data);

        }



        */





        /// <summary>
        /// マップ踏破系のデータ
        /// </summary>
        /// 
        [System.Serializable]
        public class PathData
        {
            public Vector2Int sec1;
            public Vector2Int sec2;
        }

        [SerializeField] List<Vector2Int> VisibleCell = new List<Vector2Int>();
        [SerializeField] List<Vector2Int> VisitedCell = new List<Vector2Int>();
        [SerializeField] List<PathData> pathDatas = new List<PathData>();

        public void IntoArea(Vector2Int pos)
        {
            if (!VisibleCell.Contains(pos))
            {
                VisibleCell.Add(pos);
            }
            if (!VisitedCell.Contains(pos))
            {
                VisitedCell.Add(pos);
            }
        }

        public void AddPathData(Vector2Int pos1, Vector2Int pos2)
        {

            PathData path = new PathData { sec1 = pos1, sec2 = pos2 };

            bool addflag = true;

            foreach (PathData data in pathDatas)
            {
                if (path.sec1 == data.sec1 && path.sec2 == data.sec2)
                {
                    addflag = false;
                    break;
                }
                if (path.sec1 == data.sec2 && path.sec2 == data.sec1)
                {
                    addflag = false;
                    break;
                }
            }

            if (addflag)
            {
                pathDatas.Add(path);
            }




        }

        public WalkData PlayerWalkData
        {
            get
            {              
                return new WalkData
                {
                    VisibleCell = VisibleCell,
                    VisitedCell = VisitedCell,
                    PathDatas = pathDatas,
                };
            }
        }


        


        bool[] eventflag = new bool[16] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };

        public int itemsum = 0;
        public int skillsum = 0;
        public int savesum = 0;

        /*
        public void DataToSum()
        {
            itemsum = 0;
            skillsum = 0;
            savesum = 0;

            for (int q = 0; q < item.Length; q++)
            {
                itemsum += Define.F.BoolInt(item[q]) * (int)Mathf.Pow(2, q);
            }

            for (int q = 0; q < skill.Length; q++)
            {
                skillsum += Define.F.BoolInt(skill[q]) * (int)Mathf.Pow(2, q);
            }

            for (int q = 0; q < savepoint.Length; q++)
            {
                savesum += Define.F.BoolInt(savepoint[q]) * (int)Mathf.Pow(2, q);
            }
        }
        public void SumToData()
        {
            for (int q = 0; q < item.Length; q++)
            {
                int x = itemsum % (int)Mathf.Pow(2, q + 1) - ((int)Mathf.Pow(2, q) - 1);

                if (x > 0)
                {
                    item[q] = true;
                }
                else
                {
                    item[q] = false;

                }
            }

            for (int q = 0; q < skill.Length; q++)
            {
                int x = skillsum % (int)Mathf.Pow(2, q + 1) - ((int)Mathf.Pow(2, q) - 1);
                if (x > 0)
                {
                    skill[q] = true;
                }
                else
                {
                    skill[q] = false;

                }
            }

            for (int q = 0; q < savepoint.Length; q++)
            {
                int x = savesum % (int)Mathf.Pow(2, q + 1) - ((int)Mathf.Pow(2, q) - 1);

                if (x > 0)
                {
                    savepoint[q] = true;
                }
                else
                {
                    savepoint[q] = false;

                }
            }

        }       
        public void GET_SKILL(int num)
        {
            if (num >= 0 && num < skill.Length)
            {
                skill[num] = true;

            }
        }
        public void ACHIEVE_POINT(int num)
        {
            if (num >= 0 && num < savepoint.Length)
            {
                savepoint[num] = true;

            }

        }
        public bool FLAG_SKILL(int num)
        {
            if (num >= 0 && num < skill.Length)
            {
                return skill[num];

            }
            return false;
        }
        public bool FLAG_SAVEPOINT(int num)
        {
            if (num >= 0 && num < savepoint.Length)
            {
                return savepoint[num];

            }
            return false;
        }
        //チェックサム(セーブデータの整合性確認)
        public int Checksum;

    */
    }




    public void G_DATA_SAVE(int m, int s, int r, int l)
    {
        GeneralData G_D = new GeneralData();
        G_D.musicvolume = m;
        G_D.soundvolume = s;
        G_D.resolution = r;
        G_D.language = l;
        //SaveData.SetClass<GeneralData>("GD", G_D);
        //SaveData.Save();
    }

    /*
    //ゲーム起動時にデータを持ってくる。
    public GeneralData G_DATA_LOAD()
    {
        GeneralData value = SaveData.GetClass<GeneralData>("GD", new GeneralData());

        //Define.GM.SCREEN(value.resolution);

        return value;
    }*/


    /*
    //データセレクトに入る時に表示データを持ってくる
    public SELECTDATA S_DATA_LOAD()
    {
        SELECTDATA data = SaveData.GetClass<SELECTDATA>("SS", new SELECTDATA());

        return data;

    }
    void S_DATA_SAVE(string name, int h, int m, int s)
    {
        SELECTDATA data = SaveData.GetClass<SELECTDATA>("SS", new SELECTDATA());

        if (data_num == 1 || data_num == 2 || data_num == 3 || data_num == 4)
        {
            data.filename[data_num - 1] = name;
            data.hour[data_num - 1] = h;
            data.minute[data_num - 1] = m;
            data.second[data_num - 1] = s;

        }

        SaveData.SetClass<SELECTDATA>("SS", data);
        SaveData.Save();
    }

    public PersonalData P_DATA_LOAD(int datanum)
    {
        if (datanum == 1 || datanum == 2 || datanum == 3 || datanum == 4)
        {
            data_num = datanum;
            string key = "DATA";

            switch (datanum)
            {
                case 1:
                    key = "DATA1";
                    break;
                case 2:
                    key = "DATA2";
                    break;
                case 3:
                    key = "DATA3";
                    break;
                case 4:
                    key = "DATA4";
                    break;
            }





            return SaveData.GetClass<PersonalData>(key, new PersonalData());

        }

        return null;
    }
    */
    PersonalData ActualGameData;

    /// <summary>
    /// get:現在プレイ中のデータを返す
    /// </summary>
    public PersonalData GameData
    {
        get
        {
            return ActualGameData;
        }
    }

    public int GameDataLoad
    {
        set
        {
            if (0 < value && value < 5)
            {
                data_num = value;

                string key = "DATA";

                switch (value)
                {
                    case 1:
                        key = "DATA1";
                        break;
                    case 2:
                        key = "DATA2";
                        break;
                    case 3:
                        key = "DATA3";
                        break;
                    case 4:
                        key = "DATA4";
                        break;
                }

                //ActualGameData = SaveData.GetClass(key, new PersonalData());
                Define.PM.PlayerAttack.SetData();
                ActualGameData.ItemUIUpdate();
            }
        }
    }
    /*
    public void GameDataSave()
    {

        if (data_num != 0)
        {
            string key = "DATA";

            switch (data_num)
            {
                case 1:
                    key = "DATA1";
                    break;
                case 2:
                    key = "DATA2";
                    break;
                case 3:
                    key = "DATA3";
                    break;
                case 4:
                    key = "DATA4";
                    break;
            }

            int s = (int)(ActualGameData.totalplaytime) % 60;
            int m = (int)(ActualGameData.totalplaytime / 60) % 60;
            int h = (int)(ActualGameData.totalplaytime / 3600);

            if (h > 99)
            {
                h = 99;
                m = 59;
            }
            S_DATA_SAVE(ActualGameData.savepointname, h, m, s);

            Debug.Log("Save");

            SaveData.SetClass<PersonalData>(key, ActualGameData);
            SaveData.Save();

        }
    }
    */
}

public class WalkData
{
    public List<Vector2Int> VisibleCell;
    public List<Vector2Int> VisitedCell;
    public List<SaveDataManager.PersonalData.PathData> PathDatas;

}






