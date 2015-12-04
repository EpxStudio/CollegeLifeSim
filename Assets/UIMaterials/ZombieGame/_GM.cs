using UnityEngine;
using System.Collections;

public class _GM : MonoBehaviour {

    public static _GM instance;
    public static GameObject player;
    public static Vector3 mouseLocation;
    public static bool leftClickSinglePress;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    void Update()
    {
        mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
