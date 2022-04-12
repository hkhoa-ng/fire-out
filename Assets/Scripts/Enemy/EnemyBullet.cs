using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public ParticleSystem explosion;
 
    private void Awake() {
        GameEvents.onPlayerDieDestroyObjects += SelfDestruct;
    }
    private void OnDisable() {
        
        GameEvents.onPlayerDieDestroyObjects -= SelfDestruct;
    }
    private void SelfDestruct() {
        Destroy(gameObject);
        GameEvents.Screenshake();
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}
