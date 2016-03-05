using UnityEngine;
using System.Collections;

public class FountainScript : Entity
{
	public override void OnAwake()
	{
		base.OnAwake();
		DialogueController.instance.Load(this, "fountain.txt");

		isSolid = true;  // the npc is a solid object. player can't move through it

	}

	public override void OnUpdate()
	{
		base.OnUpdate();
	}

	public override bool OnCollisionSolid(Entity other)
	{
		Debug.Log("In Collision");


		ChatController.instance.SetDialogue(this);
		return true;

	}

}
