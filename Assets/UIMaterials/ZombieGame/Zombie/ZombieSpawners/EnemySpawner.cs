using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemySpawned;
    public float spawnFrequency = 2f;
    public float spawnRange = 5f;

    void Awake()
    {
        if (enemySpawned == null)
        {
            Debug.Log("No enemey has been given to the spawner");
            Destroy(transform.gameObject);
        }
        //Debug.Log(enemySpawned);
    }

    void Start()
    {
        InvokeRepeating("spawn", spawnFrequency, spawnFrequency);
        //Debug.Log("here");
    }

    void spawn()
    {
        //Debug.Log(enemySpawned.transform);
        Instantiate(enemySpawned, transform.position + new Vector3(Random.Range(0f, spawnRange), Random.Range(0f, spawnRange), 0f), transform.rotation);
    }
}
