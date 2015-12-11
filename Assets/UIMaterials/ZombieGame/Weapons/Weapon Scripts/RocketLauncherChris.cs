using UnityEngine;
using System.Collections;

public class RocketLauncherChris : Weapon {

    public GameObject rocketProjectile;

    void start()
    {
        if (rocketProjectile == null)
        {
            Debug.Log("The rocketLauncher doesn't have a projectile in it");
        }
        multiTarget = false;
    }

    // override the shoot method because we'll be firing something... bigger
    protected override void shoot()
    {
        GameObject rock = Instantiate(rocketProjectile, firePoint.position, firePoint.rotation) as GameObject;

        rock.transform.GetComponent<RocketProjectile>().setDamage(damage);
        rock.transform.GetComponent<RocketProjectile>().setMask(notToHit);
        rock.transform.GetComponent<RocketProjectile>().setRange(range);
    }
}
