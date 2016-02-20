using UnityEngine;
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
		Debug.Log("In Collision");
		ChatController.Show("Do you want to sleep?");
		if (Input.GetKey ("y")) {
			ChatController.Show ("Sleep well my little bed bug.");
		}
		return false;

	}
    public override void OnAction()
    {
        base.OnAction();

		//ChatController.instance.SetDialogue(this);
    }
}
