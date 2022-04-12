using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject alertPrefab;
    public float spawnInterval = 3f;
    public float roomSize = 2.7f;

    private void Awake() {
        
    }
    void OnDisable() {

    }
    void OnEnable() {
        StartCoroutine(SpawnEnemy(spawnInterval, alertPrefab));
    }
    public void StartSpawning() {
        StartCoroutine(SpawnEnemy(spawnInterval, alertPrefab));
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy) {
        yield return new WaitForSeconds(interval);
        // Choose a spawn coordinate
        GameObject alert = Instantiate(alertPrefab, new Vector3(Random.Range(-roomSize, roomSize), Random.Range(-roomSize, roomSize), 0), Quaternion.identity);
        StartCoroutine(SpawnEnemy(interval, enemy));
    }
}
