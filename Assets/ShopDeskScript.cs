using UnityEngine;
using System.Collections;

public class ShopDeskScript : Entity {

    public override bool OnCollisionSolid(Entity other)
    {
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        CalculusTextbook myBook = new CalculusTextbook();
        MainPlayer.Inventory.Add(myBook);
        Debug.Log("Added new Calc Book");
    }
}
