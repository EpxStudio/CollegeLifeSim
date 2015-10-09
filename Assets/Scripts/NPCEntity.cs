using UnityEngine;
using System.Collections;

public class NPCEntity : Entity
{
    public override void OnAwake()
    {
        base.OnAwake();
        isSolid = true;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override bool OnCollisionSolid(Entity other)
    {
        int xDir = Auxs.Sign(posX - other.posX);
        int yDir = Auxs.Sign(posY - other.posY);

        if (Random.Range(0f, 1f) > 0.5f)
        {
            ChatController.Show("Hey, quit shoving.");
            MoveTo(xDir, yDir, 0.3f);
            return true;
        }
        else
        {
            ChatController.Show("Don't shove me, bitch!");
            ChatController.Show("Get out of my face.");
            other.MoveTo(-xDir, -yDir, 0.3f);
            return false;
        }
    }
}
