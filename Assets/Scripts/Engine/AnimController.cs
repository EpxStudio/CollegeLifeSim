using UnityEngine;
using System.Collections;

public class AnimController : MonoBehaviour
{
	public static AnimController instance;

	void Awake()
	{
		instance = this;
	}

	void SetAnimation()
	{
		
	}
}

class Animation
{
	public ArrayList animations;

	public Animation() { }

	public void Move(int x, int y)
	{
		
	}
}
