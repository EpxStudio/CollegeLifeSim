using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

    private float currentY = 0;
    private float currentX = 0;

    // contains mouse positon but the _GM handles it now
    [HideInInspector]
    public Vector3 lastMousePosition;
    [HideInInspector]
    public Vector3 mousePosition;

    [HideInInspector]
    public bool rightClick = false;
    [HideInInspector]
    public bool leftClick = false;

    private GameObject entity;
    private ArrayList dirKeysDown = new ArrayList();

    void Awake()
    {
        entity = GetComponent<Transform>().gameObject;
        //instance = this;
        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {

        // this is now handled by the _GM script
        lastMousePosition = mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Press mouse buttons down
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            leftClick = true;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            rightClick = true;
        }

        // Pick mouse button up
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            leftClick = false;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            rightClick = false;
        }


        // Press the key down on keyboard
        if (Input.GetKeyDown(KeyCode.W))
        {
            dirKeysDown.Add(KeyCode.W);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            dirKeysDown.Add(KeyCode.A);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            dirKeysDown.Add(KeyCode.S);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            dirKeysDown.Add(KeyCode.D);
        }

        // Pick that shit up on keyboard
        if (Input.GetKeyUp(KeyCode.W))
        {
            dirKeysDown.Remove(KeyCode.W);
            currentY -= 1;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            dirKeysDown.Remove(KeyCode.A);
            currentX += 1;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            dirKeysDown.Remove(KeyCode.S);
            currentY += 1;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            dirKeysDown.Remove(KeyCode.D);
            currentX -= 1;
        }


        if (dirKeysDown.Count >= 1)
        {
            KeyCode currentKey = (KeyCode)dirKeysDown[dirKeysDown.Count -1];

            //Debug.Log(currentKey);

            if (currentKey == KeyCode.W) currentY = 1;
            if (currentKey == KeyCode.A) currentX = -1;
            if (currentKey == KeyCode.S) currentY = -1;
            if (currentKey == KeyCode.D) currentX = 1;
        }
    }

    public float getY()
    {
        return currentY;
    }

    public float getX()
    {
        return currentX;
    }

    public bool getRightClick()
    {
        return rightClick;
    }

    public bool getLeftClick()
    {
        return leftClick;
    }
}
