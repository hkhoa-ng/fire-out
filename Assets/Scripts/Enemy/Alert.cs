using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy", 1f);
        Destroy(gameObject, 1f);
        spawnPos = transform.position;
    }

    void SpawnEnemy() {
        // Choose a random enemy to spawn
        int enemyTypesNum = enemyPrefabs.Length;
        GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyTypesNum)];
        GameObject enemy = Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);
    }
}
