using UnityEngine;
using System.Collections;

public class ZombieSilly : ZombieAbstract {

    public float speedChangeFrequency = 1.2f;
    public float speedChangeAmount = 0.1f;                   // the curSpeed is redundant to the speed of the being but I was just using it for testing
    private float maxSpeed;
    private float curSpeed;
    private Totter tot;
    private Totter change;
    private float time;

    enum Totter
    {
        Up,
        Down,
        Change,
        Stay
    }

    void Start()
    {
        maxSpeed = speed;
        curSpeed = speed;
    }

	void Update () {
        //Debug.Log(isAlive());
        checkHealth();
        updateRotation();
        totterSpeed();
        updateMove();
    }

    void totterSpeed()
    {
        time += Time.deltaTime;

        if (time >= speedChangeFrequency)
        {
            change = Totter.Change;
            time = 0f;
        }
        else
            change = Totter.Stay;


        if (change == Totter.Change)
        {
            if (curSpeed >= maxSpeed)
            {
                tot = Totter.Down;
                //Debug.Log("going Down");
            }
            else if (curSpeed <= maxSpeed / 2f)
            {
                tot = Totter.Up;
                //Debug.Log("going up");
            }

            if (tot == Totter.Up)
            {
                speed += speedChangeAmount;
                curSpeed += speedChangeAmount;
            }
            else if (tot == Totter.Down)
            {
                speed -= speedChangeAmount;
                curSpeed -= speedChangeAmount;
            }
        }
        //Debug.Log(curSpeed);
    }
}
