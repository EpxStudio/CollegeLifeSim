using UnityEngine;
using System.Collections;


//Wall.cs is essentially a copy of this class. Delete Wall.cs?
public class WallEntity : Entity
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
    
	public override bool OnCollisionSolid(Entity other)
	{
		return false;
	}
}
