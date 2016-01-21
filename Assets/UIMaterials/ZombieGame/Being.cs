using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Being : MonoBehaviour {

    protected float MAX_HEALTH;
    protected float MAX_SPEED;
    protected float MAX_ATTACK;

    public float health;
    public float speed;
    public float attack = 5;
    public float turnSpeed;
    [HideInInspector]
    public bool isSolid;
    private bool alive;

    // List of current Buffs and Debuffs on the Being
    protected LinkedList<StatusEffect> statusList = new LinkedList<StatusEffect>();   // can make this into an array list for better performance

    // initialize the health, speed and alive
    void Awake()
    {
        MAX_HEALTH = health;
        MAX_SPEED = speed;
        MAX_ATTACK = attack;
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

    public void changeSpeed(float change)
    {
        if (MAX_SPEED + change < 0)
        {
            speed = 0;
        }
        else speed = MAX_SPEED + change;
    }

    public void changeAttack(float change)
    {
        if (MAX_ATTACK + change < 0)
        {
            attack = 0;
        }
        else attack = MAX_ATTACK + change;
    }

    public void resetSpeed()
    {
        speed = MAX_SPEED;
    }

    public void resetAttack()
    {
        attack = MAX_ATTACK;
    }

    public void addBuff(Buff b, float duration, float factor)
    {
        statusList.AddLast(new StatusEffect(b, duration, factor));                // these both addLast because then the most current buff or debuff will
    }                                                                             // be the buff or debuff apllied

    public void addDebuff(Debuff d, float duration, float factor)
    {
        statusList.AddLast(new StatusEffect(d, duration, factor));
    }

    protected void checkBuffs()
    {
        List<StatusEffect> removeLater = new List<StatusEffect>();

        foreach (StatusEffect effect in statusList)
        {
            if (effect.buff != Buff.None) // check the buffs
            {
                switch (effect.buff)
                {
                    case Buff.Attack:
                        BeingBuffDebuffMethods.buffAttack(this, effect.endTime, effect.factor);
                        break;
                    case Buff.Speed:
                        BeingBuffDebuffMethods.buffSpeed(this, effect.endTime, effect.factor);
                        break;
                }
            }
            else // check the debuffs
            {
                switch (effect.debuff)
                {
                    case Debuff.Stun:
                        BeingBuffDebuffMethods.debuffStun(this, effect.endTime);
                        break;
                    case Debuff.Slow:
                        BeingBuffDebuffMethods.debuffSlow(this, effect.endTime, effect.factor);
                        break;
                    case Debuff.Attack:
                        BeingBuffDebuffMethods.debuffAttack(this, effect.endTime, effect.factor);
                        break;
                }
            }
            if (effect.endTime <= Time.time) removeLater.Add(effect);
        }

        foreach (StatusEffect effect in removeLater)
        {
            statusList.Remove(effect);
        }
    }

    // There is a better way of managing these enums and how they are viewed in the status effect class, but I'm lazy and this works
    public enum Debuff
    {
        None,
        Stun,
        Slow,
        Attack
    }
    public enum Buff
    {
        None,
        Attack,
        Speed
    }

    protected class StatusEffect
    {
        public Buff buff;
        public Debuff debuff;
        public float endTime;
        public float duration;
        public float factor;

        public StatusEffect(Buff e, float duration, float fac)
        {
            buff = e;
            debuff = Debuff.None;
            endTime = duration + Time.time;
            this.duration = duration;
            factor = fac;
        }

        public StatusEffect(Debuff e, float duration, float fac)
        {
            debuff = e;
            buff = Buff.None;
            endTime = duration + Time.time;
            this.duration = duration;
            factor = fac;
        }
    }
}

// we can easily make the most significant buff or debuff out rank lesser ones by making a seperate list for debuff and buff and then implementing 
// differance methods to check which one will be more influential on the being. Like Stun would overirde all update methods