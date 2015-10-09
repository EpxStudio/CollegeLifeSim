using UnityEngine;
using System.Collections;

public enum Keys {
    Up,
    Down,
    Left,
    Right,
    Advance
}

public class InputController : MonoBehaviour
{
    private static InputController instance;

    private delegate bool KeyFunc(KeyCode key);

    public Vector3 lastMousePosition;
    public Vector3 mousePosition;

    public int dirX = 0;
    public int dirY = 0;

    private ArrayList dirKeysDown = new ArrayList();

    void Awake()
    {
        instance = this;
        lastMousePosition = Input.mousePosition;
        mousePosition = Input.mousePosition;
    }

    void Update()
    {
        lastMousePosition = mousePosition;
        mousePosition = Input.mousePosition;

        /* Programmed specifically to fit these constraints:
         * 1. Pressing UP then pressing RIGHT will move you right.
         * 2. Pressing UP then pressing RIGHT then releasing RIGHT will move you up.
         * 3. Pressing UP then pressing RIGHT then releasing UP will move you right.
         * 4. Pressing UP then pressing DOWN will move you down.
         * 5. Pressing UP then pressing DOWN then releasing DOWN will move you up.
         * 6. Pressing UP then pressing DOWN then releasing UP will move you down.
         */

        if (GetKeyDown(Keys.Up))
        {
            dirKeysDown.Add(Keys.Up);
        }
        if (GetKeyDown(Keys.Down))
        {
            dirKeysDown.Add(Keys.Down);
        }
        if (GetKeyDown(Keys.Right))
        {
            dirKeysDown.Add(Keys.Right);
        }
        if (GetKeyDown(Keys.Left))
        {
            dirKeysDown.Add(Keys.Left);
        }

        if (GetKeyUp(Keys.Up))
        {
            dirKeysDown.Remove(Keys.Up);
        }
        if (GetKeyUp(Keys.Down))
        {
            dirKeysDown.Remove(Keys.Down);
        }
        if (GetKeyUp(Keys.Right))
        {
            dirKeysDown.Remove(Keys.Right);
        }
        if (GetKeyUp(Keys.Left))
        {
            dirKeysDown.Remove(Keys.Left);
        }

        dirX = 0;
        dirY = 0;
        if (dirKeysDown.Count >= 1)
        {
            Keys key_down = (Keys)dirKeysDown[dirKeysDown.Count - 1];
            if (key_down == Keys.Up)
                dirY = 1;
            if (key_down == Keys.Down)
                dirY = -1;
            if (key_down == Keys.Right)
                dirX = 1;
            if (key_down == Keys.Left)
                dirX = -1;
        }
    }

    // Disallow diagonal movements
    public static int GetDirectionX()
    {
        return instance.dirX;
    }

    public static int GetDirectionY()
    {
        return instance.dirY;
    }

    public static bool GetKey(Keys key)
    {
        return __GetKey(Input.GetKey, key);
    }

    public static bool GetKeyDown(Keys key)
    {
        return __GetKey(Input.GetKeyDown, key);
    }

    public static bool GetKeyUp(Keys key)
    {
        return __GetKey(Input.GetKeyUp, key);
    }

    private static bool __GetKey(KeyFunc keyFunc, Keys key)
    {
        if (key == Keys.Up)
        {
            return keyFunc(KeyCode.UpArrow) || keyFunc(KeyCode.W);
        }
        else if (key == Keys.Down)
        {
            return keyFunc(KeyCode.DownArrow) || keyFunc(KeyCode.S);
        }
        else if (key == Keys.Left)
        {
            return keyFunc(KeyCode.LeftArrow) || keyFunc(KeyCode.A);
        }
        else if (key == Keys.Right)
        {
            return keyFunc(KeyCode.RightArrow) || keyFunc(KeyCode.D);
        }
        else if (key == Keys.Advance)
        {
            return keyFunc(KeyCode.Space);
        }

        Debug.Log("Error: Key not recognized: " + key);
        return false;
    }
}
