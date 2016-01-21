using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Being {

    GameObject entity;                      // this object (I reference entity in this script even though I dont need to)
    MovementScript movement;                // movement speed of the player. We can make the player slow down and speed up in the future
    public Vector3 rotaion;                 // future clamp on the rotation speed of the player
    private GameObject weaponSpot;          // the spot on the player transform where the gun is held
    public GameObject startGun;             // Starting gun passed to the player

    ArrayList gunArray = new ArrayList();   // array of held guns
    GameObject gun;                         // current gun being used
    int curGun;                             // place in gun array

    ArrayList itemsArray = new ArrayList(); // array of held items
    GameObject item;                        // current itme being used
    int curItem;                            // place in item array

    void Start()
    {
        entity = GetComponent<Transform>().gameObject;
        movement = GetComponent<MovementScript>();
        isSolid = true;
        weaponSpot = transform.FindChild("GunSpot").gameObject;

        gun = Instantiate(startGun, weaponSpot.transform.position, weaponSpot.transform.rotation) as GameObject;
        gun.transform.parent = weaponSpot.transform;
        gun.SetActive(true);
        gunArray.Add(gun);
        curGun = 0;

        attack = 0;
        MAX_ATTACK = 0;             // set to different attack
    }

    void Update()
    {
        updateRotation();
        updateMove();
        switchGuns();
    }

    public void updateMove()
    {
        // the first part of this if statment is to make sure that the player goes the right speed when traveling in a diagonal direction
        // this is not adding the x and y components directly
        if (movement.getY() != 0 && movement.getX() != 0)
        {
            float tempSpeed = Mathf.Pow(((speed * speed) / 2f), .5f);

            entity.transform.position =
            new Vector2(entity.transform.position.x + (movement.getX() * tempSpeed  * Time.deltaTime),
                entity.transform.position.y + (movement.getY() * tempSpeed  * Time.deltaTime));
        }
        else
        {
            entity.transform.position =
            new Vector2(entity.transform.position.x + (movement.getX() * speed * Time.deltaTime),
                entity.transform.position.y + (movement.getY() * speed * Time.deltaTime));
        }
    }

    public void updateRotation()
    {
        Vector3 difference = _GM.mouseLocation - entity.transform.position;  //movement.mousePosition
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        rotaion = Quaternion.Euler(1f, 1f, rotZ) * new Vector3(10f, 0f, 0f);
        entity.transform.rotation = Quaternion.Slerp(entity.transform.rotation, Quaternion.Euler(0, 0, rotZ), turnSpeed * Time.deltaTime);//Quaternion.Euler(0f, 0f, rotZ); 
        //Debug.DrawRay(entity.transform.position, entity.transform.rotation * new Vector3(10f, 0f, 0f), Color.cyan);

        // to make the character turn more slowly, keep track of the last mouse position and then turn for a % of the current and previous over time
        // this is what slerp does
    }

    // switch to the next held gun.  Will throw an error if no guns are being held
    public void switchGuns()
    {
        if (Input.GetKeyUp(KeyCode.Q) || Input.GetAxis("Mouse ScrollWheel") > 0)
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

    // called by gun pickups to add a new.  Will throw a out of bounds error if there were no guns being held
    public void addGun(GameObject newGun)
    {
        gunArray.Add(Instantiate(newGun, weaponSpot.transform.position, weaponSpot.transform.rotation));
        (gunArray[gunArray.Count-1] as GameObject).transform.parent = weaponSpot.transform;
        (gunArray[gunArray.Count - 1] as GameObject).transform.gameObject.SetActive(false);
    }

    // switch guns. This does not work if there are no weapons being held                  can go back and use modulus to eliminate the else
    public void switchItems()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (itemsArray.Count != 1 && itemsArray.Count != 0)
            {
                if (curItem + 1 == itemsArray.Count)
                {
                    item.SetActive(false);
                    curItem = 0;
                    item = itemsArray[curGun] as GameObject;
                    item.SetActive(true);
                }
                else
                {
                    item.SetActive(false);
                    curItem += 1;
                    item = itemsArray[curGun] as GameObject;
                    item.SetActive(true);
                }
            }
        }
    }

    // called by item pickups to add new item.   Does not work properly yet. Need to figure out the transform and spot to handle items
    public void addItem(GameObject newItem)
    {
        itemsArray.Add(Instantiate(newItem, weaponSpot.transform.position, weaponSpot.transform.rotation));
        if (itemsArray.Count != 0)
        {
            (itemsArray[itemsArray.Count - 1] as GameObject).transform.parent = weaponSpot.transform;
            (itemsArray[itemsArray.Count - 1] as GameObject).transform.gameObject.SetActive(false);
        }
        else
        {
            (itemsArray[itemsArray.Count] as GameObject).transform.parent = weaponSpot.transform;
            curItem = 0;
        }
    }

}

// need to implement the new cur/MAX_SPEED in the being class
