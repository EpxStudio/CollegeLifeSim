using UnityEngine;
using System.Collections;

// Auxilliary functions
//Should this be a static class?
public class Auxs
{

    public static int Sign(int num)
    {
        return num > 0 ? 1 : (num < 0 ? -1 : 0);
    }

    public static Rect GenRectPadding(Rect rect, int padding)
    {
        return GenRectPadding(rect, padding, padding);
    }

    public static Rect GenRectPadding(Rect rect, int paddingW, int paddingH)
    {
        Rect newRect = new Rect(rect);
        newRect.x += paddingW;
        newRect.y += paddingH;
        newRect.width -= 2f * paddingW;
        newRect.height -= 2f * paddingH;
        return newRect;
    }
}
