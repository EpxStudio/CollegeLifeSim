using UnityEngine;
using System.Collections;
using System.Collections.Generic;

static class MainPlayer {

    public static List<InventoryItem> Inventory = new List<InventoryItem>();

    //baseSmartness is a set value which can be added to by smartness which is gained from items
    
    private static int baseSmartness;

    public static int smartness
    {
        get
        {
            var toReturn =  baseSmartness;
            foreach (var i in Inventory)
            {
                toReturn += i.smartness;
            }
            return toReturn;
        }
        private set
        {
            baseSmartness = value;
        }
    }    

}
