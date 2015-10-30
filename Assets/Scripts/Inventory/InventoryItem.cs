using UnityEngine;
using System.Collections;

public abstract class InventoryItem
{	
	//boolean to show if said item is passive or active (used once then gone, i.e. not permament)
	public boolean PermanentBoost;
	public int Experience;
	public int Knowledge;
	public int Hunger;
	public int Cost;
	public int Energy;
	public int Happiness;
}
