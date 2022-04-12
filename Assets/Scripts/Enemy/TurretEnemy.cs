using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public ParticleSystem explosion;
    public GameObject bulletPrefab;
    public float bulletSpeed = 2f;
    public float health = 70f;
    public float shootInterval = 5f;
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
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Water") {
            health -= 1;
            Destroy(other.gameObject);
        }
    }
    private void Update() {
        if (health <= 0) {
            Destroy(gameObject);
            GameEvents.Screenshake();
            Instantiate(explosion, transform.position, Quaternion.identity);
            GameEvents.EnemyDie();
        }
    }
    private void Start() {
        InvokeRepeating("Shoot", 0.5f, shootInterval);
    }
    private void Shoot() {
        // Vector3 targetDir = player.position - transform.position;

        Vector2 shootDir = Random.insideUnitCircle.normalized;
        float mainAngle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        float perpendicularAngle = mainAngle + 90f;

        GameObject bullet1 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        GameObject bullet2 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        GameObject bullet3 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        GameObject bullet4 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Rigidbody2D bullet1RB = bullet1.GetComponent<Rigidbody2D>();
        Rigidbody2D bullet2RB = bullet2.GetComponent<Rigidbody2D>();
        Rigidbody2D bullet3RB = bullet3.GetComponent<Rigidbody2D>();
        Rigidbody2D bullet4RB = bullet4.GetComponent<Rigidbody2D>();

        bullet1RB.rotation = mainAngle;
        bullet2RB.rotation = mainAngle + 180;
        bullet3RB.rotation = perpendicularAngle;
        bullet4RB.rotation = perpendicularAngle + 180;

        Vector2 shootDir1 = new Vector2(Mathf.Cos(mainAngle * Mathf.Deg2Rad), Mathf.Sin(mainAngle * Mathf.Deg2Rad));
        Vector2 shootDir2 = new Vector2(Mathf.Cos(perpendicularAngle * Mathf.Deg2Rad), Mathf.Sin(perpendicularAngle * Mathf.Deg2Rad));

        bullet1RB.AddForce(shootDir1 * bulletSpeed, ForceMode2D.Impulse);
        bullet2RB.AddForce(-shootDir1 * bulletSpeed, ForceMode2D.Impulse);
        bullet3RB.AddForce(shootDir2 * bulletSpeed, ForceMode2D.Impulse);
        bullet4RB.AddForce(-shootDir2 * bulletSpeed, ForceMode2D.Impulse);
    }
}
