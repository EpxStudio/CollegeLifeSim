using UnityEngine;
using System.Collections;

public class PlayerEntity : Entity
{
    public static PlayerEntity instance;

    public override void OnAwake()
    {
        base.OnAwake();
        instance = this;
        isSolid = true;
    }
    
    public override void OnUpdate()
    {
        base.OnUpdate();

        if (ChatController.isVisible)
        {
            return;
        }

        // If we want to move and aren't currently, set a destination
        int dir_x = InputController.GetDirectionX();
        int dir_y = InputController.GetDirectionY();

        MoveTo(dir_x, dir_y, Constants.PLAYER_MOVE_WALK_TIME);
    }
}
