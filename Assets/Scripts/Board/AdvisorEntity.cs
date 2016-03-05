using UnityEngine;
using System.Collections;

public class AdvisorEntity : Entity
{
	public override void OnAwake()
	{
		base.OnAwake();

		DialogueController.instance.Load(this, "IntroAdvisor.txt");

		isSolid = true;  // the npc is a solid object. player can't move through it
	}

	public override void OnUpdate()
	{
		base.OnUpdate();
	}

	public override bool OnCollisionSolid(Entity other)
	{
		print ("in collision");
		return false;
	}

	public override void OnAction()
	{
		base.OnAction();
		print ("in onAction");
		ChatController.instance.SetDialogue(this);
	}
}
