using UnityEngine;
using System.Collections;

public class Pistol : Weapon {

    protected override void effectOnTarget(Transform t)
    {
        BeingBuffDebuffMethods.bonusHolyDamage(t, damage);
    }
}
