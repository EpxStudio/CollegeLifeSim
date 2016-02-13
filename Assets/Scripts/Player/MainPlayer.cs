using UnityEngine;
using System.Collections;
using System.Collections.Generic;

static class MainPlayer {

    public static List<InventoryItem> Inventory = new List<InventoryItem>();

    //baseSmartness is a set value which can be added to by smartness which is gained from items

    //Int to keep track of money on character
    public static int money;

    private static int baseStress;
    private static int baseExhaustion;
    private static int baseHunger;

    public static int Stress
    {
        get
        {
            var toReturn =  baseStress;
            foreach (var i in Inventory)
            {
                toReturn += i.Stress;
            }
            return toReturn;
        }
    }

    public static int Exhaustion
    {
        get
        {
            var toReturn = baseExhaustion;
            foreach (var i in Inventory)
            {
                toReturn += i.Exhaustion;
            }
            return toReturn;
        }
    }

    public static int Hunger
    {
        get
        {
            var toReturn = baseHunger;
            foreach (var i in Inventory)
            {
                toReturn += i.Hunger;
            }
            return toReturn;
        }
    }

}
