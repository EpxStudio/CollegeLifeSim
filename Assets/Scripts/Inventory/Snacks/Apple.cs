using UnityEngine;
using System.Collections;

public class Apple : Snack
{
	public Apple()
	{
		PermanentBoost = false;
		Cost = 2;
		Hunger = 5;
	}
}
