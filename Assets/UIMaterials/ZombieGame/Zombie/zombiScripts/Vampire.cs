using UnityEngine;
using System.Collections;

public class Vampire : ZombieAbstract {

    public float teleportFreq = .5f;
    public float teleportRange = 4f;
    public Transform telEffectEnd;
    public Transform telEffectBeg;
    float timeTel = 0;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            heal(MAX_HEALTH/2);

            float rand = Random.Range(0f, 360f);

            Quaternion angle = transform.rotation * Quaternion.Euler(1f, 1f, rand);
            Vector3 tDirection = angle * Vector3.right;

            Transform begTel = Instantiate(telEffectBeg, transform.position, transform.rotation) as Transform;
            Destroy(begTel.gameObject, 1f);

            transform.position = transform.position + tDirection * 40f;

            Transform endTel = Instantiate(telEffectEnd, transform.position, transform.rotation) as Transform;
            Destroy(endTel.gameObject, 1f);
        }
    }

    protected override void updateMove()
    {
        //Debug.Log("newMove");
        if (Time.time > timeTel && Vector3.Distance(_GM.player.transform.position, transform.position) > 2f + teleportRange)
        {
            timeTel = Time.time + 1 / teleportFreq;
            teleport();
            transform.position = Vector2.MoveTowards(transform.position, _GM.player.transform.position, (speed * Time.deltaTime));
        }
        else if (Vector3.Distance(_GM.player.transform.position, transform.position) > 2f + teleportRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, _GM.player.transform.position, (speed * Time.deltaTime));
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, _GM.player.transform.position, (2.2f * speed * Time.deltaTime));
        }
    }

    void teleport()
    {
        float rand = Random.Range(-1, 2);
        Quaternion startAngle = Quaternion.Euler(1f,1f, rand * 55f);
        Quaternion angle = transform.rotation * startAngle;
        Vector3 tDirection = angle * Vector3.right;
        Vector3 tPoint = transform.position + tDirection * teleportRange;

        Transform begTel = Instantiate(telEffectBeg, transform.position, transform.rotation) as Transform;
        Destroy(begTel.gameObject, 1f);

        //StartCoroutine(delay(.7f));

        transform.position = tPoint;
        Transform endTel = Instantiate(telEffectEnd, transform.position, transform.rotation) as Transform;
        Destroy(endTel.gameObject, 1f);    

        //Debug.Log("tel");
    }

    IEnumerator delay(float wait)
    {
        yield return new WaitForSeconds(wait);
    }
}
