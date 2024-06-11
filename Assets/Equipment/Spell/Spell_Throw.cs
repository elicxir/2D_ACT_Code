using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Throw : SpellFunction
{

    public Throw_Projectile_Property[] throw_Projectile_Property;
    public override void Fire(Vector2 pos, Vector2 dir)
    {
        foreach(Throw_Projectile_Property p in throw_Projectile_Property)
        {
            Vector2 dir1 = PFV(p.Direction).normalized * p.Speed + Vector2.right* player.Velocity.x * p.InertiaRatio;
            projectileManager.Fire(p.projectile, pos+PFV(p.offset), dir1, dir1.magnitude, player);
        }
    }

    Vector2 PFV(Vector2 b)
    {
        if (player.FrontVector.x > 0)
        {
            return b;
        }
        else
        {
            return Vector2.left * b.x + Vector2.up * b.y;
        }
    }

}

[System.Serializable]
public class Throw_Projectile_Property
{
    public Projectile projectile;
    public Vector2 offset;
    /// <summary>
    /// ‰EŒü‚«‚ğŠî€‚É
    /// </summary>
    public Vector2 Direction;

    public int Speed;
    public float InertiaRatio = 0;
}
