using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemy : MonoBehaviour
{
    public int health = 100;
    public GameObject fireballPrefab;
    private Transform player;
    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public float shootInterval = 5f;
    public float fireballSpeed = 2f;
    public ParticleSystem explosion;

    // EVENT HANDLER: SELF DESTROY WHEN PLAYER RESPAWN
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
    
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("AnimationTransition", 1.5f, shootInterval);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Water") {
            health -= 1;
            Destroy(collision.gameObject);
        }
    }
    private void Update() {
        // Rotate the sprite accordingly to player direction (left-right)
        Vector3 lookDir = player.transform.position - this.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        if (angle > 90 || angle < -90) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }

        // Check for health
        if (health <= 0) {
            Destroy(gameObject);
            GameEvents.Screenshake();
            Instantiate(explosion, transform.position, Quaternion.identity);
            GameEvents.EnemyDie();
        }
    }
    private void AnimationTransition() {
        animator.SetTrigger("Attack");
        Invoke("Shoot", 0.5f);
    }

    private void Shoot() {
        // Get the direction of the player to shoot toward them
        Vector3 targetDir = player.position - transform.position;
        Vector2 shootDir = new Vector2(targetDir.x, targetDir.y).normalized;
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        Rigidbody2D fireballRB = fireball.GetComponent<Rigidbody2D>();
        fireballRB.rotation = angle;
        fireballRB.AddForce(shootDir * fireballSpeed, ForceMode2D.Impulse);
    }
}
