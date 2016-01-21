using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    private Transform mainCanvas;

    void Awake()
    {
        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;
    }

    void LateUpdate()
    {
        mainCanvas.FindChild("KillCountBox").GetComponent<Text>().text = "Kill Count: " + _GM.killCounter.ToString();
    }
}
