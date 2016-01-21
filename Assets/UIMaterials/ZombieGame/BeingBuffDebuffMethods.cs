using UnityEngine;
using System.Collections;

public class BeingBuffDebuffMethods : Being {
    /*
        This is a class full of static methods that preform the buff debuff effects on a being type object
    */

    //Buffs---------------------
	public static void buffAttack(Being be, float endTime, float increase)
    {
        if (endTime <= Time.time)
        {
            be.resetAttack();
        }
        else be.changeAttack(increase);
    }

    public static void buffSpeed(Being be, float endTime, float increase)
    {
        if (endTime <= Time.time)
        {
            be.resetSpeed();
        }
        else be.changeSpeed(increase);
    }


    //Debuffs ----------------------
    public static void debuffStun(Being be, float endTime)
    {
        if (endTime <= Time.time)
        {
            be.resetSpeed();
        }
        else be.speed = 0f;
    }

    public static void debuffSlow(Being be, float endTime, float decrease) // decrease should be a negative number
    {
        if (endTime <= Time.time)
        {
            be.resetSpeed();
        }
        else be.changeSpeed(decrease);
    }

    public static void debuffAttack(Being be, float endTime, float decrease) // decrease should be a negative number
    {
        if (endTime <= Time.time)
        {
            be.resetAttack();
        }
        else be.changeSpeed(decrease);
    }

    public static void debuffContinuousSlow(Being be, float endTime, float decrease)
    {
        if (endTime < Time.time)
        {
            be.resetSpeed();
        }
        else be.changeSpeed(decrease);
    }

    //Bonus Weapon damage -----------------------
    public static void bonusHolyDamage(Transform target, float weaponDamage) // this buff will do an extra 20% weapon damage to target if it is unholy ( Vampires )
    {
        if (target.tag.Equals("Vampire"))
        {
            target.GetComponent<Being>().takeDamage(weaponDamage * .2f);
            //Debug.Log(weaponDamage * .2f);
        }
    }
}
