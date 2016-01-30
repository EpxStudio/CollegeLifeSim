using UnityEngine;
using System.Collections;
using System;

public class BoardController : MonoBehaviour
{
    public static BoardController instance;

    public static ArrayList boardObjects = new ArrayList();

    void Awake()
    {
        instance = this;
		DontDestroyOnLoad(transform.gameObject);
    }

    public static void Add(BoardObject board_obj)
    {
        boardObjects.Add(board_obj);
    }

    public static void Remove(BoardObject board_obj)
    {
        boardObjects.Remove(board_obj);
    }

    public static T[] GetAt<T>(int x, int y) where T : BoardObject
    {
        ArrayList objects = new ArrayList();

        foreach (BoardObject obj in boardObjects)
        {
            if ((obj is T) != false && obj.posX == x && obj.posY == y)
            {
                objects.Add(obj);
            }
        }

        return Array.ConvertAll(
            objects.ToArray(), new Converter<object, T>(delegate(object obj) {
                return (T) obj;
            })
        );
    }
}
