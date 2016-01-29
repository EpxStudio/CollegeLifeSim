using UnityEngine;
using System.Collections;
using System;

public class GunPickup : PickupScript
{
    public override void pickupAction(GameObject player)
    {
        player.transform.GetComponent<Player>().addGun(pickupObj);
    }
}
