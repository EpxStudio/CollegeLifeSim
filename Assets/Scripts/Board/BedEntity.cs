using UnityEngine;
using System.Collections;

public class BedEntity : Entity
{
    public override void OnAwake()
    {
        base.OnAwake();

		DialogueController.instance.Load(this, "Bed.txt");

        isSolid = true;  // the npc is a solid object. player can't move through it
    }

	public override bool OnCollisionSolid(Entity other)
	{
		DialogueController.instance.Step (this);
		return false;

	}

//	public override void OnDialogueFunction(string func)
//	{
//		if (func == "cutscene") {
//		}
//		return null;
//	}

    public override void OnAction()
    {
        base.OnAction();

		//ChatController.instance.SetDialogue(this);
    }
}
