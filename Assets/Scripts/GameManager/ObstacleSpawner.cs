using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject floorSmokePrefab;
    public GameObject viewSmokePrefab;
    public GameObject debrisPrefab;
    public float floorSmokeSpawnInterval = 4f;
    public float viewSmokeSpawnInterval = 8f;
    public float debrisSpawnInterval = 3f;
    public float roomSize = 3.2f;
    public bool spawnFloorSmoke = false;
    public bool spawnViewSmoke = false;
    public bool spawnDebris = false;

    private void Awake() {
        GameEvents.onStartSpawnFloorSmoke += StartSpawnFloorSmoke;
        GameEvents.onStartSpawnViewSmoke += StartSpawnViewSmoke;
        GameEvents.onStartSpawnDebris += StartSpawnDebris;
    }
    void OnDisable() {
        GameEvents.onStartSpawnFloorSmoke -= StartSpawnFloorSmoke;
        GameEvents.onStartSpawnViewSmoke -= StartSpawnViewSmoke;
        GameEvents.onStartSpawnDebris -= StartSpawnDebris;
    }
    void OnEnable() {
        StartSpawning();
    }
    public void StartMeteor() {
        StartSpawnDebris();
    }
    public void StartViewSmoke() {
        StartSpawnViewSmoke();
    }
    public void StartFloorSmoke() {
        StartSpawnFloorSmoke();
    }
    public void StartSpawning() {
        if (spawnDebris == true) StartSpawnDebris();
        if (spawnFloorSmoke == true) StartSpawnFloorSmoke();
        if (spawnViewSmoke == true) StartSpawnViewSmoke();
    }

    private IEnumerator SpawnObstacles(float interval, GameObject obstacle) {
        yield return new WaitForSeconds(interval);
        GameObject floorSmoke = Instantiate(obstacle, new Vector3(Random.Range(-roomSize, roomSize), Random.Range(-roomSize, roomSize), 0), Quaternion.identity);
        StartCoroutine(SpawnObstacles(interval, obstacle));
    }
    private void StartSpawnFloorSmoke() {
        // spawnFloorSmoke = true;
        StartCoroutine(SpawnObstacles(floorSmokeSpawnInterval, floorSmokePrefab));
    }
    private void StartSpawnViewSmoke() {
        // spawnViewSmoke = true;
        StartCoroutine(SpawnObstacles(viewSmokeSpawnInterval, viewSmokePrefab));
    }
    private void StartSpawnDebris() {
        // spawnDebris = true;
        StartCoroutine(SpawnObstacles(debrisSpawnInterval, debrisPrefab));
    }
}
