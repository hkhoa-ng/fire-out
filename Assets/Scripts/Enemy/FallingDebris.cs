using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDebris : MonoBehaviour
{
    public GameObject debris;
    public Rigidbody2D debrisRB;
    public BoxCollider2D debrisCollider;
    public float fallSpeed = 5f;
    public float scaleStep = 0.1f;
    private float initScale = 0f;
    public float scaleInterval = 0.15f;
    private Vector2 fallDir;
    public float lifeTime = 3f;
    public ParticleSystem explosion;
    

    void Start()
    {
        debrisRB = GetComponentInChildren<Rigidbody2D>();
        debrisCollider.enabled = false;

        // Choose a rangom falling direction
        fallDir = new Vector2(Random.Range(-0.5f, 0.5f), -1).normalized;
        float angle = Mathf.Atan2(fallDir.y, fallDir.x) * Mathf.Rad2Deg;
        var targetRotation = Quaternion.Euler (new Vector3(0f,0f,angle));
        transform.rotation = targetRotation;

        // Scaling
        debris.transform.localScale = new Vector3(initScale, initScale, 1);

        InvokeRepeating("Falling", 0, scaleInterval);
        Destroy(gameObject,lifeTime);
    }

    private void Falling() {
        if (debris != null) {
            // Move the debris toward fall position
            debris.transform.position = Vector2.MoveTowards(debris.transform.position, transform.position, fallSpeed);
            // Scale the debris up over time
            if (initScale <= 1) {
                initScale += scaleStep;
                debris.transform.localScale = new Vector3(initScale, initScale, 1);
                scaleStep -= 0.0005f;
            } else {
                debrisCollider.enabled = true;
            }
        }
    }

    private void OnDestroy() {
        Instantiate(explosion, transform.position, Quaternion.identity);
        GameEvents.Screenshake();
    }
    
}

