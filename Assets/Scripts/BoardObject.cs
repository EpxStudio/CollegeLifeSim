using UnityEngine;
using System.Collections;

public class BoardObject : MonoBehaviour
{
    public int posX;
    public int posY;

    public Vector2 realPos;  // Only includes x and y. z is generated.

    void Awake()
    {
        realPos = transform.position;
        posX = Mathf.RoundToInt(realPos.x);
        posY = Mathf.RoundToInt(realPos.y);

        transform.position = new Vector3(realPos.x, realPos.y, 0f);

        OnAwake();
    }

    void Start()
    {
        BoardController.Add(this);

        OnStart();
    }

    public virtual void OnAwake() { }
    public virtual void OnStart() { }
    public virtual void OnUpdate() { }
}
