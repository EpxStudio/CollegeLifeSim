using UnityEngine;
using System.Collections;

public class BackgroundBlock : BoardObject
{
    public SpriteRenderer[] sprites;

    public override void OnAwake()
    {
        base.OnAwake();

        sprites = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.sortingOrder = Mathf.RoundToInt(realPos.y * Constants.SORTING_ORDER_INTENSITY);
        }
    }
}
