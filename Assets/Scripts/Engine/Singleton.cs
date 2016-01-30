using UnityEngine;
using System.Collections;

public class Singleton : MonoBehaviour
{
	public static Singleton instance;

	void Awake ()
	{
		if(!instance)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
