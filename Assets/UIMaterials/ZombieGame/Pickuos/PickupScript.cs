using UnityEngine;
using System.Collections;

public abstract class PickupScript : MonoBehaviour {

    public GameObject pickupObj;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag.Equals("Player"))
        {
            pickupAction(c.transform.gameObject);
            Destroy(transform.gameObject);
        }
    }

    public abstract void pickupAction(GameObject player);
}
