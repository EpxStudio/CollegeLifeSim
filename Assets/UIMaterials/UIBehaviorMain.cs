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
            mapPanel.transform.position = new Vector2(mainCamera.transform.position.x + 700, mainCamera.transform.position.y + 350);
            mapVis = true;
            GameMaster.uiVis = true;
        }
        else
        {
            mapPanel.transform.position = new Vector2(mainCamera.transform.position.x, mainCamera.transform.position.y - 200);
            mapVis = false;
            GameMaster.uiVis = false;
        }
    }


    private class onNewLevel
    {

    }

}
