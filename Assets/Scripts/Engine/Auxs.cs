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

	public static Vector3 RandomVector3()
	{
		float x = Random.Range(-1f, 1f);
		float y = Random.Range(-1f, 1f);
		float z = Random.Range(-1f, 1f);

		return new Vector3(x, y, z);
	}

	public static string PrintArray<T>(T[] arr)
	{
		return PrintArray<T>(arr, "\n");
	}

	public static string PrintArray<T>(T[] arr, string delimiter)
	{
		if (arr.Length == 0) return "";

		string output = arr[0].ToString();
		for (int i = 1; i < arr.Length; i++)
		{
			output += delimiter + arr[i].ToString();
		}
		return output;
	}
}
