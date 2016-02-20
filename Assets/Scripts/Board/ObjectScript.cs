using UnityEngine;
using System.Collections;

public class ObjectScript : Entity {

	public override void OnAwake()
	{
		base.OnAwake();
		
		//DialogueController.instance.Load(this, "npc_example");
		
		isSolid = true;  // the npc is a solid object. player can't move through it
	}
	
	public override bool OnCollisionSolid(Entity other)
	{
		return false;
	}
}
