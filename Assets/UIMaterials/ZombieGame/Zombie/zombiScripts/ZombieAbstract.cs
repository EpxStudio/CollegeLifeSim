using UnityEngine;
using System.Collections;

public abstract class ZombieAbstract: Being {

    ZombieAbstract entity;
    public float visonRange = 10;

	void Awake()
    {
        entity = this;
    }

    void Update()
    {
        checkHealth();
        updateRotation();
        updateMove();
    }

    public void updateMove()
    {
        // float xDiff = _GM.player.transform.position.x - transform.position.x;
        // float yDiff = _GM.player.transform.position.y - transform.position.y;

        //transform.position = new Vector2(transform.position.x + ((xDiff/Mathf.Abs(xDiff)) * speed * Time.deltaTime),
        //transform.position.y + ((yDiff/Mathf.Abs(yDiff) * speed * Time.deltaTime)));

        transform.position = Vector2.MoveTowards(transform.position, _GM.player.transform.position, (speed * Time.deltaTime));
        
    }

    public void updateRotation()
    {
        Vector3 difference = _GM.player.transform.position - transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }

    public void checkHealth()
    {
        if (isAlive() == false)
        {
            Destroy(transform.gameObject);
        }
    }

    // needed if the zombie doesn't start off knowing where the player is
    public void visionCheck()
    {
        
    }
}
