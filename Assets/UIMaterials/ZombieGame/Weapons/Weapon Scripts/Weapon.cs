using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Weapon : MonoBehaviour {
    
    // gun stats
    public float fireRate = 0;
    public float damage = 10;
    public float range = 10;
    public int maxAmo;
    public Transform bulletTrail;
    int currentAmo;

    // values for the class
    public LayerMask notToHit;
    private GameObject player;
    float timeToFire = 0;
    bool click = false;
    Transform firePoint;
    private GameObject gm;

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
        Vector2 mousePosition = _GM.mouseLocation;
        Vector2 firePointVector = new Vector2(firePoint.transform.position.x, firePoint.transform.position.y);

        //Debug.DrawRay(firePoint.transform.position, firePoint.transform.rotation * new Vector3(10f, 0f, 0f), Color.black);
        //Debug.DrawLine(firePointVector, (mousePosition - firePointVector) * 100);

        foreach (Quaternion value in rayList)
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, (value * firePoint.transform.rotation) * new Vector3(1f, 0f, 0f), range, notToHit);
            Debug.DrawRay(firePoint.transform.position, (value * firePoint.transform.rotation) * new Vector3(range, 0f, 0f), Color.black);

            effect(value);

            if (hit.collider != null)
            {
                if (hit.transform.GetComponent<ZombieBasic>() != null)
                {
                    hit.transform.GetComponent<ZombieBasic>().takeDamage(damage);
                Debug.Log("it was hit " + hit.transform.name);
                }
            }
        }
    }

    void effect(Quaternion value)
    {
        Instantiate(bulletTrail, firePoint.position, value * firePoint.rotation);
    }

}
