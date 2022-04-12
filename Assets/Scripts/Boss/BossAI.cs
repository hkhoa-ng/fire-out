using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAI : MonoBehaviour
{
    // Animation states
    const string BOSS_IDLE = "BossIdle";
    const string BOSS_METEOR = "BossMeteor";
    const string BOSS_SHOOT = "BossShoot";
    const string BOSS_SUMMON = "BossSummon";
    const string BOSS_CHASE = "BossChase";
    private Animator animator;
    private string currentState;
    private Transform playerPos;
    private SpriteRenderer spriteRenderer;
    private Transform startPos;
    private int index = 0;
    
    private string[] attackStates = {"shoot", "meteor", "chase", "summon"};
    public float idleDuration = 2f;
    public float shootDuration = 2f;
    public float chaseDuration = 5f;
    public float summonDuration = 2f;
    public float meteorDuration = 2f;
    public float chaseSpeed = 4f;
    public int enemiesSpawnNum = 3;
    public int shootNum = 5;
    public int meteorSpawn = 3;
    public GameObject bulletPrefab;
    public float bulletSpeed = 3f;
    public GameObject alertPrefab;
    public GameObject meteorPrefab;
    public ParticleSystem explosion;
    public float health = 3000f;
    private void OnDisable() {
        GameEvents.onPlayerDieDestroyObjects -= SelfDestruct;
    }
    private void SelfDestruct() {
        Destroy(gameObject);
        GameEvents.Screenshake();
        // Instantiate(explosion, transform.position, Quaternion.identity);
    }
    
    private void Awake() {
        GameEvents.onPlayerDieDestroyObjects += SelfDestruct;
        animator = GetComponent<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        startPos = this.gameObject.transform;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();

        EntryAnimation();
    }
    private void EntryAnimation() {
        animator.Play("BossSpawn");
        Invoke(nameof(IdleState), 1f);
    }
    private void IdleState() {
        ChangeAnimationState(BOSS_IDLE);
        Invoke(nameof(ChooseState), idleDuration);
    }
    private void ChangeAnimationState(string newState) {
        // Stop the same animation from interrupting itself
        if (currentState == newState) return;

        // Play the animation
        animator.Play(newState);

        // Reassign the current state
        currentState = newState;
    }
    private void ChooseState() {
        if (index > attackStates.Length - 1) {
            index = 0;
        }
        string chosenState = attackStates[index];
        if (chosenState == "meteor") {
            int meteorToSpawn = meteorSpawn;
            ChangeAnimationState(BOSS_METEOR);
            Invoke(nameof(IdleState), meteorDuration);
            while (meteorToSpawn > 0) {
                SpawnMeteor(meteorSpawn);
                meteorToSpawn--;
            }
        }
        if (chosenState == "chase") {
            ChangeAnimationState(BOSS_CHASE);
            Invoke(nameof(IdleState), chaseDuration);
        }
        if (chosenState == "shoot") {
            int numToShoot = shootNum;
            ChangeAnimationState(BOSS_SHOOT);
            Invoke(nameof(IdleState), shootDuration);
            while (numToShoot > 0) {
                Shooting(numToShoot);
                numToShoot--;
            }
        }
        if (chosenState == "summon") {
            int enemiesToSummon = enemiesSpawnNum;
            ChangeAnimationState(BOSS_SUMMON);
            Invoke(nameof(IdleState), summonDuration);
            while (enemiesToSummon > 0) {
                Summoning(enemiesToSummon);
                enemiesToSummon--;
            }
        }
        index++;
    }
    private void Shooting(int delay) {
        Invoke(nameof(SpawnBullet), delay * 1.2f);
    }
    private void SpawnBullet() {
        Vector2 shootDir = Random.insideUnitCircle.normalized;
        float mainAngle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        float perpendicularAngle = mainAngle + 90f;

        float randomAngle = Random.Range(-20, 20);
        // float randomAngle = 0;
        float angle1 = -20 + randomAngle;
        float angle2 = -55 + randomAngle;
        float angle3 = -90 + randomAngle;
        float angle4 = -125 + randomAngle;
        float angle5 = -160 + randomAngle;

        GameObject bullet1 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        GameObject bullet2 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        GameObject bullet3 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        GameObject bullet4 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        GameObject bullet5 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Rigidbody2D bullet1RB = bullet1.GetComponent<Rigidbody2D>();
        Rigidbody2D bullet2RB = bullet2.GetComponent<Rigidbody2D>();
        Rigidbody2D bullet3RB = bullet3.GetComponent<Rigidbody2D>();
        Rigidbody2D bullet4RB = bullet4.GetComponent<Rigidbody2D>();
        Rigidbody2D bullet5RB = bullet5.GetComponent<Rigidbody2D>();

        bullet1RB.rotation = angle1;
        bullet2RB.rotation = angle2;
        bullet3RB.rotation = angle3;
        bullet4RB.rotation = angle4;
        bullet5RB.rotation = angle5;

        Vector2 shootDir1 = new Vector2(Mathf.Cos(angle1 * Mathf.Deg2Rad), Mathf.Sin(angle1 * Mathf.Deg2Rad));
        Vector2 shootDir2 = new Vector2(Mathf.Cos(angle2 * Mathf.Deg2Rad), Mathf.Sin(angle2 * Mathf.Deg2Rad));
        Vector2 shootDir3 = new Vector2(Mathf.Cos(angle3 * Mathf.Deg2Rad), Mathf.Sin(angle3 * Mathf.Deg2Rad));
        Vector2 shootDir4 = new Vector2(Mathf.Cos(angle4 * Mathf.Deg2Rad), Mathf.Sin(angle4 * Mathf.Deg2Rad));
        Vector2 shootDir5 = new Vector2(Mathf.Cos(angle5 * Mathf.Deg2Rad), Mathf.Sin(angle5 * Mathf.Deg2Rad));

        bullet1RB.AddForce(shootDir1 * bulletSpeed, ForceMode2D.Impulse);
        bullet2RB.AddForce(shootDir2 * bulletSpeed, ForceMode2D.Impulse);
        bullet3RB.AddForce(shootDir3 * bulletSpeed, ForceMode2D.Impulse);
        bullet4RB.AddForce(shootDir4 * bulletSpeed, ForceMode2D.Impulse);
        bullet5RB.AddForce(shootDir5 * bulletSpeed, ForceMode2D.Impulse);
    }
    private void Summoning(int delay) {
        Invoke(nameof(SpawnAlert), delay * 1.5f);
    }
    private void SpawnAlert() {
        GameObject alert = Instantiate(alertPrefab, new Vector3(Random.Range(-3, 3), Random.Range(-3, 1.5f), 0), Quaternion.identity);
    }
    private void SpawnMeteor(int delay) {
        Invoke(nameof(SpawnMeteorWaves), delay * 0.5f);
    }
    private void SpawnMeteorWaves() {
        Instantiate(meteorPrefab, new Vector3(Random.Range(-4f, 4f), Random.Range(-4, 4f), 0), Quaternion.identity);
        Instantiate(meteorPrefab, new Vector3(Random.Range(-4f, 4f), Random.Range(-4, 4f), 0), Quaternion.identity);
        Instantiate(meteorPrefab, new Vector3(Random.Range(-4f, 4f), Random.Range(-4, 4f), 0), Quaternion.identity);
        Instantiate(meteorPrefab, new Vector3(Random.Range(-4f, 4f), Random.Range(-4, 4f), 0), Quaternion.identity);
    }
    private void Update() {
        if (currentState == BOSS_CHASE) {
            Vector3 targetDir = playerPos.position - animator.transform.position;
            Vector2 moveDir = new Vector2(targetDir.x, targetDir.y).normalized;

            // Rotate the sprite accordingly to move direction (left-right)
            if (moveDir.x < 0) {
                spriteRenderer.flipX = false;
            } else {
                spriteRenderer.flipX = true;
            }

            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, chaseSpeed * Time.deltaTime);
        }
        if (currentState == BOSS_IDLE) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector3(0, 3, 0), chaseSpeed * Time.deltaTime);
        }
        if (health <= 0) {
            GameEvents.StartScreenTransition();
            GameEvents.GameWin();
            Destroy(gameObject);
            GameEvents.Screenshake();
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Water") {
            health -= 1;
            Destroy(other.gameObject);
        }
    }
}
