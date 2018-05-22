using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    [SerializeField] GameObject meleeEnemyPrefab;
    [SerializeField] GameObject rangedEnemyPrefab;
    [SerializeField] int rangedSpawnCount = 0;
    [SerializeField] int meleeSpawnCount = 0;

    [SerializeField] float initialDelay = 0.5f;
    [SerializeField] float spawnTime = 3;

    float spawnTimer = 0;

    bool spawning = false;

    List<Transform> meleeSpawns = new List<Transform>();
    List<Transform> rangedSpawns = new List<Transform>();

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.tag == "Melee Spawn")
                meleeSpawns.Add(child);
            else if (child.tag == "Ranged Spawn")
                rangedSpawns.Add(child);
        }
    }

    void Update()
    {
        if (spawning)
            if (spawnTimer < 0)
                Spawn();
            else
                spawnTimer -= Time.deltaTime;
    }

    void Spawn()
    {
        // Stop spawning
        if (rangedSpawnCount == 0 && meleeSpawnCount == 0)
        {
            spawning = false;
            Destroy(gameObject);
        }

        // Reset the spawn time
        spawnTimer = spawnTime;

        // Try to spawn a enemy at each spawn point
        for (int i = 0; i < meleeSpawns.Count; i++)
            if (meleeSpawnCount > 0)
            {
                meleeSpawnCount--;
                Instantiate(meleeEnemyPrefab, meleeSpawns[i].position, meleeEnemyPrefab.transform.rotation);
            }

        for (int i = 0; i < rangedSpawns.Count; i++)
            if (rangedSpawnCount > 0)
            {
                rangedSpawnCount--;
                Instantiate(rangedEnemyPrefab, rangedSpawns[i].position, rangedEnemyPrefab.transform.rotation);
            }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            spawnTimer = initialDelay;
            spawning = true;
        }
    }
}
