using UnityEngine;
using System.Collections;

public class PlayerEntity : Entity
{
    public static PlayerEntity instance;

    public int lastDirX = 0;
    public int lastDirY = 0;

    public override void OnAwake()
    {
        base.OnAwake();

        instance = this;  // There is only one player, and we can reference it using PlayerEntity.instance
        isSolid = true;  // The player is solid
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        // if the chat is running, don't let the player move
        if (!ChatController.isVisible)
        {
            UpdateMove();
        }

        // if the player tries to open a door or talk to a person, let's try it
        // but only if they stopped moving
        if (!isMoving)
        {
            UpdateAction();
        }
    }

    public void UpdateMove()
    {
        // get the direction the player is moving (diagonals are not allowed)
        int dir_x = InputController.GetDirectionX();
        int dir_y = InputController.GetDirectionY();

        // if the player is actually making the character move a direction
        if (dir_x != 0 || dir_y != 0)
        {
            // move that character!
            MoveTo(dir_x, dir_y, Constants.PLAYER_MOVE_WALK_TIME);

            // record the direction he moved (or attempted to move)
            lastDirX = dir_x;
            lastDirY = dir_y;
        }
    }

    public void UpdateAction()
    {
        // if the player presses the button
        if (InputController.GetKeyDown(Keys.Advance))
        {
            // TODO: do something?
			//What are you thinking?
            Entity[] objects = BoardController.GetAt<Entity>(posX + lastDirX, posY + lastDirY);

            foreach (Entity obj in objects)
            {
                if (obj == this)
                {
                    continue;
                }

                obj.OnAction();
            }
        }
    }
}
