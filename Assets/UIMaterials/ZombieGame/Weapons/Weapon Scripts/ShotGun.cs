using UnityEngine;
using System.Collections;

public class ShotGun : Weapon {

	void Start () {
        //damage = 10;

        rayList.Add(Quaternion.Euler(1f, 1f, 10f));
        rayList.Add(Quaternion.Euler(1f, 1f, -10f));
        rayList.Add(Quaternion.Euler(1f, 1f, 5f));
        rayList.Add(Quaternion.Euler(1f, 1f, -5f));
    }
    //Debug.DrawLine(firePointVector, (mousePosition - firePointVector) * 100, Color.red);
    //Debug.DrawLine(firePointVector, Quaternion.Euler(1f, 1f, 5f) * (mousePosition - firePointVector) * 100);
    //Debug.DrawLine(firePointVector, Quaternion.Euler(1f, 1f, -5f) * (mousePosition - firePointVector) * 100, Color.yellow);
    //Debug.DrawLine(firePointVector, Quaternion.Euler(1f, 1f, 10f) * (mousePosition - firePointVector) * 100);
    //Debug.DrawLine(firePointVector, Quaternion.Euler(1f, 1f, -10f) * (mousePosition - firePointVector) * 100, Color.yellow);

    protected override void effectOnTarget(Transform t)
    {
        t.GetComponent<ZombieAbstract>().addDebuff(Being.Debuff.Slow, .5f, -1f);
    }
}
