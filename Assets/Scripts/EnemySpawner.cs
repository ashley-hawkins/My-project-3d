using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Bounds bounds;

    // getting a reference to the object you want to instantiate
    public GameObject enemyPrefab;

    void Start()
    {
        RandomSpawnEnemies(10);
    }

    void RandomSpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; ++i)
        {
            // creating this variable so I don't have to specify the full bounds.extents every time
            var extents = bounds.extents;

            // select a random position within the bounds that are specified in the editor
            Vector3 randomPosition = new(Random.Range(-extents.x, extents.x), Random.Range(-extents.y, extents.x), Random.Range(-extents.z, extents.z));
            randomPosition += bounds.center;

            SpawnEnemy(randomPosition);
        }
    }

    GameObject SpawnEnemy(Vector3 position)
    {
        // call the Instantiate function, using the enemyPrefab that is specified in the editor.
        return Instantiate(enemyPrefab, position, Quaternion.identity);
    }
}
