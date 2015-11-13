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
		//Does other code need to go here?
        base.OnUpdate();
    }

    public void OnCollisionEntity(Entity other)
    {
		//this code doesn't do anything
		//yay
        int xDir = Auxs.Sign(posX - other.posX);
        int yDir = Auxs.Sign(posY - other.posY);
    }
}
