using UnityEngine;
using System.Collections;


//Wall.cs is essentially a copy of this class. Delete Wall.cs?
public class WallEntity : Entity
{
    public bool OnCollisionEntity(Entity other)
    {
		return false;
    }
}
