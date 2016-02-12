using UnityEngine;
using System.Collections;

static class MainPlayer {
    //Object attributed to player of game
    //Possesses stats which are gained from items 

    //baseSmartness is a set value which can be added to by smartness which is gained from items
    private static int baseSmartness;

    public static int smartness
    {
        get
        {
            return baseSmartness;
        }
        private set
        {
            baseSmartness = value;
        }
    }    

}
