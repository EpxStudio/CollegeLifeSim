using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Player : Being {

    GameObject entity;                      // this object (I reference entity in this script even though I dont need to)
    MovementScript movement;                // movement speed of the player. We can make the player slow down and speed up in the future
    public Vector3 rotaion;                 // future clamp on the rotation speed of the player
    public int rotationOffset = 90;
    private GameObject weaponSpot;          // the spot on the player transform where the gun is held
    public GameObject startGun;             // Starting gun passed to the player

    ArrayList gunArray = new ArrayList();   // list of held guns
    GameObject gun;                         // current gun being used
    int curGun;                             // place in gun array

    void Awake()
    {
        entity = GetComponent<Transform>().gameObject;
        movement = GetComponent<MovementScript>();
        isSolid = true;
        weaponSpot = transform.FindChild("GunSpot").gameObject;

        gun = Instantiate(startGun, weaponSpot.transform.position, weaponSpot.transform.rotation) as GameObject;
        gun.transform.parent = weaponSpot.transform;
        gunArray.Add(gun);
        curGun = 0;
        //Instantiate(mainWeapon, weaponSpot.transform.position, rotaion);
        //Debug.Log(weaponSpot.transform.position.y);
    }

    void Update()
    {
        updateRotation();
        updateMove();
        switchGuns();
    }

    public void updateMove()
    {
        entity.transform.position = 
            new Vector2(entity.transform.position.x + (movement.getX() * speed * Time.deltaTime),
                entity.transform.position.y + (movement.getY() * speed * Time.deltaTime));
    }

    public void updateRotation()
    {
        Vector3 difference = _GM.mouseLocation - entity.transform.position;  //movement.mousePosition
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        rotaion = Quaternion.Euler(1f, 1f, rotZ) * new Vector3(10f, 0f, 0f);
        entity.transform.rotation = Quaternion.Euler(0f, 0f, rotZ); // * (turnSpeed * Time.deltaTime) not needed
        //Debug.DrawRay(entity.transform.position, entity.transform.rotation * new Vector3(10f, 0f, 0f), Color.cyan);
        // to make the character turn more slowly, keep track of the last mouse position and then turn for a % of the current and previous over time
    }

    public void switchGuns()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (gunArray.Count != 1)
            {
                if (curGun + 1 == gunArray.Count)
                {
                    gun.SetActive(false);
                    curGun = 0;
                    gun = gunArray[curGun] as GameObject;
                    gun.SetActive(true);
                }
                else
                {
                    gun.SetActive(false);
                    curGun += 1;
                    gun = gunArray[curGun] as GameObject;
                    gun.SetActive(true);
                }
            }
        }
    }

    // called by gun pickups to add a new  
    public void addGun(GameObject newGun)
    {
        gunArray.Add(Instantiate(newGun, weaponSpot.transform.position, weaponSpot.transform.rotation));
        (gunArray[gunArray.Count-1] as GameObject).transform.parent = weaponSpot.transform;
        (gunArray[gunArray.Count - 1] as GameObject).transform.gameObject.SetActive(false);
    }
}
