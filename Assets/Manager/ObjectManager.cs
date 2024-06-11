using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Managers_MainGame
{
    public AreaManager AreaManager;
    public orbManager orbManager;
    public ProjectileManager ProjectileManager;
    public SpriteObjectManager SpriteObjectManager;

    public override void OnGameStart()
    {
        ProjectileManager.OnGameStart();
    }

    public override void OnSectionChanged(Vector2Int newSction)
    {
        base.OnSectionChanged(newSction);
    }

    public void OnSectionChanged()
    {
        orbManager.OnSectionChanged();
    }













}
