using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapButtonClass : MonoBehaviour {

    public GameObject button;
    public string title;
    public string highlightText;
    public int sceneNum;

    public MapButtonClass(GameObject g, string t, string h, int n)
    {
        button = g;
        title = t;
        highlightText = h;
        sceneNum = n;
    }

    void Awake()
    {
        button.transform.FindChild("Text").GetComponent<Text>().text = title;
        //Debug.Log(highlightText);
    }

    public void highlightDisplay()
    {
        transform.parent.GetComponent<MapPanelScript>().desplayText(highlightText);
    }
}
