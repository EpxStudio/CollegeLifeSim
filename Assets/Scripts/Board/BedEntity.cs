﻿using UnityEngine;
using System.Collections;

public class BedEntity : Entity
{
    public override void OnAwake()
    {
        base.OnAwake();

//		DialogueController.instance.Load(this, "npc_bed");

        isSolid = true;  // the npc is a solid object. player can't move through it
    }

	public override bool OnCollisionSolid(Entity other)
	{
		return false;
	}
    public override void OnAction()
    {
        base.OnAction();

		ChatController.instance.SetDialogue(this);
    }
}
