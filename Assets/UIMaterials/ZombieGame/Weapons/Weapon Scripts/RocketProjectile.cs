using UnityEngine;
using System.Collections;

public class RocketProjectile : MonoBehaviour {

    public float damage = 20;
    public float speed = 10;
    public float explosionRadius = 10f;
    float range = 30;
    public Transform hitEffect;
    public Transform flyEffect;

    [HideInInspector]
    public LayerMask toHit;
    Vector3 startPos;
    Transform flyEffectPoint;
    Transform flyEffectCur;

    void Awake()
    {
        startPos = transform.position;
        flyEffectPoint = transform.FindChild("effectPoint").transform;
        
        //flyEffectCur = Instantiate(flyEffect, flyEffectPoint.position, Quaternion.LookRotation(flyEffectPoint.position, Vector3.right)) as Transform;
        //Debug.Log(90 + flyEffectPoint.rotation.z);
    }

	void Update () {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
        //flyEffectCur.position = transform.position;

        if (Vector3.Distance(startPos, transform.position) > range)
        {
            explosion();
            effect();
            Destroy(transform.gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        //Debug.Log(toHit.value);
        if (c.IsTouchingLayers(toHit))//c.tag.Equals("Enemy") || c.tag.Equals("Block"))
        {
            explosion();
            effect();
            Destroy(transform.gameObject);
        }
    }

    void explosion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, toHit);

        foreach (Collider2D col in colliders)
        {
            if (col.transform.GetComponent<ZombieAbstract>() != null)
            {
                col.transform.GetComponent<ZombieAbstract>().takeDamage(damage);
            }
        }
    }

    void effect()
    {
        Transform effect = Instantiate(hitEffect, transform.position, transform.rotation) as Transform;
        Destroy(effect.gameObject, 1.5f);
    }

    public void setDamage(float dam)
    {
        damage = dam;
    }

    public void setMask(LayerMask la)
    {
        toHit = la;
    }

    public void setRange(float flo)
    {
        range = flo;
    }
}

// need a block collider
