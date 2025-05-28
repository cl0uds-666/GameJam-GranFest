using UnityEngine;

public class TrackObjectSpawner : MonoBehaviour
{
    [Header("Spawn Prefabs")]
    [SerializeField] private GameObject[] spawnPrefabs; // Tyre stack, oil spill, etc

    [Header("Spawn Points (on-track only)")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Spawn Settings")]
    [SerializeField] private int initialSpawnCount = 10;
    [SerializeField] private bool spawnOverTime = false;
    [SerializeField] private float spawnInterval = 2f;

    private float timer;

    void Start()
    {
        // Initial spawn at game start
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnOne();
        }
    }

    void Update()
    {
        if (!spawnOverTime) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnOne();
        }
    }

    void SpawnOne()
    {
        if (spawnPoints.Length == 0 || spawnPrefabs.Length == 0)
        {
            Debug.LogWarning("No spawn points or prefabs assigned to TrackObjectSpawner.");
            return;
        }

        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject prefab = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];
        Instantiate(prefab, point.position, Quaternion.identity);
    }
}
