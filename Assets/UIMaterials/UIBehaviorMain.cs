using UnityEngine;
using System.Collections;

public class UIBehaviorMain : MonoBehaviour {

    public GameObject mapPanel;
    public Camera mainCamera;
    public GameObject MyCanvas;
    private bool mapVis = false;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(GameObject.Find("EventSystem").transform.gameObject);
        //Debug.Log("hello1");
    }

    void OnLevelWasLoaded()
    {
        //print("UIMainCanvas");
        mainCamera = Camera.main;
        MyCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
        moveMap();
    }

    public void moveMap()
    {
        if (mapVis == false)
        {
            //Debug.Log(mainCamera);
            mapPanel.transform.position = new Vector2(mainCamera.transform.position.x - 3, mainCamera.transform.position.y + 6);
            mapVis = true;
        }
        else
        {
            mapPanel.transform.position = new Vector2(mainCamera.transform.position.x, mainCamera.transform.position.y - 50);
            mapVis = false;
        }
    }


    private class onNewLevel
    {

    }

}
