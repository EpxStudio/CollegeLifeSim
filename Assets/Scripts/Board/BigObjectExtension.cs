using UnityEngine;
using System.Collections;

public class BigObjectExtension : Entity
{
	public Entity parent; // all actions will be redirected onto this object.

	public override void OnAwake()
	{
		base.OnAwake();

		isSolid = true;  // the npc is a solid object. player can't move through it
	}

	public override void OnUpdate()
	{
		base.OnUpdate();
	}

	public override bool OnCollisionSolid(Entity other)
	{
		return parent.OnCollisionSolid(other);
	}

	public override void OnAction()
	{
		parent.OnAction();
	}
}
