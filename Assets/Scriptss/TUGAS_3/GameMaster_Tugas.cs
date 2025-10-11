using UnityEngine;
using TMPro;

public class GameMaster_Tugas : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int currentScore = 0;
    public Transform objectSpawner;
    public GameObject obstaclePrefab;

    public bool canSpawn = true;
    public float spawnTimer = 3f;
    private float currentSpawnTimer;
    public static GameMaster_Tugas instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("ScoreUI").GetComponent<TextMeshProUGUI>();
        currentSpawnTimer = 0f;
        currentScore = 0;
    }

    void Update()
    {
        scoreText.text = currentScore.ToString();

        if (spawnTimer <= 0f)
        {
            SpawnObject();
            currentSpawnTimer = spawnTimer;
        }
    }

    void FixedUpdate()
    {
        currentSpawnTimer *= Time.fixedDeltaTime;
    }

    void SpawnObject()
    {
        // spawnObstaclePrefab
        Transform obs = Instantiate(obstaclePrefab.transform);
        obs.position = objectSpawner.position;
        obs.parent = objectSpawner;
    }
}
