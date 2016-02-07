using UnityEngine;
using System.Collections;

public class NPCEntity : Entity
{
    public override void OnAwake()
    {
        base.OnAwake();

		DialogueController.instance.Load(this, "npc_example");

        isSolid = true;  // the npc is a solid object. player can't move through it
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override bool OnCollisionSolid(Entity other)
    {
        // check which direction the player relative to me
        int xDir = Auxs.Sign(other.posX - posX);
        int yDir = Auxs.Sign(other.posY - posY);

        // 50% chance
        if (Random.Range(0f, 1f) > 0.5f)
        {
            // This shows the message in the chat and then moves the NPC away because he's shoved
            ChatController.Show("Hey, quit shoving.");
            
			return MoveTo(-xDir, -yDir, 0.3f);
		}
        else
        {
            // This will make two successive messages
            ChatController.Show("Don't shove me, pal!");
            ChatController.Show("Get out of my face.");

            // Move the other object (the player) away from the npc
            other.MoveTo(xDir, yDir, 0.3f);

            return false;
        }
    }

    public override void OnAction()
    {
        base.OnAction();

		ChatController.instance.SetDialogue(this);
    }
}
