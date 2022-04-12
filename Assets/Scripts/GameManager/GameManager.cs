using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private GameObject player;
    private GameObject gameUI;
    public bool playerAlive = true;
    public float respawnTime = 2f;
    public float iFrameTime = 2f;

    // ------------PLAYER LIVES MONITOR------------
    public int maxLives = 10;
    public int currentLives;
    public Text liveDisplay;
    // ------------PARTICLE EFFECT------------
    public ParticleSystem explosion;
    // ----------SPAWNERS----------
    public GameObject enemySpawner;
    public GameObject obstacleSpawner;
    // ----------STAGE----------
    public int currentStage = 1;
    public Text stageDisplay;
    public int score = 0;
    // ----------CARDS----------
    public List<GameObject> cardsList;
    private List<GameObject> availableCards;
    private Canvas cardUI;
    private GameObject spawnedCard1;
    private GameObject spawnedCard2;
    public bool playerIsGhosted = false;
    public bool timeStopOccasionally = false;
    public float defaultGameSpeed = 1f;
    public bool criticalEnergy = false;
    public GameObject bossPrefab;

    // ----------PLAYER GET HIT----------
    private void DisablePlayer() {
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.transform.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    private void EnablePlayer() {
        if (playerIsGhosted == false) {
            player.GetComponent<SpriteRenderer>().enabled = true;
        }
        player.transform.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    private void DisableGameUI() {
        gameUI.GetComponent<Canvas>().enabled = false;
    }
    private void EnableGameUI() {
        gameUI.GetComponent<Canvas>().enabled = true;
    }
    private void PlayerHit(onEventData data) {
        if (currentLives != 0) {
            currentLives--;
            player = data.sender;
            PlayerDied();
            GameEvents.UpdatePlayerLives(currentLives);
        } else {
            GameEvents.StartScreenTransition();
            // Invoke(nameof(RestartScene), 0.5f);
            RestartScene();
        }
    }
    private void RestartScene() {
        SceneManager.LoadScene("GameLose");
    }
    public void PlayerDied() {
        /* When player dies:
        + Set the player to inactive
        + Move player to the iFrame state
        + Disable spawners
        + Player state is now dead
        + Explosion + screenshake 
        + Destroy remaining enemies 
        + Reset score */

        // player.gameObject.SetActive(false);
        DisablePlayer();
        player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");

        // enemySpawner.SetActive(false);
        // obstacleSpawner.SetActive(false);
        DisableSpawner();

        playerAlive = false;
        GameEvents.Screenshake();
        Instantiate(explosion, player.transform.position, Quaternion.identity);
        score = 0;
        // GameEvents.PlayerDieDestroyObjects();

        if (currentLives <= 0) {
            // GameOver();
            Invoke(nameof(RestartScene), 1.5f);
        } else {
            Invoke(nameof(Respawn), respawnTime);
        }

        GameEvents.StartScreenTransition();
    }

    private void GameOver() {
        currentLives = maxLives;
        Invoke(nameof(Respawn), respawnTime);
    }
    private void Respawn() {
        // Destroy all remaining enemies
        GameEvents.PlayerDieDestroyObjects();
        player.gameObject.transform.position = Vector3.zero;
        // player.gameObject.SetActive(true);
        EnablePlayer();
        if (!criticalEnergy) {
            player.GetComponent<Player>().energy = 100f;
        } else {
            player.GetComponent<Player>().energy = 15f;
        }
        
        // turn off player's collision (iFrame)
        playerAlive = true;
        Invoke(nameof(TurnOnCollision), iFrameTime);
         
        Invoke(nameof(EnableSpawners), 2f);
        

        GameEvents.EndScreenTransition();
        Time.timeScale = defaultGameSpeed;
        StartPauseTime();
        if (currentStage >= 10) {
            SpawnBoss();
            enemySpawner.GetComponent<EnemySpawner>().StopAllCoroutines();
        }
    }
    private void SpawnBoss() {
        Instantiate(bossPrefab, new Vector3(0, 3, 0), Quaternion.identity);
    }
    private void DisableSpawner() {
        enemySpawner.GetComponent<EnemySpawner>().StopAllCoroutines();
        obstacleSpawner.GetComponent<ObstacleSpawner>().StopAllCoroutines();
    }
    private void EnableSpawners() {
        if (currentStage < 10) enemySpawner.GetComponent<EnemySpawner>().StartSpawning();
        obstacleSpawner.GetComponent<ObstacleSpawner>().StartSpawning();
    }
    private void TurnOnCollision() {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    // ----------GAME MANAGER EVENT HANDLER----------
    void Start()
    {   
        // PLAYER LIVES MONITOR
        currentLives = maxLives;

        stageDisplay.GetComponent<Text>().text = currentStage.ToString("D2") + "/10";

        availableCards = cardsList;

        cardUI = GameObject.Find("Card UI").GetComponent<Canvas>();
        obstacleSpawner = GameObject.Find("Obstacle Spawner");
        enemySpawner = GameObject.Find("Enemy Spawner");

        StartPauseTime();

        GameEvents.UpdatePlayerLives(currentLives);
    }
    private void GetPlayer(onEventData data) {
        player = data.sender;
    }
    private void GetGameUI(onEventData data) {
        gameUI = data.sender;
    }
    // private void GetCardUI(onEventData data) {
    //     // cardUI = data.sender.GetComponent<Canvas>();
    //     cardUI = GameObject.Find("Card UI").GetComponent<Canvas>();
    //     Debug.Log(cardUI);
    // }
    private void ScoreIncrease() {
        score += 1;
    }
    private void Awake() {
        GameEvents.onPlayerHit += PlayerHit;
        GameEvents.onMassiveScreenshake += MassiveScreenshake;
        GameEvents.onGameStartGetPlayer += GetPlayer;
        GameEvents.onGameStartGetUI += GetGameUI;
        GameEvents.onEnemyDie += ScoreIncrease;
        GameEvents.onResumeGameAfterCards += ResumeGame;
        // GameEvents.onChooseCardGetCardUI += GetCardUI;
        GameEvents.onSelectACardGetCardID += DeleteSelectedCardFromPile;
        GameEvents.onBossDie += GameWinEndGame;
    }
    void OnDisable() {
        GameEvents.onPlayerHit -= PlayerHit;
        GameEvents.onMassiveScreenshake -= MassiveScreenshake;
        GameEvents.onGameStartGetPlayer -= GetPlayer;
        GameEvents.onGameStartGetUI -= GetGameUI;
        GameEvents.onEnemyDie -= ScoreIncrease;
        GameEvents.onResumeGameAfterCards -= ResumeGame;
        // GameEvents.onChooseCardGetCardUI -= GetCardUI;
        GameEvents.onSelectACardGetCardID -= DeleteSelectedCardFromPile;
        GameEvents.onBossDie -= GameWinEndGame;
    }
    private void GameWinEndGame() {
        Invoke(nameof(GameWinScene), 1.5f);
    }
    private void GameWinScene() {
        SceneManager.LoadScene("GameWin");
    }
    private void Update() {
        // Check score
        if (score == currentStage + 4  && currentStage < 10) {
        // if (score == 1 && currentStage < 10) {
            currentStage += 1;
            score = 0;
            GameEvents.PlayerDieDestroyObjects();
            // Disable the player script (no more input)
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
            player.GetComponent<Player>().enabled = !player.GetComponent<Player>().enabled;
            
            // enemySpawner.SetActive(false);
            // obstacleSpawner.SetActive(false);
            DisableSpawner();
            CancelInvoke();
            Invoke(nameof(ChooseCardCleanup), respawnTime);
        }

        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     PauseGameTime();
            
        // }
        // if (Input.GetKeyDown(KeyCode.C)) {
        //     ResumeGameTime();
        // }

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("Menu");
        }
    }
    private void StartPauseTime() {
        if (timeStopOccasionally) {
            InvokeRepeating(nameof(PauseGameTime), 2f, Random.Range(4f, 7f));
        }
    }
    private void PauseGameTime() {
        Debug.Log("Stop!");
        Time.timeScale = 0.05f;
        Invoke(nameof(ResumeGameTime), 0.015f);
    }
    private void ResumeGameTime() {
        Time.timeScale = defaultGameSpeed;
    }

    private void ChooseCardCleanup() {
        // CancelInvoke();
        GameEvents.StartScreenTransition();
        Invoke(nameof(ChooseCard), respawnTime);
    }
    private void ChooseCard() {
        // gameUI.SetActive(false);
        DisableGameUI();
        GameEvents.PlayerDieDestroyObjects();
        // player.gameObject.SetActive(false);
        DisablePlayer();
        GameEvents.EndScreenTransition();
        cardUI.enabled = true;

        int midPoint = availableCards.Count/2;

        var index1 = Random.Range(0,(midPoint));
        var index2 = Random.Range(midPoint + 1,(availableCards.Count));
        if (availableCards.Count > 2) {
            spawnedCard1 = Instantiate(availableCards[index1], new Vector3(3, 0, 0), Quaternion.identity);
            spawnedCard2 = Instantiate(availableCards[index2], new Vector3(-3, 0, 0), Quaternion.identity);
            // spawnedCard1 = Instantiate(availableCards[index1], new Vector3(3, 0, 0), Quaternion.identity);
            // spawnedCard2 = Instantiate(availableCards[index2], new Vector3(-3, 0, 0), Quaternion.identity);
        } else {
            spawnedCard1 = Instantiate(availableCards[0], new Vector3(3, 0, 0), Quaternion.identity);
            spawnedCard2 = Instantiate(availableCards[1], new Vector3(-3, 0, 0), Quaternion.identity);
        }
        
        // availableCards.Remove(availableCards[index]);
        // availableCards.Remove(spawnedCard1);
        // availableCards.Remove(spawnedCard2);
    }
    private void DeleteSelectedCardFromPile(int idToDelete) {
        for (int i = 0; i < availableCards.Count; i++) {
            if (availableCards[i].GetComponent<Card>().id == idToDelete) {
                availableCards.Remove(availableCards[i]);
            }
        }
    }
    private void ResumeGame() {
        stageDisplay.GetComponent<Text>().text = currentStage.ToString("D2") + "/10";
        
        GameEvents.StartScreenTransition();
        
        // gameUI.SetActive(true);
        Invoke(nameof(EnableGameUIDisableCardUI), respawnTime/2);
        Invoke(nameof(Respawn), respawnTime);
        
    }
    private void EnableGameUIDisableCardUI() {
        Destroy(spawnedCard1);
        Destroy(spawnedCard2);
        player.GetComponent<Player>().enabled = true;
        cardUI.enabled = false;
        EnableGameUI();
        GameEvents.ChangeAllColor();
    }
    // ---------GAME MANAGER CURSES---------
    private void MassiveScreenshake(float strengthModifier) {
        GetComponent<ScreenShake>().strengthModifier = strengthModifier;
    }
    
}
