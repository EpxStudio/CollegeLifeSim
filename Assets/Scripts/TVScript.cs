using UnityEngine;
using System.Collections;

public class TVScript : Entity
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

		ChatController.Show ("Today on Dino News!!!");
		return true;

	}

}