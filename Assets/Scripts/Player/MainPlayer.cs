using UnityEngine;
using System.Collections;
using System.Collections.Generic;

static class MainPlayer {

    public static List<InventoryItem> Inventory = new List<InventoryItem>();

    //baseSmartness is a set value which can be added to by smartness which is gained from items
    
    private static int baseStress;

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
        private set
        {
            baseStress = value;
        }
    }    

}
