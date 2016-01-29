using UnityEngine;
using System.Collections;

public class Entity : BoardObject
{
    public int lastPosX;
    public int lastPosY;

    public float timer = 0f;

    public bool isMoving = false;
    protected float timeToMove = 0f;
    protected Vector2 startPos;
    protected Vector2 endPos;

    public SpriteRenderer sprite;

    public bool isSolid = false;

    public override void OnAwake()
    {
        base.OnAwake();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        OnUpdate();

        // If this entity is moving, then slide it along to its destination
        if (isMoving)
        {
            realPos = Vector2.Lerp(startPos, endPos, timer / timeToMove);

            timer += Time.fixedDeltaTime;
            if (timer >= timeToMove)
            {
                isMoving = false;
                realPos = endPos;
            }
        }

        // Use 3/4s 3D power! Use y as z to make a 3d effect
        transform.position = new Vector3(realPos.x, realPos.y, 0f);
        sprite.sortingOrder = Mathf.RoundToInt(-1f * realPos.y * Constants.SORTING_ORDER_INTENSITY);
    }

    // Relative Position
    public bool MoveTo(int x, int y, float time_to_move = 0f)
    {
        return JumpTo(posX + x, posY + y, time_to_move);
    }

    // Absolute position
    public bool JumpTo(int x, int y, float time_to_move = 0f)
    {
        if (posX == x && posY == y)
            return true;

        if (isMoving)
            return false;

        if (isSolid)
        {
            Entity[] entities = GameObject.FindObjectsOfType<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity.isSolid && entity.posX == x && entity.posY == y)
                {
                    if (!entity.OnCollisionSolid(this))
                    {
                        return false;
                    }
                }
            }
        }

        startPos = new Vector2(posX, posY);
        posX = x;
        posY = y;
        endPos = new Vector2(posX, posY);
        timer = 0f;
        timeToMove = time_to_move;

        if (time_to_move <= 0f)
            return true;

        isMoving = true;
        return true;
    }

    // Return true if the position is now open to move into
	//Is this always false?
    public virtual bool OnCollisionSolid(Entity other) { return false; }

    // the player does an action on this thing
    public virtual void OnAction() { }
}
