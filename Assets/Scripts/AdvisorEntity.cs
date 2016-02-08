using UnityEngine;
using System.Collections;

public class AdvisorEntity : Entity
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
		
		ChatController.Show ("Hello welcome to college, I will be you're advisor");

		return true;

	}

}