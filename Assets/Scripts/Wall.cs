using UnityEngine;
using System.Collections;

public class Wall : Entity
{
    public override void OnAwake()
    {
        base.OnAwake();
        isSolid = true;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public void OnCollisionEntity(Entity other)
    {
        int xDir = Auxs.Sign(posX - other.posX);
        int yDir = Auxs.Sign(posY - other.posY);
    }
}
