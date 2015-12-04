using UnityEngine;
using System.Collections;

public abstract class Being : MonoBehaviour {

    private readonly float MAX_HEALTH;

    public float health;
    public float speed;
    public float turnSpeed;
    [HideInInspector]
    public bool isSolid;
    private bool alive;

    // initialize the health and alive
    public Being()
    {
        MAX_HEALTH = health;
        alive = true;
    }

    // pass a positive or negitive to change the health
    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            alive = false;
        }
    }

    // will heal only up to max health
    public void heal(float heal)
    {
        if (health + heal > MAX_HEALTH)
        {
            health = MAX_HEALTH;
        }
        else health += heal;
    }

    // returns bool of the current alive status
    public bool isAlive()
    {
        return alive;
    }
}

