using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;

    [Header("Obstacle Settings")]
    public List<GameObject> obstacleObjects;
    public int amount = 10;
    public float minDistance = 1.0f;

    [Header("Spawn Areas")]
    public List<Transform> spawnAreas;
    public GameObject ObstacleContainer;

    private List<Vector3> spawnedPositions;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public void SpawnObstacles()
    {
        int spawnedCount = 0;
        int maxAttemptsPerSpawn = 50;

        spawnedPositions = new List<Vector3>();

        for (int i = 0; i < amount; i++)
        {
            GameObject randomObstacleTemplate = obstacleObjects[Random.Range(0, obstacleObjects.Count)];

            Transform randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];
            Renderer renderer = randomArea.GetComponent<Renderer>();

            if (renderer == null) return;

            Bounds bounds = renderer.bounds;
            bool validPositionFound = false;

            for (int attempt = 0; attempt < maxAttemptsPerSpawn; attempt++)
            {
                float randomX = Random.Range(bounds.min.x, bounds.max.x);
                float randomZ = Random.Range(bounds.min.z, bounds.max.z);
                float y = bounds.max.y;

                Vector3 spawnPos = new Vector3(randomX, y, randomZ);

                if (IsValidPosition(spawnPos))
                {
                    GameObject newObstacle = Instantiate(randomObstacleTemplate, spawnPos, Quaternion.identity, ObstacleContainer.transform);
                    newObstacle.SetActive(true);

                    spawnedPositions.Add(spawnPos);
                    spawnedCount++;
                    validPositionFound = true;
                    break;
                }
            }

            if (!validPositionFound) continue;
        }
    }

    bool IsValidPosition(Vector3 newPos)
    {
        foreach (Vector3 pos in spawnedPositions)
        {
            if (Vector3.Distance(newPos, pos) < minDistance)
                return false;
        }
        return true;
    }
}
