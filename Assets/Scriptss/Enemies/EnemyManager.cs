using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public bool spawnOnStart = false;
    public int maxEnemyCount = 10;
    public int currentEnemyCount;

    [Tooltip("Enemy spawning speed per second")]
    public float enemySpawningSpeed = .7f;
    public bool isSpawningEnemy = false;
    public bool hasSpawnedEnemy = false;
    public Transform enemySpawnSpot;

    public GameObject enemyPrefab;
    public List<GameObject> enemyList = new();

    public static EnemyManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (spawnOnStart)
            SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        // spawn enemy
        isSpawningEnemy = true;
        InstantiateEnemy();
    }

    void InstantiateEnemy()
    {
        GameObject _e = Instantiate(enemyPrefab, transform);
        enemyList.Add(_e);
        _e.transform.position = new Vector3(Random.Range(-.5f, .5f), 0, Random.Range(-.5f, .5f)) + enemySpawnSpot.position;
        currentEnemyCount++;

        if (currentEnemyCount < maxEnemyCount)
        {
            Invoke(nameof(InstantiateEnemy), enemySpawningSpeed);
        }
        else
        {
            isSpawningEnemy = false;
            hasSpawnedEnemy = true;
        }
    }

    void LateUpdate()
    {
        // if not spawning enemy and no enemy on list
        if (!isSpawningEnemy && enemyList.Count == 0 && hasSpawnedEnemy)
        {
            Debug.Log("Enemy Cleared");
        }
    }

    public void DamageRandom()
    {
        int x = Random.Range(0, enemyList.Count);
        enemyList[x].GetComponent<EnemyInstance>().TakeDamage(100);
    }
}
