using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Weapon : MonoBehaviour {
    
    // I did a major update to this class to let guns hit multiple targets and to have a blood aplat effect. Make sure that you add this effect to your weapons

    // gun stats
    public float fireRate = 0;
    public float damage = 10;
    public float range = 10;
    public int maxAmo;
    public bool multiTarget = false;                       // if the ray can hit more than the first target hit
    public float effectSpawnRate = 20;
    public Transform bulletTrail;
    public Transform hitEffect;
    int currentAmo;

    // values for the class
    public LayerMask notToHit;
    private GameObject player;
    float timeToFire = 0;
    bool click = false;
    Transform firePoint;
    private GameObject gm;
    private float timeToSpawnEffect = 0;

    // list of rotation on vectors if mutiple vectors need to be fired from a gun
    [HideInInspector]
    public List<Quaternion> rayList = new List<Quaternion>();


    // if this is not being called that means a subclass of Weapon is being overridden. THIS CAN BE VERY ANNOYING!
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player").transform.gameObject;
        firePoint = transform.FindChild("FirePoint");
        gm = GameObject.FindGameObjectWithTag("GameMaster").gameObject;
        if (firePoint == null)
        {
            Debug.LogError("Can't find the 'FirePoint' of the weapon");
        }
        if (hitEffect == null)
        {
            Debug.Log("Can't find the 'hitEffect' of the weapon");
        }

        rayList.Add(Quaternion.Euler(1f, 1f, 1f));
    }

    
	void Update () {
        if (fireRate == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                shoot();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                shoot();
            }
        }
    }

    void shoot()
    {
        //Vector2 mousePosition = _GM.mouseLocation;
        //Vector2 firePointVector = new Vector2(firePoint.transform.position.x, firePoint.transform.position.y);

        //Debug.DrawRay(firePoint.transform.position, firePoint.transform.rotation * new Vector3(10f, 0f, 0f), Color.black);
        //Debug.DrawLine(firePointVector, (mousePosition - firePointVector) * 100);

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
                            tempHit.transform.GetComponent<ZombieAbstract>().takeDamage(damage);
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
                        hit.transform.GetComponent<ZombieAbstract>().takeDamage(damage);
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
            Transform tempEffect = Instantiate(hitEffect, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal)) as Transform;
            Destroy(tempEffect.gameObject, .5f);
        }
    }

}
