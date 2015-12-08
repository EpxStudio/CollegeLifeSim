using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class MapPanelScript : MonoBehaviour {

    //Hashtable buttonList = new Hashtable ();
    //Hashtable sceneTable = new Hashtable();
    GameObject scrollView;
    Text text;
    
	void Awake()
    {
        //buttonList.Add("Advisor", GameObject.Find("MapPanel/MapButton"));
        //buttonList.Add("Dorm", GameObject.Find("MapPanel/MapButton (1)"));
        //buttonList.Add("Adviser", GameObject.Find("MapPanel/MapButton (2)"));
        //buttonList.Add("Template", GameObject.Find("MapPanel/MapButton (3)"));

        //sceneTable.Add("MapButton", "AdvisorRoom");

        // Change the text
        //(buttonList["Advisor"] as GameObject).transform.FindChild("Text").GetComponent<Text>().text = "Advisor";
        //(buttonList["Dorm"] as GameObject).transform.FindChild("Text").GetComponent<Text>().text = "Dorm";
        //(buttonList["Adviser"] as GameObject).transform.FindChild("Text").GetComponent<Text>().text = "Advisor";
        //(buttonList["Template"] as GameObject).transform.FindChild("Text").GetComponent<Text>().text = "Template";


        //remake buttons

        scrollView = GameObject.Find("MapPanel/InfoPanel");
        Debug.Log(scrollView.name);

    }

    public void desplayText (string s)
    {
        scrollView.transform.FindChild("Viewport").FindChild("Content").GetComponent<Text>().text = s;
    }


    public void changeScene(GameObject g)
    {
        if (Application.loadedLevel != g.GetComponent<MapButtonClass>().sceneNum)
        {
            Application.LoadLevel(g.GetComponent<MapButtonClass>().sceneNum);
        }
    }
}
