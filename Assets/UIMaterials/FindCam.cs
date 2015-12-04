using UnityEngine;
using System.Collections;

public class FindCam : MonoBehaviour {

    void OnLevelWasLoaded()
    {
        transform.GetComponent<UIBehaviorMain>().mainCamera = Camera.main;
        transform.GetComponent<Canvas>().worldCamera = Camera.main;
        //print("it worked");
    }
	
}
