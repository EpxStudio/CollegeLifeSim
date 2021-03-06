﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Weapon : MonoBehaviour {
    
    // I did a major update to this class to let guns hit multiple targets and to have a blood splat effect. Make sure that you add this effect to your weapons

    // gun stats
    public float fireRate = 0;
    public float damage = 10;
    public float range = 10;
    //public int maxAmo;
    public float critChance = 0f;                          // needs to be between 0 and 1
    public bool multiTarget = false;                       // if the ray can hit more than the first target hit
    //public float effectSpawnRate = 20;
    public Transform bulletTrail;
    public Transform hitEffect;
    public AudioClip fireSound;
    int currentAmo;

    // values for the class
    public LayerMask notToHit;
    private GameObject player;
    float timeToFire = 0;
    //bool click = false;
    protected Transform firePoint;
    //private GameObject gm;
    private float timeToSpawnEffect = 0;

    // list of rotation on vectors if mutiple vectors need to be fired from a gun
    [HideInInspector]
    public List<Quaternion> rayList = new List<Quaternion>();


    // if this is not being called that means a subclass of Weapon is being overridden. THIS CAN BE VERY ANNOYING!
	void Awake () {
        //player = GameObject.FindGameObjectWithTag("Player").transform.gameObject;
        firePoint = transform.FindChild("FirePoint");
        //gm = GameObject.FindGameObjectWithTag("GameMaster").gameObject;
        if (firePoint == null)
        {
            Debug.LogError("Can't find the 'FirePoint' of the weapon");
        }
        if (hitEffect == null)
        {
            Debug.Log("Can't find the 'hitEffect' of the weapon");
        }
        if ((critChance > 1f || critChance < 0.1f) && critChance != 0)
        {
            Debug.Log("The 'critChance' must be between .1 and 1");
            critChance = 0f;
        }

        // default bullet angle
        rayList.Add(Quaternion.Euler(0f, 0f, 0f));
    }

    
	void Update () {
        if (fireRate == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                shoot();
                if (fireSound != null)
                {
                    playSound();
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                shoot();
                if (fireSound != null)
                {
                    playSound();
                }
            }
        }
    }

    // shoot casts a number of rays eqaul to the amount of quarternions in the ray list. It is devided into multi target and first target hit
    protected virtual void shoot()
    {
        foreach (Quaternion value in rayList)
        {
            RaycastHit2D[] allHit = Physics2D.RaycastAll(firePoint.transform.position, (value * firePoint.transform.rotation) * new Vector3(1f, 0f, 0f), range, notToHit);

            if (multiTarget == true && allHit.Length > 1)            // if the gun can hit multiple targtes with a ray
            {
                foreach (RaycastHit2D tempHit in allHit)
                {
                    if (tempHit.collider != null)
                    {
                        if (tempHit.transform.GetComponent<ZombieAbstract>() != null)
                        {
                            tempHit.transform.GetComponent<ZombieAbstract>().takeDamage(checkCrit());
                            effectOnTarget(tempHit.transform);
                            //Debug.Log("it was hit " + hit.transform.name);
                        }
                    }

                    Vector3 hitPos;
                    Vector3 hitNormal;

                    if (tempHit.collider == null)
                    {
                        hitPos = firePoint.position + (((value * firePoint.transform.rotation) * new Vector3(1f, 0f, 0f)) * range);
                        hitNormal = new Vector3(9999, 9999, 9999);   // crazy vector so we know we didnt hit anything
                    }
                    else
                    {
                        hitPos = tempHit.point;
                        hitNormal = tempHit.normal;
                    }

                    effect(value, hitPos, hitNormal);
                }
            }
            else     // if a ray only hits the first target
            {
                RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, (value * firePoint.transform.rotation) * new Vector3(1f, 0f, 0f), range, notToHit);

                if (hit.collider != null)
                {
                    if (hit.transform.GetComponent<ZombieAbstract>() != null)
                    {
                        hit.transform.GetComponent<ZombieAbstract>().takeDamage(checkCrit());
                        effectOnTarget(hit.transform);
                        //Debug.Log("it was hit " + hit.transform.name);
                    }
                }

                Vector3 hitPos;
                Vector3 hitNormal;

                if (hit.collider == null)
                {
                    hitPos = firePoint.position + (((value * firePoint.transform.rotation) * new Vector3(1f, 0f, 0f)) * range);
                    hitNormal = new Vector3(9999, 9999, 9999);   // crazy vector so we know we didnt hit anything
                }
                else
                {
                    hitPos = hit.point;
                    hitNormal = hit.normal;
                    //Debug.Log(hitNormal);
                }

                effect(value, hitPos, hitNormal);

                /*if (Time.time >= timeToSpawnEffect)      this can be code to regulate the amount of bullets created on screen
                {
                    Vector3 hitPos;

                    if (hit.collider == null)
                        hitPos = (firePoint.transform.rotation) * new Vector3(range, 0f, 0f);
                    else
                        hitPos = hit.point;                

                    effect(value, hitPos);
                    timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
                }*/
            }
        }
    }

    void effect(Quaternion value, Vector3 hitPos, Vector3 hitNormal)
    {
        Transform trail = Instantiate(bulletTrail, firePoint.position, value * firePoint.rotation) as Transform;
        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if (lr != null && multiTarget != true)
        {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitPos);
        }
        else
        {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, firePoint.position + (((value * firePoint.transform.rotation) * new Vector3(1f, 0f, 0f)) * range));
        }

        Destroy(trail.gameObject, 0.04f);

        if (hitNormal != new Vector3(9999,9999,9999))
        {
            Transform tempEffect = Instantiate(hitEffect, hitPos, Quaternion.LookRotation(hitNormal)) as Transform;
            Destroy(tempEffect.gameObject, .5f);
        }
    }

    void playSound()
    {
        transform.GetComponent<AudioSource>().clip = fireSound;
        transform.GetComponent<AudioSource>().volume = 0.3f;
        transform.GetComponent<AudioSource>().Play();
    }

    // checkCrit will check if the shot was a crit and return the modified damage if it was. Will just return the origonal damage if not
    protected float checkCrit()
    {
        float tempF = Random.Range(0, 10);

        if ( tempF <= (critChance - .1f) * 10 && critChance > 0)
        {
            //Debug.Log("CRIT!");
            return (damage * 2f);
        }
        else return (damage);
    }

    protected virtual void effectOnTarget(Transform t)
    {
        // this is an empty method that can be overridden to add on hit effects. I did not make this abstract so that
        // people that made previous classes would not have to add a new method to their subclasses

        /*
        add this method to your subclass of weapon and then add methods that apply a status effects from the BeingBuffDebuffMethods class

        protected override void effectOnTarget(Transform t)
        {
        
        }

        */
    }
}
