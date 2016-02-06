using UnityEngine;
using System.Collections;

public class FountainScript : Entity
{
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

		ChatController.Show ("Would you like to make a wish?");
		return true;

	}

}
