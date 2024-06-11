using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameConsts
{
    public class Glyph
    {
        public static readonly string UP="ğA";
        public static readonly string DOWN = "ğB";
        public static readonly string RIGHT = "ğD";
        public static readonly string LEFT = "ğC";
        public static readonly string CROSS = "ğ@";

        public static readonly string VERTICAL = "ğE";
        public static readonly string HORIZONTAL = "ğF";

        public static readonly string A = "ğP";
        public static readonly string B = "ğQ";
        public static readonly string X = "ğR";
        public static readonly string Y = "ğS";



        public static readonly string L = "ğ…";
        public static readonly string R = "ğ†";

    }


    public class Game
    {
        public static readonly int width = 320;
        public static readonly int height = 240;

        public static readonly float Gravity = 720;
    }

    public class Map
    {
        public static readonly int tile_size = 16;

        public static readonly int width = 20;
        public static readonly int height = 15;

        public static readonly int Cell_Holizontal = 64;
        public static readonly int Cell_Vertical = 32;
    }

    public class DebugMode
    {
        public static readonly bool input = false;
        public static readonly bool gamestate = true;
        public static readonly bool damage = false;
        public static readonly bool save_load = true;
    }
}

namespace Managers
{
    public static class GM
    {
        public static readonly GameManager Game = GameManager.Game_Manager;

        public static readonly InputSystemManager Inputs = Game.Input_Manager;

        public static readonly PlayerManager Player = Game.mainGame.playerManager;
        public static readonly EnemyManager Enemy = Game.mainGame.enemyManager;

        public static readonly ProjectileManager Projectile = Game.mainGame.projectileManager;
        public static readonly orbManager EXPorb = Game.mainGame.orbManager;

        public static readonly UI_Manager UI = Game.UI_Manager;

        public static readonly EquipmentDataManager EDM = Game.EquipmentDataManager;

        public static readonly Equipment EQ = Game.Equipment;

        public static readonly MapManager MAP = Game.Map_Manager;

        public static readonly Equipment EQU = Game.Equipment;

        public static readonly EventManager Event = Game.Event_Manager;

        public static readonly MiniMap MiniMAP = Game.MiniMap;

        public static readonly ObjectManager OBJ = Game.ObjectManger;


    }
    /*
    public class Inputs
    {

        public bool Button(string id)
        {
            return GameManager.Input_Manager.Button(id);
        }
        public bool ButtonUp(string id)
        {
            return GameManager.Input_Manager.ButtonUp(id);
        }
        public bool ButtonDown(string id)
        {
            return GameManager.Input_Manager.ButtonDown(id);
        }

        public Vector2 InputVector()
        {
            return GameManager.Input_Manager.InputVector();
        }
    }
    */

    namespace Projectiles
    {

    }

    namespace General
    {

    }

    namespace Special
    {

    }


}