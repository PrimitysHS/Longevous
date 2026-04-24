using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;

    public int amountToSpawn = 10;
    public Transform player;
    public float minDistanceFromPlayer = 8f;

    void Start()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Transform chosenPoint = null;
        int tries = 20;

        while (tries > 0)
        {
            Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

            if (Vector2.Distance(point.position, player.position) >= minDistanceFromPlayer)
            {
                chosenPoint = point;
                break;
            }

            tries--;
        }

        if (chosenPoint == null) return;

        int randomEnemy = Random.Range(0, enemyPrefabs.Length);

        Instantiate(enemyPrefabs[randomEnemy], chosenPoint.position, Quaternion.identity);
    }
}